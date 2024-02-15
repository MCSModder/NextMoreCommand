using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using XLua;

namespace SkySwordKill.NextMoreCommand.Patchs;

public delegate void OnTrigger(List<string> triggerTypes, DialogEnvironmentContext env);

[HarmonyPatch(typeof(DialogAnalysis), nameof(DialogAnalysis.TryTrigger))]
public static class DialogAnalysisTryTriggerPatch
{
    public static readonly List<OnTrigger> OnTryTrigger =
        new List<OnTrigger>();


    public static void Prefix(IEnumerable<string> triggerTypes,
        DialogEnvironment                         env)
    {
        var dialogEnvironment = env ?? new();
        var triggers          = triggerTypes.ToList();
        if (OnTryTrigger.Count != 0)
        {
            foreach (var trigger in OnTryTrigger)
            {

                trigger.Invoke(triggers, dialogEnvironment.GetDialogEnvironmentContext());
            }
        }
    }
}