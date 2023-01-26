using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using JSONClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.Extension;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Custom;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs
{
    [HarmonyPatch(typeof(NpcJieSuanManager), "InitCyData")]
    public static class NpcJieSuanManagerPatch
    {
        public static void Prefix(NpcJieSuanManager __instance)
        {

            Next.Main.LogInfo("释放TempFlowchart里面内容");
            TempFlowchart.Flowcharts.Clear();
            FungusUtils.Flowcharts.Clear();
        }
    }
}