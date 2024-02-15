using System;
using HarmonyLib;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension;

[RegisterCommand]
[DialogEvent("SetSummonMonstar")]
[DialogEvent("召唤角色")]
public class SetSummonMonstar : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        // NPCEx.GetJsonString()
        var npc = command.ToNpcId();


        SummonUtils.CreateInstantiate(npc);
        callback?.Invoke();
    }
}