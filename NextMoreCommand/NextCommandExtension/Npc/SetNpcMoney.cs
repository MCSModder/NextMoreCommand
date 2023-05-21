using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [RegisterCommand]
    [DialogEvent("SetNpcMoney")]
    [DialogEvent("设置角色灵石")]
    public class SetNpcMoney : IDialogEvent
    {
        private int npc;
        private int money;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            money = command.GetInt(1, 0);
            if (npc > 0)
            {
                var data = jsonData.instance.AvatarBackpackJsonData[npc.ToNpcId()];
                var nowMoney = data.GetInt("money");
                MyLog.Log(command, $"角色ID:{npc} 角色名:{npc.GetNpcName()} 当前灵石:{nowMoney} 设置灵石:{money}");
                MyLog.Log(command, $"");
                NPCEx.SetMoney(npc, money);
            }
            else
            {
                MyLog.Log(command, $"角色ID:{npc} 不能为小于等于 0", true);
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}