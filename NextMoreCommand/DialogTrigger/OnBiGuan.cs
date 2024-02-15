using HarmonyLib;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.DialogTrigger;

[HarmonyPatch(typeof(UIBiGuanTuPoPanel), nameof(UIBiGuanTuPoPanel.TuPo))]
public static class OnBiGuanTuPo
{
    private static string[] _triggerTypeBeforeBiGuanTuPo = { "闭关功法突破前", "BeforeBiGuanTuPo" };
    public static void Prefix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName(),
        };
        if (DialogAnalysis.TryTrigger(_triggerTypeBeforeBiGuanTuPo, env, true))
        {
            MyPluginMain.LogInfo("闭关功法突破前触发器");
        }
    }

    private static string[] _triggerTypeAfterBiGuanTuPo = { "闭关功法突破后", "AfterBiGuanTuPo" };
    public static void Postfix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName()
        };
        if (DialogAnalysis.TryTrigger(_triggerTypeAfterBiGuanTuPo, env, true))
        {
            MyPluginMain.LogInfo("闭关功法突破后触发器");
        }
    }
}

[HarmonyPatch(typeof(UIBiGuanGanWuPanel), nameof(UIBiGuanGanWuPanel.GanWu))]
public static class OnBiGuanGanWu
{
    private static string[] _triggerTypeBeforeBiGuanGanWu = { "闭关感悟思绪前", "BeforeBiGuanGanWu" };
    public static void Prefix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName(),
        };
        if (DialogAnalysis.TryTrigger(_triggerTypeBeforeBiGuanGanWu, env, true))
        {
            MyPluginMain.LogInfo("闭关感悟思绪前触发器");
        }
    }
    private static string[] _triggerTypeAfterBiGuanGanWu = { "闭关感悟思绪后", "AfterBiGuanGanWu" };
    public static void Postfix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName()
        };
        if (DialogAnalysis.TryTrigger(_triggerTypeAfterBiGuanGanWu, env, true))
        {
            MyPluginMain.LogInfo("闭关感悟思绪后触发器");
        }
    }
}

[HarmonyPatch(typeof(UIBiGuanLingWuPanel), nameof(UIBiGuanLingWuPanel.ReadBook))]
public static class OnBiGuanLingWu
{
    private static string[] _triggerTypeBeforeBiGuanLingWu = { "闭关领悟功法前", "BeforeBiGuanLingWu" };
    public static void Prefix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName(),
        };
        if (DialogAnalysis.TryTrigger(_triggerTypeBeforeBiGuanLingWu, env, true))
        {
            MyPluginMain.LogInfo("闭关领悟功法前触发器");
        }
    }
    private static  string[] _triggerTypeAfterBiGuanLingWu = { "闭关领悟功法后", "AfterBiGuanLingWu" };

    public static void Postfix()
    {
        var env = new DialogEnvironment()
        {
            mapScene = Tools.getScreenName()
        };
        if (DialogAnalysis.TryTrigger(_triggerTypeAfterBiGuanLingWu, env, true))
        {
            MyPluginMain.LogInfo("闭关领悟功法后触发器");
        }
    }
}