using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GUIPackage;
using HarmonyLib;
using Newtonsoft.Json;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.NextSeachNpcExtension;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Patchs
{


  



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