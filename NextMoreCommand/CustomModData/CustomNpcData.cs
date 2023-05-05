using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.Extension;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.NPC;

namespace SkySwordKill.NextMoreCommand.CustomModData
{
    [ModData("CustomNpc")]
    public class CustomNpcData : IModData
    {

        public void Read(ModConfig modConfig)
        {
            CustomModDataManager.LoadData(modConfig.GetNDataDir(), "CustomNpc", modConfig, LoadCustomNpc);
        }
        public bool Check(ModConfig modConfig)
        {
            var path = modConfig.Path;
            var modNDataDir = modConfig.GetNDataDir();
            return !path.Contains("workshop\\content\\1189490") && modNDataDir.CombinePath("CustomNpc").HasPath();
        }


        public void LoadCustomNpc(string path, ModConfig modConfig)
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
        private void SaveCustomNpc(Dictionary<string, CustomNpc> customNpcs, ModConfig modConfig)
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

        private void SaveNpcImportant(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict)
        {
            var value = npc.Value.ToNpcImportantDate();
            if (value != null)
            {
                dict[npc.Key] = value;
            }
        }

        delegate void SaveJsonAction(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict);


        private void SaveAvatar(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict)
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

        private void SaveWuJiang(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict)
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

        private void SaveBackPack(KeyValuePair<string, CustomNpc> npc, Dictionary<string, JObject> dict)
        {
            var value = npc.Value.ToBackPack();
            if (value != null)
            {
                dict[npc.Key] = value;
            }
        }

        private void SaveJson(string filename, Dictionary<string, CustomNpc> customNpcs, SaveJsonAction func)
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
}