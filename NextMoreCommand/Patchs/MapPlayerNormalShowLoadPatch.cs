using System.Collections.Generic;
using HarmonyLib;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(MapPlayerNormalShow), nameof(MapPlayerNormalShow.LoadSpine))]
    public static class MapPlayerNormalShowLoadPatch
    {
        private static List<string> _original = new List<string>()
        {
            "MapPlayerYuJian",
            "MapPlayerHeDianZu"
        };
        public static bool Prefix(MapPlayerNormalShow __instance)
        {
            var seid = __instance.NowDunShuSpineSeid;
            var spine = seid.Spine;
            if (_original.Contains(spine))
            {
                return true;
            }
            var traverse = Traverse.Create(__instance);
            var name = Tools.instance.getStaticSkillName(seid.skillid);
            var nowSpineName = traverse.Field<string>("nowSpineName");
            if (nowSpineName.Value == spine)
            {
                return false;
            }

            if (!AssetsUtils.GetCustomMapPlayerSpine(name, out var customMapPlayerSpine)) return false;
            nowSpineName.Value = spine;
            var playerSpine = __instance.PlayerSpine;
            var isNan = traverse.Field<MapPlayerController>("controller").Value.IsNan;
            playerSpine.skeletonDataAsset = customMapPlayerSpine.LoadSkeletonDataAsset(spine);
            playerSpine.initialSkinName = isNan ? "男" : "女";
            playerSpine.Initialize(true, false);
            return false;
        }
    }
}