using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Teleport
{
    [RegisterCommand]
    [DialogEvent("SetNpcRemoveFollow")]
    [DialogEvent("设置角色取消跟随")]
    public class SetNpcRemoveFollow : IDialogEvent
    {
        private int[] npcList;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);

            npcList = command.ToNpcListId().ToArray();
            MyLog.Log(command, $"开始执行角色取消跟随 角色ID列表:{JArray.FromObject(command.ParamList).ToString(Formatting.None)}");
            MyLog.Log(command, $"开始执行角色取消跟随 有效角色ID列表:{JArray.FromObject(npcList).ToString(Formatting.None)}");
            NpcUtils.RemoveNpcFollow(npcList);
            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}