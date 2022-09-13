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
        private Flowchart _flowchart;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            string key = command.GetStr(0);
            string tagBlock = command.GetStr(1);
            int ItemId = command.GetInt(2, -1);

            DialogAnalysis.CancelEvent();
            _flowchart = TempFlowchart.GetFlowchart(key);

            if (_flowchart != null)
            {
                var index = FindIndex(tagBlock, ItemId);
                if (index < 0)
                {
                    Main.LogInfo("FungusEvent : 跳转FungusBlock " + tagBlock);
                    _flowchart.ExecuteBlock(tagBlock);
                }
                else
                {
                    var block = _flowchart.FindBlock(tagBlock);
                  Main.LogInfo( $"FungusEvent : 跳转FungusBlock {tagBlock} ItemId:{ItemId} index:{index} ");
                    _flowchart.ExecuteBlock(block, index);
                }

                return;
            }

          Main.LogError("FungusEvent : 对应flowchart不存在");
        }
        
        private int FindIndex(string tagBlock, int itemId)
        {
            var block = _flowchart.FindBlock(tagBlock);
            if (block == null || itemId < 0) return -1;
            foreach (var command in block.commandList)
            {
                if (command.ItemId == itemId) return command.CommandIndex;
            }

            return -1;
        }
    }
}