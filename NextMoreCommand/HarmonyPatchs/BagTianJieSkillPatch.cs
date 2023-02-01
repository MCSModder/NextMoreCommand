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
        if (miShu.Type <= 2) return true;
        __instance.MiShu = miShu;
        __instance.BindSkill = new ActiveSkill();
        __instance.BindSkill.SetSkill(miShu.Skill_ID, Player.getLevelType());
        if (TianJieYiLingWuSkills.StringListContains(__instance.MiShu.id))
        {
            __instance.IsLingWu = true;
        }

        var result = false;
        switch (miShu.Type)
        {
            case 3:
                result = DialogAnalysis.GetInt(__instance.MiShu.PanDing) == 1;
                break;
            case 4:
                result = DialogAnalysis.CheckCondition(__instance.MiShu.PanDing);
                break;
        }
        if (!__instance.IsLingWu && result)
        {
            __instance.IsCanLingWu = true;
        }
        return false;
    }
}

