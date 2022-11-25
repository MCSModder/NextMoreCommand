using Fungus;
using HarmonyLib;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;
[HarmonyPatch(typeof(CheckTaskIsOut),nameof(CheckTaskIsOut.OnEnter))]
public static class CheckTaskIsOutOnEnter
{
    public static void Postfix(CheckTaskIsOut __instance)
    {
        var instance = Traverse.Create(__instance);
        var result = instance.Field<BooleanVariable>("Result").Value;
        if (result.Value)
        {
          var env =  new DialogEnvironment();
          env.customData.Add("TaskID",instance.Field<IntegerVariable>("TaskId").Value.ToString());
          DialogAnalysis.TryTrigger(new[] { "任务过期", "TaskFinishTime" }, env, true);
        }
    }
}