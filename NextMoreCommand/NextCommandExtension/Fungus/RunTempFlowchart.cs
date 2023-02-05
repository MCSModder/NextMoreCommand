using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fungus
{
    [RegisterCommand]
    [DialogEvent("RunTempFlowchart")]
    [DialogEvent("运行临时对话")]
    public class RunTempFlowchart : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            string key = command.GetStr(0);
            string tagBlock = command.GetStr(1);
            int itemId = command.GetInt(2, -1);

            DialogAnalysis.CancelEvent();
            var flowchart = TempFlowchart.GetFlowchart(key);

            if (flowchart == null)
            {
                MyLog.FungusLogError("对应flowchart不存在");
                return;
            }

            var index = flowchart.FindIndex(tagBlock, itemId, out Block block);
            if (index < 0)
            {
                MyLog.FungusLog($"跳转FungusBlock {tagBlock}");
                flowchart.ExecuteBlock(tagBlock);
            }
            else
            {
                MyLog.FungusLog($"跳转FungusBlock {tagBlock} ItemId:{itemId} index:{index}");
                flowchart.ExecuteBlock(block, index);
            }

            callback?.Invoke();
        }
    }
}