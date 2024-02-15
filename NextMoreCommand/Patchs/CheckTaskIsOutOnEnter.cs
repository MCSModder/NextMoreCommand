using Fungus;
using HarmonyLib;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(CheckTaskIsOut), nameof(CheckTaskIsOut.OnEnter))]
public static class CheckTaskIsOutOnEnter
{
    private static string[] _triggerTypes = { "任务过期", "TaskFinishTime" };
    public static void Postfix(CheckTaskIsOut __instance)
    {
        var instance = Traverse.Create(__instance);
        var result   = instance.Field<BooleanVariable>("Result").Value;
        if (result.Value)
        {
            var env = new DialogEnvironment();
            env.customData.Add("TaskID", instance.Field<IntegerVariable>("TaskId").Value.ToString());
            DialogAnalysis.TryTrigger(_triggerTypes, env, true);
        }
    }
}