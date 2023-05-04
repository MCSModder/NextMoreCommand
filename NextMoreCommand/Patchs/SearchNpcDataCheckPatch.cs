using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand.NextSeachNpcExtension;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(Main), "SearchNpc")]
    public static class MainSearchNpcPatch
    {
        public static void Postfix()
        {
            SearchNpcDataInfo.Clear();
        }
    }
  



    [HarmonyPatch(typeof(SearchNpcData), nameof(SearchNpcData.Check))]
    public static class SearchNpcDataCheckPatch
    {

        public static bool Prefix(string matchStr, SearchNpcData __instance, ref bool __result)
        {
           // MyLog.Log("SearchNpcDataCheckPatch", $"matchStr:{matchStr}");
            var npcJson = __instance.ID.NPCJson();
            if (!matchStr.Contains(":") || npcJson == null)
            {
                return true;
            }
            var searchNpcDataInfo = SearchNpcDataInfo.Inst;
            searchNpcDataInfo.SetSearchNpcDataInfo(matchStr,__instance);
            if (!SearchNpcDataManager.Match(searchNpcDataInfo))
            {
                searchNpcDataInfo = null;
                return true;
            }
            __result = true;
            return false;
        }

    }
}