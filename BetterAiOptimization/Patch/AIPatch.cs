using BetterAiOptimization.Data;
using HarmonyLib;
using KBEngine;

namespace BetterAiOptimization.Patch
{
    [HarmonyPatch(typeof(AI))]
    public static class AIPatch
    {
        [HarmonyPatch(nameof(AI.getSkillWeight))]
        [HarmonyPrefix]
        public static bool GetSkillWeightPrefixPatch(int skillId, ref int __result, ref AI __instance)
        {
            if (!AiSkillData.TryGetValue(__instance, skillId, out var aiSkillData)) return true;
            aiSkillData.OrderSkill();
            __result = aiSkillData.Weight;
            return false;
        }
    }
}