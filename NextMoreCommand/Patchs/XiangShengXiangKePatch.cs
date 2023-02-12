using System.Collections.Generic;
using HarmonyLib;

namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(Tools), nameof(Tools.GetXiangSheng))]
[HarmonyPatch(typeof(Tools), nameof(Tools.GetXiangKe))]
public static class XiangShengXiangKePatch
{
    public static void Postfix(ref Dictionary<int, int> __result)
    {
        __result[5] = 5;
    }
}