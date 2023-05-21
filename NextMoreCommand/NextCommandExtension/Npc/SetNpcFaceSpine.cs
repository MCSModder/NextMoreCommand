using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [RegisterCommand]
    [DialogEvent("SetNpcFaceSpine")]
    [DialogEvent("设置立绘战斗骨骼")]
    public class SetNpcFaceSpine : IDialogEvent
    {
        private int npc;
        private string value;
        private bool show;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            value = command.GetStr(1);
            show = command.GetBool(2);
            if (npc > 0)
            {

                NpcUtils.SetNpcFaceSpine(npc, value);
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