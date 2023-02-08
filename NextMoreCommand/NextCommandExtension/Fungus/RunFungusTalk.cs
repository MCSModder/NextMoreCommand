using System;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fungus
{
    [RegisterCommand]
    [DialogEvent("RunFungusTalk")]
    [DialogEvent("运行官方对话")]
    public class RunFungusTalk : IDialogEvent
    {
        private int talkID;
        private string tagBlock;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            talkID = command.GetInt(0, -1);
            tagBlock = command.GetStr(1);

            MyLog.Log(command, $"开始创建官方对话 流程名:Talk{talkID} 模块名:{tagBlock}");

            if (FungusUtils.GetTalk(talkID) == null)
            {
                MyLog.Log(command, $"创建失败官方对话 流程名:Talk{talkID} 模块名:{tagBlock} 不存在该流程名", true);
            }
            else
            {
                FungusUtils.TalkBlockName = tagBlock;
                FungusUtils.TalkFunc = flowchart =>
                {
                    MyLog.Log(command, $"开始执行官方对话 流程名:Talk{talkID} 模块名:{tagBlock}");
                    return flowchart.ExecuteIfHasBlock(FungusUtils.TalkBlockName);
                };
                FungusUtils.TalkOnComplete = () =>
                {
                    MyLog.Log(command, $"执行完毕官方对话开始跳转 流程名:Talk{talkID}  模块名:{tagBlock}");
                    MyLog.LogCommand(command, false);
                };
                FungusUtils.TalkOnFailed = () =>
                {
                    MyLog.Log(command, $"执行失败官方对话 流程名:Talk{talkID}  模块名:{tagBlock} 找不到对应模块名", true);
                    MyLog.LogCommand(command, false);
                };
                FungusUtils.isTalkActive = true;
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}