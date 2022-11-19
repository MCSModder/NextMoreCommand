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
            if (!FungusUtils.TryGetFlowchart(flowchartName, out Flowchart flowchart))
            {
                Main.LogError($"FungusEvent : 对应{flowchartName} flowchart不存在");
                return;
            }
           

            
            if ( flowchart.ExecuteIfHasBlock(tagBlock))
            {
                Main.LogInfo($"FungusEvent : 跳转FungusBlock {tagBlock}");
               
            }
            else
            {
                MyLog.FungusLogError($"Block名称不存在 {tagBlock}");
               
            }
            callback?.Invoke();
        }

    }
}