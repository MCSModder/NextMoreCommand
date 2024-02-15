using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [DialogEvent("SetBindNpc")]
    public class SetBindNpc : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npcId = command.ToNpcId();
            if (npcId > 1)
            {
                var npc = new UINPCData(npcId);
                npc.RefreshData();
                env.bindNpc = npc;
                env.roleBindID = npc.ZhongYaoNPCID;
                env.roleID = npc.ID;
                env.roleName = npc.Name;
            }

            callback?.Invoke();
        }
    }
}