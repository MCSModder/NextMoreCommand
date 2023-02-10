using System;
using System.Collections.Generic;
using JSONClass;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetNpcWudaoExp")]
    [DialogEvent("设置角色悟道经验")]
    public class SetNpcWudaoExp : IDialogEvent
    {
        private int npc;
        private int wudaoId;
        private int exp;
        private Dictionary<int, WuDaoAllTypeJson> WuDaoAllType => WuDaoAllTypeJson.DataDict;
        private string GetWudaoName(int wudaoId) => WuDaoAllType[wudaoId].name1;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            wudaoId = command.GetInt(1, 0);
            exp = command.GetInt(2, 0);
            if (npc > 0 && WuDaoAllType.ContainsKey(wudaoId) && exp >= 0)
            {
                MyLog.Log(command, $"角色ID:{npc} 角色名:{npc.GetNpcName()} ");
                MyLog.Log(command, $"悟道名字:{GetWudaoName(wudaoId)} 悟道ID:{wudaoId} 设置经验值:{exp}");
                WuDaoUtils.SetWudaoExp(npc, wudaoId, exp);
            }
            else
            {
                MyLog.Log(command, $"角色ID:{npc} 不能为小于等于 0", true);
                if (!WuDaoAllType.ContainsKey(wudaoId))
                {
                    MyLog.Log(command, $"悟道ID:{wudaoId} 不存在,建议查看WuDaoAllTypeJson.json文件", true, false);
                }

                if (exp < 0)
                {
                    MyLog.Log(command, $"设置经验值:{exp} 不能为小于 0", true, false);
                }
       
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}