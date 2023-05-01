using BepInEx;
using HarmonyLib;
using UniqueCream.CustomDungeons.NextSupport.NextEvent.DungeonEvent;

namespace Zerxz.BetterSeidOptimization;


[BepInPlugin("zerxz.plugin.BetterSeidOptimization", "BetterSeidOptimization", "1.0.0")]
public class BetterSeidOptimization : BaseUnityPlugin
{
    private Harmony _harmony;
    public static BetterSeidOptimization Inst;
    private void Awake()
    {
        Inst = this;
        _harmony = new Harmony("zerxz.plugin.BetterSeidOptimization");
        _harmony.PatchAll();

    }
    private void OnDestroy()
    {
        _harmony.UnpatchAll();
    }
}
