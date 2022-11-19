using HarmonyLib;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs
{
    [HarmonyPatch(typeof(NpcJieSuanManager), "InitCyData")]
    public static class NpcJieSuanManagerPatch
    {
        public static void Prefix()
        {
            Next.Main.LogInfo("释放TempFlowchart里面内容");
            TempFlowchart.Flowcharts.Clear();
            if (FungusUtils.isActive)
            {
                FungusUtils.InitFlowchart();
            }
        }
    }
}