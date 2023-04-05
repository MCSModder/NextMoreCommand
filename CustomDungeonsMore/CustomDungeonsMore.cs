using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UniqueCream.CustomDungeons.NextSupport.NextEvent.DungeonEvent;

namespace Zerxz.CustomDungeonsMore;

[BepInDependency("UniqueCream.CustomDungeons", BepInDependency.DependencyFlags.HardDependency)]
[BepInPlugin("zerxz.plugin.CustomDungeonsMore", "CustomDungeonsMore", "1.0.0")]
public class CustomDungeonsMore : BaseUnityPlugin
{
    private Harmony _harmony;
    private void Awake()
    {
        _harmony = new Harmony("SubscribeDependencies");
        _harmony.PatchAll();

    }
    private void OnDestroy()
    {
        _harmony.UnpatchAll();
    }
}

[HarmonyPatch(typeof(Dungeon_ShowExit), (nameof(Dungeon_ShowExit.Execute)))]
public static class DungeonShowExitExecutePatch
{
    private static List<string> _original = new List<string>()
    {
        "MapPlayerYuJian",
        "MapPlayerHeDianZu",
        "MapPlayerWalk"
    };
    public static void Postfix()
    {
        var normalShow = MapPlayerController.Inst.NormalShow;
        var nowSpineName = Traverse.Create(normalShow).Field<string>("nowSpineName");
        if (_original.Contains(nowSpineName.Value))
        {
            return;
        }
        nowSpineName.Value = "";
        normalShow.Refresh();
    }

}