using System;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fungus
{
    [RegisterCommand]
    [DialogEvent("RunFungusFlowchart")]
    [DialogEvent("运行官方对话流程")]
    public class RunFungusFlowchart : IDialogEvent
    {
        private string _flowchartName;
        private string _tagBlock;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            _flowchartName = command.GetStr(0);
            _tagBlock = command.GetStr(1);

            MyLog.Log(command, $"开始创建官方对话流程 流程名:{_flowchartName} 模块名:{_tagBlock}");
            if (FungusUtils.GetFlowchart(_flowchartName) == null)
            {
                MyLog.Log(command, $"失败创建官方对话流程 流程名:{_flowchartName} 不存在该流程名", true);
            }
            else
            {
                FungusUtils.TalkBlockName = _tagBlock;
                FungusUtils.TalkFunc = flowchart =>
                {
                    MyLog.LogCommand(command);
                    MyLog.Log(command, $"开始执行官方对话流程 流程名:{_flowchartName} 模块名:{_tagBlock}");
                    return flowchart.ExecuteIfHasBlock(FungusUtils.TalkBlockName);
                };
                FungusUtils.TalkOnComplete = () =>
                {
                    MyLog.Log(command, $"执行完毕官方对话流程开始跳转 流程名:{_flowchartName} 模块名:{_tagBlock}");
                    MyLog.LogCommand(command, false);
                };
                FungusUtils.TalkOnFailed = () =>
                {
                    MyLog.Log(command, $"执行失败官方对话流程 流程名:{_flowchartName} 模块名:{_tagBlock} 找不到对应模块名", true);
                    MyLog.LogCommand(command, false);
                };
                FungusUtils.isTalkActive = true;
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}