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
            MyLog.LogCommand(command);
            var npc = command.ToNpcId();
            var qingFeng = command.GetInt(1, 0);
            if (npc > 0)
            {
                var nowQinFeng = NPCEx.GetFavor(npc);
                var resultQinFeng = nowQinFeng + qingFeng;
                MyLog.Log(command, $"开始执行增加角色情分 角色ID:{npc} 角色名:{npc.GetNpcName()} 增加情分:{qingFeng}");
                MyLog.Log(command, $"执行增加角色情分 当前情分:{nowQinFeng} 增加情分:{qingFeng} 情分结果:{resultQinFeng}");
                NPCEx.AddQingFen(npc, qingFeng, true);
            }
            else
            {
                MyLog.Log(command, $"执行失败增加角色情分 角色ID:{npc} 不能小于 0", true);
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}