using HarmonyLib;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(BiaoBaiManager), "CalcBiaoBaiScore")]
    static class BiaoBaiManagerCalcBiaoBaiScorePatch
    {
        private static string[] _triggerTypes = { "计算表白分数", "CalcBiaoBaiScore" };
        public static void Postfix()
        {
            if (DialogAnalysis.TryTrigger(_triggerTypes, null, true))
            {
                MyLog.Log("计算表白分数触发器", "触发计算表白分数");
            }

        }
    }
}