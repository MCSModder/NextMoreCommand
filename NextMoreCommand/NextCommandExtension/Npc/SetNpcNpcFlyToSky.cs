using System;
using script.EventMsg;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetNpcNpcFlyToSky")]
    [DialogEvent("设置角色飞升")]
    public class SetNpcNpcFlyToSky : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npc = NPCEx.NPCIDToNew(command.GetInt(0, -1));
            var data = NpcJieSuanManager.inst.GetNpcData(npc);
            data.SetField("IsFly", true);
            EventMag.Inst.SaveEvent(npc, 4);
            
            callback?.Invoke();
        }
    }
}