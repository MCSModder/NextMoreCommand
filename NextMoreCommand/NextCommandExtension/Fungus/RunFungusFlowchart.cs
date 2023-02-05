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
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var flowchartName = command.GetStr(0);
            var tagBlock = command.GetStr(1);
           MyPluginMain.LogInfo($"FungusEvent : RunFungusFlowchart");

            if (FungusUtils.GetFlowchart(flowchartName) == null)
            {
               MyPluginMain.LogError($"FungusEvent : 对应{flowchartName} flowchart不存在");
            }
            else
            {
                FungusUtils.TalkBlockName = tagBlock;
                FungusUtils.TalkFunc = flowchart => flowchart.ExecuteIfHasBlock(FungusUtils.TalkBlockName);
                FungusUtils.TalkOnComplete = () =>MyPluginMain.LogInfo($"FungusEvent : 跳转FungusBlock {FungusUtils.TalkBlockName}");
                FungusUtils.TalkOnFailed = () => MyLog.FungusLogError($"Block名称不存在 {FungusUtils.TalkBlockName}");
                FungusUtils.isTalkActive = true;
            }


            callback?.Invoke();
        }
    }
}