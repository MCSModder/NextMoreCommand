using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension;

[DialogEvent("SetAllDaolvDeath")]
[DialogEvent("设置所有道侣死亡")]
public class SetAllDaolvDeath : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        var npcId = command.ToNpcListId().ToArray();
        MyLog.Log(command, $"所有道侣死亡 需要过滤角色ID列表:{JArray.FromObject(command.ParamList).ToString(Formatting.None)}");
        MyLog.Log(command, $"所有道侣死亡 需要过滤有效角色ID列表:{JArray.FromObject(npcId).ToString(Formatting.None)}");
        DaolvUtils.SetAllDaolvDeath(npcId);
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}