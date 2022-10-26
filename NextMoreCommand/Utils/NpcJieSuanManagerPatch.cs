using HarmonyLib;

namespace SkySwordKill.NextMoreCommand.Utils
{
    [HarmonyPatch(typeof(NpcJieSuanManager), "InitCyData")]
    public class NpcJieSuanManagerPatch
    {
        [HarmonyPrefix]
        public static void FixMethod(NpcJieSuanManager __instance)
        {
            Next.Main.LogInfo("释放TempFlowchart里面内容");
            TempFlowchart.Flowcharts.Clear();
        }
    }
}