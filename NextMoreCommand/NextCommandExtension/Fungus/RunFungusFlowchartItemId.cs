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
    [DialogEvent("RunFungusFlowchartItemId")]
    [DialogEvent("运行官方对话流程ID")]
    public class RunFungusFlowchartItemId : IDialogEvent
    {
        private string _flowchartName;
        private string _tagBlock;
        private int    _itemId;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            _flowchartName = command.GetStr(0);
            _tagBlock = command.GetStr(1);
            _itemId = command.GetInt(2, -1);

            MyLog.Log(command, $"开始创建官方对话流程ID 流程名:{_flowchartName} 模块名:{_tagBlock} 对话Id:{_itemId}");
            if (FungusUtils.GetFlowchart(_flowchartName) == null)
            {
                MyLog.Log(command, $"失败创建官方对话流程ID 流程名:{_flowchartName} 不存在该流程名", true);
            }
            else
            {
                FungusUtils.TalkBlockName = _tagBlock;
                FungusUtils.TalkItemId = _itemId;
                FungusUtils.TalkFunc = flowchart =>
                {
                    MyLog.LogCommand(command);
                    MyLog.Log(command, $"开始执行官方对话流程 流程名:{_flowchartName} 模块名:{_tagBlock} 对话Id:{_itemId}");
                    var index = flowchart.FindIndex(FungusUtils.TalkBlockName, FungusUtils.TalkItemId, out Block block);
                    if (block == null)
                    {
                        MyLog.Log(command, $"失败执行官方对话流程 模块名:{_tagBlock} 不存在", true);
                        return false;
                    }

                    if (index < 0)
                    {
                        var msg = _itemId == -1 ? "对话Id不能为空和字符串" : $" {FungusUtils.TalkItemId.ToString()} 对话Id不存在";
                        MyLog.Log(command, $"失败执行官方对话流程 {msg}", true);
                        return false;
                    }

                    MyLog.Log(command, $"官方对话流程开始跳转 流程名:{_flowchartName} 模块名:{_tagBlock} 对话Id:{_itemId}");
                    flowchart.ExecuteBlock(block, index);
                    return true;
                };
                FungusUtils.TalkOnComplete = () =>
                {
                    MyLog.Log(command, $"官方对话流程跳转完毕 流程名:{_flowchartName} 模块名:{_tagBlock}  对话Id:{_itemId}");
                    MyLog.LogCommand(command, false);
                };
                FungusUtils.TalkOnFailed = () =>
                {
                    MyLog.Log(command,
                        $"执行失败官方对话流程 流程名:{_flowchartName} 模块名:{_tagBlock}  对话Id:{_itemId} 找不到对应模块名或者对话Id", true);
                    MyLog.LogCommand(command, false);
                };
                FungusUtils.isTalkActive = true;
            }


            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}