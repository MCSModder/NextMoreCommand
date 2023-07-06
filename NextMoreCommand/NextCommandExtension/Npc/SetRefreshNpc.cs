using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [DialogEvent("SetRefreshNpc")]
    public class SetRefreshNpc : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npc = command.ToNpcId();
            NpcUtils.RefreshNpc(npc);
            callback?.Invoke();
        }
    }
}