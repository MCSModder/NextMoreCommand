using System;
using System.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetNpcRemoveFollow")]
    [DialogEvent("设置角色取消跟随")]
    public class SetNpcRemoveFollow : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npcList = command.ParamList.Where(item => item.ToNpcId() > 0).Select(item => item.ToNpcId()).ToArray();
            NpcUtils.RemoveNpcFollow(npcList);
            callback?.Invoke();
        }
    }
}