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
    [DialogEvent("RunFungusItemId")]
    public class RunFungusItemId : IDialogEvent
    {
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

            var flowchart = env.flowchart;
            var index = flowchart.FindIndex(tagBlock, itemId, out Block block);
            
            if (block == null)
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
                flowchart.ExecuteBlock(block, index);
            }

            callback?.Invoke();
        }
    }
}