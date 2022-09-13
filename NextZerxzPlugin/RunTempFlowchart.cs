using SkySwordKill;
using SkySwordKill.Next;
using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand
{
    [DialogEvent("RunTempFlowchart")]
    public class RunTempFlowchart : IDialogEvent
    {
        private Flowchart flowchart;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            string key = command.GetStr(0);
            string tagBlock = command.GetStr(1);
            int ItemId = command.GetInt(2, -1);

            DialogAnalysis.CancelEvent();
            flowchart = TempFlowchart.GetFlowchart(key);

            if (flowchart != null)
            {
                var index = FindIndex(tagBlock, ItemId);
                if (index < 0)
                {
                    Next.Main.LogInfo("FungusEvent : 跳转FungusBlock " + tagBlock);
                    flowchart.ExecuteBlock(tagBlock);
                }
                else
                {
                    var block = flowchart.FindBlock(tagBlock);
                    Next.Main.LogInfo( $"FungusEvent : 跳转FungusBlock {tagBlock} ItemId:{ItemId} index:{index} ");
                    flowchart.ExecuteBlock(block, index);
                }

                return;
            }

            Next.Main.LogError("FungusEvent : 对应flowchart不存在");
        }
        
        private int FindIndex(string tagBlock, int itemId)
        {
            var block = flowchart.FindBlock(tagBlock);
            if (block == null || itemId < 0) return -1;
            foreach (var command in block.commandList)
            {
                if (command.ItemId == itemId) return command.CommandIndex;
            }

            return -1;
        }
    }
}