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
    [DialogEvent("SetNpcDeath")]
    public class SetNpcDeath : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var deathType = command.GetInt(0);
            var npcId = command.GetInt(1);
            var killNpcId = command.GetInt(2);
            var after = command.GetBool(3);
            var I = NpcJieSuanManager.inst;
            if (I != null)
            {
                MyLog.FungusLog($"设置 Npc死亡类型{(DeathType)deathType} NpcID:{npcId}");
                I.npcDeath.SetNpcDeath(deathType, npcId, killNpcId, after);
            }
            else
            {
                Main.LogError("NpcJieSuanManager还没有实例化");
            }
        }
    }
}