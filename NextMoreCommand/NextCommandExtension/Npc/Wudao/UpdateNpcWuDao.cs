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
    [DialogEvent("UpdateNpcWuDao")]
    [DialogEvent("更新角色悟道")]
    public class UpdateNpcWuDao : IDialogEvent
    {
        private int npc;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            if (npc > 0)
            {
                MyLog.Log(command, $"角色ID:{npc} 角色名:{npc.GetNpcName()} ");
                WuDaoUtils.UpdateNpcWuDao(npc);
      
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