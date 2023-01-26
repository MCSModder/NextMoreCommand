using System;
using Bag;
using HarmonyLib;
using JSONClass;
using KBEngine;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

[HarmonyPatch(typeof(BagTianJieSkill), MethodType.Constructor, new Type[] { typeof(TianJieMiShuData) })]
public static class BagTianJieSkillPatch
{
    public static Avatar Player => PlayerEx.Player;
    public static JSONObject TianJieYiLingWuSkills => Player.TianJieYiLingWuSkills;
    public static bool Prefix(ref BagTianJieSkill __instance, TianJieMiShuData miShu)
    {
        if (miShu.Type != 3) return true;
        __instance.MiShu = miShu;
        __instance.BindSkill = new ActiveSkill();
        __instance.BindSkill.SetSkill(miShu.Skill_ID, Player.getLevelType());
        if (TianJieYiLingWuSkills.StringListContains(__instance.MiShu.id))
        {
            __instance.IsLingWu = true;
        }

        if (!__instance.IsLingWu && DialogAnalysis.GetInt(__instance.MiShu.PanDing) == 1)
        {
            __instance.IsCanLingWu = true;
        }
        return false;
    }
}

