using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;
using Tab;
using YSGame.Fight;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("SetRemoveEquip")]
    [DialogEvent("移除装备")]
    public class SetRemoveEquip : IDialogEvent
    {


        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            TabUIMag.OpenTab2(4);
            if (TabUIMag.Instance != null)
            {
              
                var itemPanel = TabUIMag.Instance.WuPingPanel;
                var list = command.ToListInt();
                var dict = itemPanel.EquipDict;
                foreach (var type in list)
                {
                    if (dict.TryGetValue(type, out var equipSlot) && equipSlot.Item != null)
                    {
                        itemPanel.RmoveEquip((EquipSlotType)type);
                    }
                }


                TabUIMag.Instance.TryEscClose();
            }

            callback?.Invoke();
        }
    }
}