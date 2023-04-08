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
// using SkySwordKill.NextMoreCommand.Puerts;

namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(ModManager), "LoadModData")]
public static class ModManagerLoadModData
{
    public static bool HasPath(this string path) => Directory.Exists(path);
    public static string CombinePath(this string path, string folder) => Path.Combine(path, folder);

    public static bool HasDirectory(params string[] paths)
    {
        var result = false;
        foreach (var path in paths)
        {
            if (path.HasPath())
            {
                result = true;
            }
        }
        return result;
    }

    private static void CreateTemplateTest(string dir)
    {
        MyPluginMain.LogInfo("创建警花测试");
        var npc = new CustomNpc()
        {
            Id = 8000,
            Name = "天宫警花",
            Title = "天宫仙子",
            SexType = 2,
            CustomWujiang = new List<CustomWujiang>()
            {
                new CustomWujiang(),
                new CustomWujiang()
            },
            CustomNpcImportantDate = new CustomNpcImportantDate(),
            CustomBackPack = new CustomBackPack(),
        };
        npc.CustomNpcAvatar.Add(new CustomNpcAvatar());
        npc.CustomNpcAvatar.Add(new CustomNpcAvatar());
        npc.Init();
        //npc.Init();
        File.WriteAllText(dir.CombinePath("天宫警花.json"),
            npc.ToString());
    }
    // public static string[] JsExt = new[]
    // {
    //     ".js",
    //     ".cjs",
    //     ".mjs",
    // };
    // private static void LoadCustomJsData(ModConfig modConfig, string rootPath) => Main.Res.DirectoryHandle("", rootPath, (ResourcesManager.FileHandle)((virtualPath, filePath) =>
    // {
    //
    //     if (!JsExt.Contains(Path.GetExtension(filePath)))
    //         return;
    //     var jsFileCache = new JsFileCache()
    //     {
    //         FromMod = modConfig,
    //         FilePath = filePath.Replace("\\", "/")
    //     };
    //     var jsPath = Path.GetFileNameWithoutExtension(virtualPath).Replace("\\", "/");
    //     try
    //     {
    //         JsEnvManager.AddJsFileCache(jsPath, jsFileCache);
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new ModLoadException(string.Format("JavaScript {0} 加载失败。", (object)jsPath), ex);
    //     }
    // }));
    public static void Prefix(ModConfig modConfig)
    {
     
        var path = modConfig.Path;
        var isWorkshop = path.Contains("workshop\\content\\1189490");
        var modNDataDirDir = modConfig.GetNDataDir();

        // if (HasPath(path.CombinePath("JS")))
        // {
        //     MyPluginMain.LogInfo($"=================== NextMore开始加载 =====================");
        //
        //     LoadCustomJsData(modConfig, path.CombinePath("JS"));
        //     MyPluginMain.LogInfo($"=================== NextMore结束加载 =====================");
        //
        // }

        if (HasPath(modNDataDirDir.CombinePath("CustomNpc")) && !isWorkshop)
        {
            //if (modConfig.Name.Contains("天宫镜花")) CreateTemplateTest(modNDataDirDir.CombinePath("CustomNpc"));
            MyPluginMain.LogInfo($"=================== NextMore开始生成 =====================");
            Main.LogIndent = 2;
            try
            {
                LoadCustomNpcData(modNDataDirDir, modConfig);
            }
            catch (Exception)
            {
                modConfig.State = ModState.LoadFail;
                throw;
            }

            modConfig.State = ModState.LoadSuccess;
            Main.LogIndent = 0;


            MyPluginMain.LogInfo($"=================== NextMore结束生成 =====================");
        }

        if (HasPath(modNDataDirDir.CombinePath("CustomSkillCombo")))
        {
            MyPluginMain.LogInfo($"=================== NextMore开始加载 =====================");
            Main.LogIndent = 2;
            try
            {
                LoadCustomSkillComboData(modNDataDirDir, modConfig);
            }
            catch (Exception)
            {
                modConfig.State = ModState.LoadFail;
                throw;
            }

            modConfig.State = ModState.LoadSuccess;
            Main.LogIndent = 0;


            MyPluginMain.LogInfo($"=================== NextMore结束加载 =====================");
        }
    }

    public static void LoadData(string modDir, string folder, ModConfig modConfig, Action<string, ModConfig> onComplete)
    {
        if (HasPath(modDir.CombinePath(folder)))
        {
            onComplete.Invoke(modDir.CombinePath(folder), modConfig);
        }
    }

    // ReSharper disable once HeapView.DelegateAllocation
    public static void LoadCustomNpcData(string modDir, ModConfig modConfig) =>
        LoadData(modDir, "CustomNpc", modConfig, LoadCustomNpc);

    public static void LoadCustomSkillComboData(string modDir, ModConfig modConfig) =>
        LoadData(modDir, "CustomSkillCombo", modConfig, LoadCustomSkillCombo);

    private static void LoadCustomSkillCombo(string path, ModConfig modConfig)
    {
        foreach (var filePath in Directory.GetFiles(path, "*.json", SearchOption.AllDirectories))
        {
            try
            {
                var json = File.ReadAllText(filePath);

                var skill = JObject.Parse(json)?.ToObject<SkillCombo>();
                if (skill == null)
                {
                    continue;
                }

                skill.Init();
                SkillComboManager.SkillCombos[skill.SkillName] = skill;
                MyPluginMain.LogInfo(string.Format("ModManager.LoadData".I18N(),
                    filePath));
            }
            catch (Exception e)
            {
                throw new ModLoadException(string.Format("CustomSkillCombo {0} 加载失败。".I18NTodo(), filePath), e);
            }
        }
    }

    public static void LoadCustomNpc(string path, ModConfig modConfig)
    {
        var customNpcs = new Dictionary<string, CustomNpc>();

        foreach (var filePath in Directory.GetFiles(path, "*.json", SearchOption.AllDirectories))
        {
            try
            {
                string json = File.ReadAllText(filePath);

                var npc = JObject.Parse(json)?.ToObject<CustomNpc>();
                if (npc == null) continue;
                if (npc.Id >= 20000)
                {
                    Main.LogWarning($"NpcId:{npc.Id.ToString()} 已经超过2万值 建议改小2万以内值");
                    continue;
                }

                var id = npc.Id.ToString();
                customNpcs[id] = npc;
                CustomNpc.CustomNpcs[id] = npc;
                MyPluginMain.LogInfo(string.Format("ModManager.LoadData".I18N(),
                    filePath));
            }
            catch (Exception e)
            {
                throw new ModLoadException(string.Format("CustomNpc {0} 加载失败。".I18NTodo(), filePath), e);
            }
        }

        SaveCustomNpc(customNpcs, modConfig);
    }

    delegate void SaveJsonAction(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict);

    private static void SaveCustomNpc(Dictionary<string, CustomNpc> customNpcs, ModConfig modConfig)
    {
        if (customNpcs.Count == 0)
        {
            return;
        }

        var modData = modConfig.GetDataDir();
        SaveJson(modData.CombinePath("AvatarJsonData.json"), customNpcs, SaveAvatar);
        SaveJson(modData.CombinePath("NPCImportantDate.json"), customNpcs, SaveNpcImportant);
        SaveJson(modData.CombinePath("WuJiangBangDing.json"), customNpcs, SaveWuJiang);
        SaveJson(modData.CombinePath("BackpackJsonData.json"), customNpcs, SaveBackPack);
    }

    private static void SaveAvatar(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict)
    {
        if (npc.Value.CustomNpcAvatar.Count == 0)
        {
            return;
        }

        var list = npc.Value.ToAvatarJsonDataList();
        foreach (var item in list)
        {
            dict[item.Id.ToString()] = item.ToJObject();
        }
    }

    private static void SaveNpcImportant(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict)
    {
        var value = npc.Value.ToNpcImportantDate();
        if (value != null)
        {
            dict[npc.Key] = value;
        }
    }


    private static void SaveWuJiang(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict)
    {
        var count = npc.Value.CustomWujiang?.Count;
        if (count is 0 or null)
        {
            return;
        }

        foreach (var value in npc.Value.ToWuJiangBindingList())
        {
            dict[value.Id.ToString()] = value.ToJObject();
        }
    }

    private static void SaveBackPack(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict)
    {
        var value = npc.Value.ToBackPack();
        if (value != null)
        {
            dict[npc.Key] = value;
        }
    }

    private static void SaveJson(string filename, Dictionary<string, CustomNpc> customNpcs, SaveJsonAction func)
    {
        var dictionary = new Dictionary<string, JObject>();
        foreach (var npc in customNpcs)
        {
            func?.Invoke(npc, dictionary);
        }

        if (dictionary.Count == 0)
        {
            return;
        }

        ;

        if (File.Exists(filename))
        {
            var jObject = JObject.Parse(File.ReadAllText(filename));
            var file = jObject.ToObject<Dictionary<string, JObject>>();
            if (file != null)
            {
                // MyPluginMain.LogInfo(jObject);

                foreach (var item in file)
                {
                    if (!dictionary.ContainsKey(item.Key))
                    {
                        //  MyPluginMain.LogInfo(item.Value);
                        dictionary.Add(item.Key, item.Value);
                    }
                }
            }
        }

        var json = JObject.FromObject(dictionary).ToString(Formatting.Indented);
        File.WriteAllText(filename, json);
    }
}