using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("AddNpcMoney")]
    [DialogEvent("增加角色灵石")]
    public class AddNpcMoney : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npc = command.ToNpcId();
            var money = command.GetInt(1, 0);
            NPCEx.AddJsonInt(npc,"money",money);
         
            callback?.Invoke();
        }
    }
}