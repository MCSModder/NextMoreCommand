using HarmonyLib;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.DialogTrigger;

[HarmonyPatch(typeof(UIBiGuanTuPoPanel), nameof(UIBiGuanTuPoPanel.TuPo))]
public static class OnBiGuanTuPo
{
    public static void Prefix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName(),
        };
        if (DialogAnalysis.TryTrigger(new[] { "闭关功法突破前", "BeforeBiGuanTuPo" }, env, true))
        {
            MyPluginMain.LogInfo("闭关功法突破前触发器");
        }
    }

    public static void Postfix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName()
        };
        if (DialogAnalysis.TryTrigger(new[] { "闭关功法突破后", "AfterBiGuanTuPo" }, env, true))
        {
            MyPluginMain.LogInfo("闭关功法突破后触发器");
        }
    }
}

[HarmonyPatch(typeof(UIBiGuanGanWuPanel), nameof(UIBiGuanGanWuPanel.GanWu))]
public static class OnBiGuanGanWu
{
    public static void Prefix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName(),
        };
        if (DialogAnalysis.TryTrigger(new[] { "闭关感悟思绪前", "BeforeBiGuanGanWu" }, env, true))
        {
            MyPluginMain.LogInfo("闭关感悟思绪前触发器");
        }
    }

    public static void Postfix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName()
        };
        if (DialogAnalysis.TryTrigger(new[] { "闭关感悟思绪后", "AfterBiGuanGanWu" }, env, true))
        {
            MyPluginMain.LogInfo("闭关感悟思绪后触发器");
        }
    }
}

[HarmonyPatch(typeof(UIBiGuanLingWuPanel), nameof(UIBiGuanLingWuPanel.ReadBook))]
public static class OnBiGuanLingWu
{
    public static void Prefix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName(),
        };
        if (DialogAnalysis.TryTrigger(new[] { "闭关领悟功法前", "BeforeBiGuanLingWu" }, env, true))
        {
            MyPluginMain.LogInfo("闭关领悟功法前触发器");
        }
    }

    public static void Postfix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName()
        };
        if (DialogAnalysis.TryTrigger(new[] { "闭关领悟功法后", "AfterBiGuanLingWu" }, env, true))
        {
            MyPluginMain.LogInfo("闭关领悟功法后触发器");
        }
    }
}