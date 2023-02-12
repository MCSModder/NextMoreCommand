using HarmonyLib;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Beta;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(NpcJieSuanManager), "InitCyData")]
    public static class NpcJieSuanManagerPatch
    {
        public static void Prefix(NpcJieSuanManager __instance)
        {
            MyPluginMain.LogInfo("释放TempFlowchart里面内容");
            TempFlowchart.Flowcharts.Clear();
            FungusUtils.Flowcharts.Clear();
            if (CustomSceneAssetBundle.CustomSceneAssetBundles.Count != 0)
            {
                foreach (var customSceneAssetBundle in CustomSceneAssetBundle.CustomSceneAssetBundles)
                {
                    customSceneAssetBundle.Value.Unload();
                }

                CustomSceneAssetBundle.CustomSceneAssetBundles.Clear();
            }
        }
    }
}