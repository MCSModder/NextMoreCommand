using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [RegisterCommand]
    [DialogEvent("SetNpcDefaultSkinSpine")]
    [DialogEvent("设置角色默认皮肤骨骼")]
    public class SetNpcDefaultSkinSpine : IDialogEvent
    {
        private int    npc;
        private string skin;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            skin = command.GetStr(1);
            if (npc > 0)
            {

                NpcUtils.SetNpcDefaultSkinSpine(npc, skin);
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