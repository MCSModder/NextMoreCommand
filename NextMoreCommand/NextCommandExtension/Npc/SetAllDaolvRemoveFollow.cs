using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetAllDaolvRemoveFollow")]
    [DialogEvent("设置所有道侣取消跟随")]
    public class SetAllDaolvRemoveFollow : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var daolvId = PlayerEx.Player.DaoLvId.ToList().ToArray();
            NpcUtils.RemoveNpcFollow(daolvId);
            callback?.Invoke();
        }
    }
}