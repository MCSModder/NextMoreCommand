using SkySwordKill;
using SkySwordKill.Next;
using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("RunTempFlowchart")]
    public class RunTempFlowchart : IDialogEvent
    {
        private Flowchart _flowchart;
        private Block _block;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            string key = command.GetStr(0);
            string tagBlock = command.GetStr(1);
            int ItemId = command.GetInt(2, -1);

            DialogAnalysis.CancelEvent();
            _flowchart = TempFlowchart.GetFlowchart(key);

            if (_flowchart == null)
            {
                MyLog.FungusLogError("对应flowchart不存在");
                return;
            }

            var index = FindIndex(tagBlock, ItemId);
            if (index < 0)
            {
                MyLog.FungusLog($"跳转FungusBlock {tagBlock}");
                _flowchart.ExecuteBlock(tagBlock);
            }
            else
            {
                MyLog.FungusLog($"跳转FungusBlock {tagBlock} ItemId:{ItemId} index:{index}");
                _flowchart.ExecuteBlock(_block, index);
            }

            _block = null;
            _flowchart = null;
        }

        private int FindIndex(string tagBlock, int itemId)
        {
            _block = _flowchart.FindBlock(tagBlock);
            if (_block == null || itemId < 0) return -1;
            foreach (var command in _block.commandList)
            {
                if (command.ItemId == itemId) return command.CommandIndex;
            }

            return -1;
        }
    }
}