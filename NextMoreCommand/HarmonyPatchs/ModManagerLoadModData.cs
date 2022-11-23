using System;
using System.Collections.Generic;
using System.IO;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.Extension;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.CustomMap;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

[HarmonyPatch(typeof(ModManager), "LoadModData")]
public static class ModManagerLoadModData
{
    public static void Postfix(ModConfig modConfig)
    {
        var modNDataDirDir = modConfig.GetNDataDir();
        var customMapDatapath = Path.Combine(modNDataDirDir, "CustomMapData");
        var customMapTypepath = Path.Combine(modNDataDirDir, "CustomMapType");
        var hasPath = Directory.Exists(customMapTypepath) || Directory.Exists(customMapDatapath);
        if (hasPath)
        {
            Main.LogInfo($"=================== 地图Mod初始化 =====================");
        }


        try
        {
            LoadCustomMapData(modNDataDirDir);
            LoadCustomMapType(modNDataDirDir);
            //LoadCustomMapType(modNDataDirDir);
        }
        catch (Exception)
        {
            modConfig.State = ModState.LoadFail;
            throw;
        }

        modConfig.State = ModState.LoadSuccess;
        Main.LogIndent = 0;
        if (hasPath)
        {
            Main.LogInfo($"=================== 地图Mod结束加载 =====================");
        }
    }

    public static void LoadCustomMapData(string modDir)
    {
        var path = Path.Combine(modDir, "CustomMapData");
        if (!Directory.Exists(path)) return;
        foreach (var filePath in Directory.GetFiles(path, "*.json", SearchOption.AllDirectories))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                JArray.Parse(json).ToObject<List<CustomMapData>>()?.ForEach(TryCustomMapData);
                Main.LogInfo(string.Format("ModManager.LoadData".I18N(),
                    $"{path}/{Path.GetFileNameWithoutExtension(filePath)}.json"));
            }
            catch (Exception e)
            {
                throw new ModLoadException(string.Format("CustomMapData {0} 加载失败。".I18NTodo(), filePath), e);
            }
        }
    }

    private static void TryCustomMapData(CustomMapData customMapData)
    {
        customMapData.InitHighAndWide();
        customMapData.InitMapTypes();
        CustomMapManager.CustomMapDatas[customMapData.ID] = customMapData;
    }

    public static void LoadCustomMapType(string modDir)
    {
        var path = Path.Combine(modDir, "CustomMapType");
        if (!Directory.Exists(path)) return;
        foreach (var filePath in Directory.GetFiles(path, "*.json", SearchOption.AllDirectories))
        {
            try
            {
                string json = File.ReadAllText(filePath);
               var list = JsonConvert.DeserializeObject<List<ModCustomMapType>>(json,new ModCustomMapTypeConverter());
               list?.ForEach(TryCustomMapType);
                Main.LogInfo(string.Format("ModManager.LoadData".I18N(),
                    $"{path}/{Path.GetFileNameWithoutExtension(filePath)}.json"));
            }
            catch (Exception e)
            {
                throw new ModLoadException(string.Format("CustomMapType {0} 加载失败。".I18NTodo(), filePath), e);
            }
        }
    }

    private static void TryCustomMapType(ModCustomMapType customMapType)
    {
        CustomMapManager.CustomMapType[customMapType.ID] = customMapType;
        CustomMapManager.ChineseCustomMapType[customMapType.Cid] = customMapType.ID;
    }
}