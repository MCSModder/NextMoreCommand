using System;
using System.Collections.Generic;
using JSONClass;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Wudao
{
    [RegisterCommand]
    [DialogEvent("AddNpcWudaoSkill")]
    [DialogEvent("增加角色悟道技能")]
    public class AddNpcWudaoSkill : IDialogEvent
    {
        private int npc;
        private int wudaoSkill;
        private Dictionary<int, WuDaoAllTypeJson> WuDaoAllType => WuDaoAllTypeJson.DataDict;
        private string GetWudaoName(int wudaoId) => WuDaoAllType[wudaoId].name1;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            wudaoSkill = command.GetInt(1, 0);
            if (npc > 0)
            {
                MyLog.Log(command, $"角色ID:{npc} 角色名:{npc.GetNpcName()} ");
                MyLog.Log(command, $"悟道技能ID:{wudaoSkill} ");
                WuDaoUtils.AddNpcWuDao(npc, wudaoSkill);
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