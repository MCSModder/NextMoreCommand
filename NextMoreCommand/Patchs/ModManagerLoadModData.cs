using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.Extension;
using SkySwordKill.Next.Mod;
using SkySwordKill.Next.Res;
using SkySwordKill.NextMoreCommand.Custom.NPC;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.CustomModData;


namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(ModManager), "LoadModData")]
public static class ModManagerLoadModData
{

    public static void Prefix(ModConfig modConfig)
    {
        CustomModDataManager.Read(modConfig);
    }

}