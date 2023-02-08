using System;
using script.EventMsg;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetNpcFlyToSky")]
    [DialogEvent("设置角色飞升")]
    public class SetNpcNpcFlyToSky : IDialogEvent
    {
        private int npc;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId(0, -1);
            if (npc > 0)
            {
                MyLog.Log(command, $"设置角色飞升 角色ID:{npc} 角色名:{npc.GetNpcName()}");
                var data = NpcJieSuanManager.inst.GetNpcData(npc);
                data.SetField("IsFly", true);
                EventMag.Inst.SaveEvent(npc, 4);
            }
            else
            {
                MyLog.Log(command, $"设置角色飞升失败 角色ID:{npc} 不能为小于等于 0", true);
            }

            MyLog.LogCommand(command, false);

            callback?.Invoke();
        }
    }
}