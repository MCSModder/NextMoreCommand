using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.Extension;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using Random = UnityEngine.Random;

namespace SkySwordKill.NextMoreCommand.CustomModData
{
    public class ChatInfo
    {
        [JsonProperty("id")]
        public string Id = string.Empty;
        [JsonProperty("condition")]
        public string Condition = string.Empty;
        [JsonProperty("event")]
        public List<string> Event = new List<string>();
        [JsonProperty("priority")]
        public int Priority = 0;
        [JsonProperty("bindNpc")]
        public int BindNpc = 0;
        [JsonIgnore]
        public ModConfig ModConfig = new ModConfig();
        public void Init()
        {
            Event = Event.Where(dialog => !string.IsNullOrWhiteSpace(dialog)).ToList();
        }
        public DialogEnvironment GetEnv(DialogEnvironment environment = null)
        {
            var env = environment ?? new DialogEnvironment()
            {
                mapScene = Tools.getScreenName()
            };
            if (BindNpc > 0)
            {
                DialogAnalysis.BindNpc(env, BindNpc);
            }

            return env;
        }
        public void RunEvent(DialogEnvironment environment = null)
        {
            var count = Event.Count;
            if (count == 0)
            {
                return;
            }
            var @event = count == 1 ? Event[0] : Event[Random.Range(0, Event.Count - 1)];
            var env = GetEnv(environment);
            if (DialogAnalysis.IsRunningEvent)
            {
                DialogAnalysis.SwitchDialogEvent(@event, env);
            }
            else
            {
                DialogAnalysis.StartDialogEvent(@event, env);
            }
        }
        public bool CheckCondition(DialogEnvironment environment = null)
        {
            if (!string.IsNullOrWhiteSpace(Condition))
            {
                return false;
            }
            var env = GetEnv(environment);
            return DialogAnalysis.CheckCondition(Condition, env);
        }
        public void SetBindNpc(int npcID)
        {
            BindNpc = npcID;
        }
    }

    public static class ChatRandomManager
    {
        public readonly static Dictionary<string, ChatInfo> ChatRandomInfo = new Dictionary<string, ChatInfo>();
        public static void RegisterChat(IEnumerable<ChatInfo> list)
        {
            foreach (var chat in list)
            {
                RegisterChat(chat);
            }
        }
        public static void RegisterChat(ChatInfo chatRandomInfo)
        {
            if (string.IsNullOrWhiteSpace(chatRandomInfo.Id))
            {
                return;
            }
            ChatRandomInfo[chatRandomInfo.Id] = chatRandomInfo;
        }
        public static void RegisterChat(string key, ChatInfo chatRandomInfo)
        {
            ChatRandomInfo[key] = chatRandomInfo;
        }
        public static ChatInfo GetRandomChatInfo()
        {
            if (ChatRandomInfo.Count == 0)
            {
                return null;
            }
            var chat = ChatRandomInfo.Values.OrderBy(info => -info.Priority).ToList();
            return chat[Random.Range(0, chat.Count - 1)];
        }
        public static List<ChatInfo> GetChatInfoList()
        {
            return ChatRandomInfo.Count == 0 ? new List<ChatInfo>() : ChatRandomInfo.Values.ToList();
        }
        public static void Clear()
        {
            ChatRandomInfo.Clear();
        }
    }

    [ModData("CustomRandomChat")]
    public class CustomRandomChat : IModData
    {
        public void Read(ModConfig modConfig)
        {
            CustomModDataManager.LoadData(modConfig.GetNDataDir(), "RandomChatExpand", modConfig, LoadCustomRandomChat);
        }
        public bool Check(ModConfig modConfig)
        {
            var modNData = modConfig.GetNDataDir();
            return Directory.Exists(modNData.CombinePath("RandomChatExpand"));
        }
        private static void LoadCustomRandomChat(string path, ModConfig modConfig)
        {
            
            foreach (var filePath in Directory.GetFiles(path, "*.json", SearchOption.AllDirectories))
            {
                try
                {
                    var list = JsonConvert.DeserializeObject<List<ChatInfo>>(File.ReadAllText(filePath)).Where(chat => !string.IsNullOrWhiteSpace(chat.Id)).ToList();
                    foreach (var info in list)
                    {
                        info.ModConfig = modConfig;
                        info.Init();
                    }
                    ChatRandomManager.RegisterChat(list);
                    MyPluginMain.LogInfo(string.Format("ModManager.LoadData".I18N(),
                        filePath));
                }
                catch (Exception e)
                {
                    throw new ModLoadException(string.Format("CustomSkillCombo {0} 加载失败。".I18NTodo(), filePath), e);
                }
            }
        }
    }
}