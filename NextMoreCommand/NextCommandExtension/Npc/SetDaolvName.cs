using System;
using System.Runtime.InteropServices.WindowsRuntime;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [DialogEvent("SetDaolvName")]
    public class SetDaolvName : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npcId = command.ToNpcId();
            var chengHu = command.GetStr(1);
            if (PlayerEx.IsDaoLv(npcId) && !string.IsNullOrWhiteSpace(chengHu))
            {
                PlayerEx.SetDaoLvChengHu(npcId,chengHu);
            }
            callback?.Invoke();
        }
    }
}