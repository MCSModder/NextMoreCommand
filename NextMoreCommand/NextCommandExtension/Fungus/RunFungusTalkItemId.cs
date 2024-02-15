using System;
using Fungus;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fungus
{
    [RegisterCommand]
    [DialogEvent("RunFungusTalkItemId")]
    [DialogEvent("运行官方对话ID")]
    public class RunFungusTalkItemId : IDialogEvent
    {
        private int    talkID;
        private string tagBlock;
        private int    itemId;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            talkID = command.GetInt(0, -1);
            var flowchartName = $"Talk{talkID}";
            tagBlock = command.GetStr(1);
            itemId = command.GetInt(2, -1);

            DialogAnalysis.CancelEvent();
            MyLog.Log(command, $"开始创建官方对话ID 流程名:对应Talk{talkID} 模块名:{tagBlock} 对话Id:{itemId}");

            if (FungusUtils.GetTalk(talkID) == null)
            {
                MyLog.Log(command, $"失败创建官方对话ID 流程名:Talk:{talkID} 不存在该流程名", true);
            }
            else
            {
                FungusUtils.TalkBlockName = tagBlock;
                FungusUtils.TalkItemId = itemId;
                FungusUtils.TalkFunc = flowchart =>
                {
                    MyLog.LogCommand(command);
                    MyLog.Log(command, $"开始执行官方对话 流程名:{flowchartName} 模块名:{tagBlock} 对话Id:{itemId}");
                    var index = flowchart.FindIndex(FungusUtils.TalkBlockName, FungusUtils.TalkItemId, out var block);
                    if (block == null)
                    {
                        MyLog.Log(command, $"失败执行官方对话 模块名:{tagBlock} 不存在", true);
                        return false;
                    }

                    if (index < 0)
                    {
                        var msg = itemId == -1 ? "对话Id不能为空和字符串" : $" {FungusUtils.TalkItemId.ToString()} 对话Id不存在";
                        MyLog.Log(command, $"失败执行官方对话 {msg}", true);
                        return false;
                    }

                    MyLog.Log(command, $"官方对话开始跳转 流程名:{flowchartName} 模块名:{tagBlock} 对话Id:{itemId}");
                    flowchart.ExecuteBlock(block, index);
                    return true;
                };
                FungusUtils.TalkOnComplete = () =>
                {
                    MyLog.Log(command, $"官方对话跳转完毕 流程名:{flowchartName} 模块名:{tagBlock}  对话Id:{itemId}");
                    MyLog.LogCommand(command, false);
                };
                FungusUtils.TalkOnFailed = () =>
                {
                    MyLog.Log(command, $"执行失败官方对话 流程名:{flowchartName} 模块名:{tagBlock}  对话Id:{itemId} 找不到对应模块名或者对话Id",
                        true);
                    MyLog.LogCommand(command, false);
                };
                FungusUtils.isTalkActive = true;
            }

            MyLog.LogCommand(command);
            callback?.Invoke();
        }
    }
}