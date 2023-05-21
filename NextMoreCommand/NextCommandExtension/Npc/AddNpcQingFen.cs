using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [RegisterCommand]
    [DialogEvent("AddNpcQingFen")]
    [DialogEvent("增加角色情分")]
    public class AddNpcQingFen : IDialogEvent
    {
        private int _npc;
        private int _qingFeng;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            _npc = command.ToNpcId();
            _qingFeng = command.GetInt(1, 0);
            if (_npc > 0)
            {
                var nowQinFeng = NPCEx.GetFavor(_npc);
                var resultQinFeng = nowQinFeng + _qingFeng;
                MyLog.Log(command, $"开始执行增加角色情分 角色ID:{_npc} 角色名:{_npc.GetNpcName()} 增加情分:{_qingFeng}");
                MyLog.Log(command, $"执行增加角色情分 当前情分:{nowQinFeng} 增加情分:{_qingFeng} 情分结果:{resultQinFeng}");
                NPCEx.AddQingFen(_npc, _qingFeng, true);
            }
            else
            {
                MyLog.Log(command, $"执行失败增加角色情分 角色ID:{_npc} 不能小于 0", true);
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}