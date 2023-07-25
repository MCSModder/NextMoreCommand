﻿using System.Collections.Generic;
using System.IO;
using HarmonyLib;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

[HarmonyPatch(typeof(YSNewSaveSystem), nameof(YSNewSaveSystem.AutoLoad))]
public static class YsNewSaveSystemLoadPatch
{
    public static bool Prefix()
    {
        var cgSpineManager = CGSpineManager.Instance;
        if (cgSpineManager.skeletonGraphicGameObject is null)
        {
            return true;
        }
        cgSpineManager.Reset();
        cgSpineManager.LoadSave();
        return false;
    }
    
    // public static JObject LoadData(int id, int index, string fileName)
    // {
    //     var savePath = Paths.GetNewSavePath();
    //     var pathPre = YSNewSaveSystem.GetAvatarSavePathPre(id, index);
    //     var path = $"{savePath}/{pathPre}/{fileName}";
    //     if (!File.Exists(path))
    //     {
    //         return null;
    //     }
    //
    //     var text = File.ReadAllText(path);
    //    MyPluginMain.LogInfo(text);
    //     return string.IsNullOrWhiteSpace(text)?null:JObject.Parse(text);
    // }
    //
    // public static void Postfix(int avatarIndex, int slot, int DFIndex)
    // {
    //     var data = LoadData(avatarIndex, slot, "nickname.json");
    //     if (data == null)
    //     {
    //         return;
    //     }
    //
    //     NpcUtils.SelfNameDict = data.ToObject<Dictionary<int, string>>();
    // }
}

// [HarmonyPatch(typeof(YSNewSaveSystem), nameof(YSNewSaveSystem.SaveGame))]
// public static class YsNewSaveSystemSavePatch
// {
//     public static void Prefix()
//     {
//         YSNewSaveSystem.Save("nickname.json", new JSONObject(JObject.FromObject(NpcUtils.SelfNameDict).ToString()));
//     }
// }