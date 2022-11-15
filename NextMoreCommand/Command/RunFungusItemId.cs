using SkySwordKill;
using SkySwordKill.Next;
using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("RunFungusItemId")]
    public class RunFungusItemId : IDialogEvent
    {
        private Flowchart _flowchart;
        private Block _block;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var tagBlock = command.GetStr(0);
            var itemId = command.GetInt(1, -1);

            DialogAnalysis.CancelEvent();

            if (env.flowchart == null)
            {
                Main.LogError("FungusEvent : 对应flowchart不存在");
                return;
            }

            var index = FindIndex(tagBlock, itemId);
            if (_block == null)
            {
                MyLog.FungusLogError($"Block名称不存在 {tagBlock}");
            }
            else if (index < 0)
            {
                var msg = itemId == -1 ? "ItemId不能为空和字符串" : $"ItemId不存在 {itemId}";
                MyLog.FungusLogError(msg);
            }
            else
            {
                Main.LogInfo($"FungusEvent : 跳转FungusBlock {tagBlock} ItemId:{itemId} index:{index} ");
                env.flowchart.ExecuteBlock(_block, index);
            }

            _block = null;
            _flowchart = null;
            callback?.Invoke();
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