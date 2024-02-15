using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [RegisterCommand]
    [DialogEvent("AddNpcMoney")]
    [DialogEvent("增加角色灵石")]
    public class AddNpcMoney : IDialogEvent
    {
        private int _npc;
        private int _money;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            _npc = command.ToNpcId();
            _money = command.GetInt(1, 0);
            MyLog.LogCommand(command);
            if (_npc > 0)
            {
                MyLog.Log(command, $"开始执行增加角色灵石 角色ID:{_npc} 角色名:{_npc.GetNpcName()} 增加灵石:{_money}");
                var data        = jsonData.instance.AvatarBackpackJsonData[_npc.ToNpcId()];
                var nowMoney    = data.GetInt("money");
                var resultMoney = nowMoney + _money;
                MyLog.Log(command, $"执行增加角色灵石 当前灵石:{nowMoney} 增加灵石:{_money} 灵石结果:{resultMoney}");
                NPCEx.SetMoney(_npc, resultMoney);
            }
            else
            {
                MyLog.Log(command, $"执行失败增加角色灵石 角色ID:{_npc} 不能小于 0", true);
            }


            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}