using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [RegisterCommand]
    [DialogEvent("SetNpcFightSpine")]
    [DialogEvent("设置角色战斗骨骼")]
    public class SetNpcFightSpine : IDialogEvent
    {
        private int npc;
        private bool show;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            show = command.GetBool(1, false);
            if (npc > 0)
            {

                NpcUtils.SetNpcFightSpine(npc, show);
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