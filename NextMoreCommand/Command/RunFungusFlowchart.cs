using SkySwordKill;
using SkySwordKill.Next;
using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("RunFungusFlowchart")]
    public class RunFungusFlowchart : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var flowchartName = command.GetStr(0);
            var tagBlock = command.GetStr(1);
            Main.LogInfo($"FungusEvent : RunFungusFlowchart");

            if (FungusUtils.GetFlowchart(flowchartName) == null)
            {
                Main.LogError($"FungusEvent : 对应{flowchartName} flowchart不存在");
            }
            else
            {
                FungusUtils.TalkBlockName = tagBlock;
                FungusUtils.TalkFunc = flowchart => flowchart.ExecuteIfHasBlock(FungusUtils.TalkBlockName);
                FungusUtils.TalkOnComplete = () => Main.LogInfo($"FungusEvent : 跳转FungusBlock {FungusUtils.TalkBlockName}");
                FungusUtils.TalkOnFailed = () => MyLog.FungusLogError($"Block名称不存在 {FungusUtils.TalkBlockName}");
                FungusUtils.isTalkActive = true;
            }


            callback?.Invoke();
        }
    }
}