using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc.Teleport
{
    [RegisterCommand]
    [DialogEvent("SetAllDaolvRemoveFollow")]
    [DialogEvent("设置所有道侣取消跟随")]
    public class SetAllDaolvRemoveFollow : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            var daolvId = PlayerEx.Player.DaoLvId.ToList().ToArray();
            MyLog.Log(command, $"开始执行所有道侣取消跟随 角色ID列表:{JArray.FromObject(daolvId).ToString(Formatting.None)}");
            NpcUtils.RemoveNpcFollow(daolvId);
            MyLog.LogCommand(command,false);
            callback?.Invoke();
        }
    }
}