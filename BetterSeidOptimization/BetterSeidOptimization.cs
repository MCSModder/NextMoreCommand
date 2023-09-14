using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using SkySwordKill.Next.Mod;

namespace Zerxz.BetterSeidOptimization;

[BepInPlugin("zerxz.plugin.BetterSeidOptimization", "BetterSeidOptimization", "1.0.0")]
public class BetterSeidOptimization : BaseUnityPlugin
{
    private Harmony _harmony;
    public static BetterSeidOptimization Inst;
    public static bool BetterMode => _betterMode.Value;
    private static ConfigEntry<bool> _betterMode;
    private void Awake()
    {
        _betterMode = Config.Bind("开启优化模式", "Seid优化", false, "");
        Inst = this;
        _harmony = new Harmony("zerxz.plugin.BetterSeidOptimization");
        _harmony.PatchAll();
        ModManager.ModLoadComplete += ModLoadComplete;
        ModManager.ModSettingChanged += ModSettingChanged;

    }
    private void InitSettingAction()
    {
        _betterMode.SettingChanged += (sender, args) =>
        {
            ModManager.SetModSetting("NMC_BetterSeidMode", _betterMode.Value);
        };

    }
    private void ModSettingChanged()
    {
        if (ModManager.TryGetModSetting("NMC_BetterSeidMode", out bool betterSeid))
        {
            _betterMode.Value = betterSeid;
        }

    }
    private void ModLoadComplete()
    {
        ModManager.SetModSetting("NMC_BetterSeidMode", _betterMode.Value);

    }
    private void OnDestroy()
    {
        _harmony.UnpatchAll();
    }
}