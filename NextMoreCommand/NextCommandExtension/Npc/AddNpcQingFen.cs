using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("AddNpcQingFen")]
    [DialogEvent("增加角色情分")]
    public class AddNpcQingFen : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npc = command.ToNpcId();
            var qingFeng = command.GetInt(1, 0);
            NPCEx.AddQingFen(npc, qingFeng);
       
            callback?.Invoke();
        }
    }
}