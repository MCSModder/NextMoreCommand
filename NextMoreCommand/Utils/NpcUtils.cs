using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using UnityEngine.Events;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public struct NpcInfo
    {
        public string Dialog;
        public int Id;
        public string Name => Id.GetNpcName();

        public NpcInfo(int id, string dialog = "")
        {
            Dialog = dialog;
            Id = id;
        }

        public string GetDialogName() => string.IsNullOrWhiteSpace(Dialog) ? "无" : Dialog;
    }

    public static class ExtendsNpc
    {
        public static int ToNpcId(this string instance)
        {
            return NPCEx.NPCIDToNew(Convert.ToInt32(instance));
        }

        public static string GetNpcName(this int instance)
        {
            return DialogAnalysis.GetNpcName(instance.ToNpcNewId());
        }

        public static string ToNpcId(this int instance)
        {
            return instance.ToNpcNewId().ToString();
        }

        public static int ToNpcNewId(this int instance)
        {
            return NPCEx.NPCIDToNew(instance);
        }

        public static List<int> ToNpcListId(this DialogCommand instance, int index = 0)
        {
            if (instance.ParamList.Length >= index)
            {
                return new List<int>();
            }

            var list = new List<string>();

            for (var i = index; i < instance.ParamList.Length; i++)
            {
                list.Add(instance.ParamList[i]);
            }

            return list.Select(item => item.ToNpcId()).Where(item => item > 0).ToList();
        }

        public static List<int> ToListInt(this DialogCommand instance, int index = 0)
        {
            if (instance.ParamList.Length <= index)
            {
                return new List<int>();
            }

            var list = new List<string>();

            for (var i = index; i < instance.ParamList.Length; i++)
            {
                list.Add(instance.ParamList[i]);
            }

            return list.Select(item => Convert.ToInt32(item)).ToList();
        }

        public static List<string> ToListString(this DialogCommand instance, int index = 0)
        {
            var list = new List<string>();
            if (instance.ParamList.Length <= index)
            {
                return list;
            }


            for (var i = index; i < instance.ParamList.Length; i++)
            {
                list.Add(instance.ParamList[i]);
            }

            return list;
        }

        public static int ToNpcId(this DialogCommand instance, int index = 0, int value = 0)
        {
            return instance.GetInt(index, value).ToNpcNewId();
        }

        public static bool HasNpcFollowGroup(this DataGroup<string> instance)
        {
            return instance.HasGroup(NpcUtils.NpcFollow);
        }

        public static Dictionary<string, string> GetNpcFollowGroup(this DataGroup<string> instance)
        {
            return instance.GetGroup(NpcUtils.NpcFollow);
        }

        public static void SetNpcFollow(this DataGroup<string> instance, string key, string value)
        {
            instance.Set(NpcUtils.NpcFollow, key, value);
        }

        public static string GetNpcFollow(this DataGroup<string> instance, string key)
        {
            return instance.Get(NpcUtils.NpcFollow, key);
        }
    }

    public static class NpcUtils
    {
        public static Dictionary<int, string> SelfNameDict = new Dictionary<int, string>();
        public static JSONObject AvatarRandomJsonData => jsonData.instance.AvatarRandomJsonData;
        public static JSONObject AvatarJsonData => jsonData.instance.AvatarJsonData;
        public static bool IsNpc(int id) => NPCEx.NPCIDToNew(id) <= 1;
        public static bool IsNpc(string id) => IsNpc(Convert.ToInt32(id));
        public const string NpcFollow = "NPC_FOLLOW_NEXT";
        public static DataGroup<string> StrGroup => DialogAnalysis.AvatarNextData.StrGroup;
        public static Dictionary<string, string> NpcFollowGroup => StrGroup.GetNpcFollowGroup();
        public static bool IsFightScene => Tools.getScreenName().ToUpper().Contains("YSNEW");

        public static void AddNpcFollow()
        {
            if (NpcFollowGroup.Count == 0)
            {
                return;
            }

            MyPluginMain.LogInfo("开始添加npc跟随");
            var isAdd = false;
            foreach (var key in NpcFollowGroup)
            {
                AddNpc(new NpcInfo(key.Key.ToNpcId(), key.Value), out isAdd);
            }

            if (isAdd && !UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
            {
                NpcJieSuanManager.inst.isUpDateNpcList = true;
            }
        }

        public static bool HasNpcFollow(int id)
        {
            return HasNpcFollow(id.ToNpcId());
        }

        public static bool HasNpcFollow(string key)
        {
            return StrGroup.Has(NpcFollow, key);
        }

        public static void SetNpcFollow(NpcInfo npcInfo)
        {
            StrGroup.Set(NpcFollow, npcInfo.Id.ToNpcId(), npcInfo.Dialog);
        }

        public static void SetNpcFollow(string key, string value)
        {
            StrGroup.Set(NpcFollow, key, value);
        }

        public static void RemoveNpcFollow(int id)
        {
            RemoveNpcFollow(id.ToNpcId());
        }

        public static void RemoveNpcFollow(string key)
        {
            var isRemove = false;
            if (NpcFollowGroup.ContainsKey(key))
            {
                RemoveNpc(key.ToNpcId(), out isRemove);
                NpcFollowGroup.Remove(key);
            }

            if (isRemove && !UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
            {
                NpcJieSuanManager.inst.isUpDateNpcList = true;
            }
        }

        public static void RemoveNpcFollow(int[] keys)
        {
            var list = keys.Select(item => item.ToString()).ToArray();
            RemoveNpcFollow(list);
        }

        public static void RemoveNpcFollow(string[] keys)
        {
            var isRemove = false;
            foreach (var key in keys)
            {
                if (NpcFollowGroup.ContainsKey(key))
                {
                    RemoveNpc(key.ToNpcId(), out isRemove);
                    NpcFollowGroup.Remove(key);
                }
            }


            if (isRemove && !UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
            {
                NpcJieSuanManager.inst.isUpDateNpcList = true;
            }
        }

        public static string GetNpcFollow(int key)
        {
            return GetNpcFollow(key.ToNpcId());
        }

        public static string GetNpcFollow(string key)
        {
            return StrGroup.Get(NpcFollow, key);
        }

        public static JSONObject GetNpcData(string npcId)
        {
            var num = Convert.ToInt32(npcId);
            if (IsNpc(npcId))
            {
                return null;
            }

            return GetNpcData(num);
        }

        public static JSONObject GetNpcData(int npcId)
        {
            var num = NPCEx.NPCIDToNew(npcId);
            if (num <= 1)
            {
                return null;
            }

            var id = num.ToString();
            JSONObject jsonObject = null;
            if (jsonData.instance != null)
            {
                if (AvatarRandomJsonData.HasField(id))
                {
                    jsonObject = AvatarRandomJsonData[id];
                }
                else if (AvatarJsonData.HasField(id))
                {
                    jsonObject = AvatarJsonData[id];
                }
            }

            return jsonObject;
        }

        public static bool SetNpcName(string id, string name) => IsNpc(id) && SetNpcName(id, name);

        public static bool SetNpcName(int id, string name)
        {
            var npc = GetNpcData(id);
            if (npc == null)
            {
                return false;
            }

            npc.SetField("Name", name);
            return true;
        }

        public static string GetSelfName(string id)
        {
            var npcId = Convert.ToInt32(id);
            if (npcId == 0)
            {
                return "";
            }

            return GetSelfName(npcId);
        }

        public static string GetSelfName(int id)
        {
            if (SelfNameDict.TryGetValue(id, out string value))
            {
                return value;
            }

            return "";
        }

        public static bool SetSelfName(string id, string name)
        {
            var npcId = Convert.ToInt32(id);
            if (npcId == 0)
            {
                return false;
            }

            return SetSelfName(npcId, name);
        }

        public static bool SetSelfName(int id, string name)
        {
            SelfNameDict[id] = name;
            return true;
        }

        public static List<int> GetNpcList(DialogCommand command, int count) => GetNpcList(command, count, count);

        public static List<int> GetNpcList(DialogCommand command, int minCount, int restCount)
        {
            var list = new List<int>();
            var paramCount = command.ParamList.Length;
            if (paramCount < minCount)
            {
                return list;
            }

            if (paramCount == restCount)
            {
                var npcId = command.GetStr(1, string.Empty);
                var npcArr = npcId.Split(',');
                foreach (var npc in npcArr)
                {
                    MyPluginMain.LogInfo($"添加NPCID: [{npc}]");
                    list.Add(NPCEx.NPCIDToNew(Convert.ToInt32(npc)));
                }
            }
            else
            {
                for (int i = restCount - 1; i < command.ParamList.Length; i++)
                {
                    var npc = NPCEx.NPCIDToNew(command.GetInt(i, -1));
                    if (npc >= 0)
                    {
                        MyPluginMain.LogInfo($"添加NPCID: [{npc}]");
                        list.Add(npc);
                    }
                }
            }

            return list;
        }

        public static void BindDialogEvent(int npc, string dialog)
        {
            var emptyDialog = string.IsNullOrWhiteSpace(dialog) || !DialogAnalysis.DialogDataDic.ContainsKey(dialog);
            if (emptyDialog || !UINPCJiaoHu.Inst.TNPCIDList.Contains(npc))
            {
                return;
            }

            if (NPCEx.IsDeath(npc)) return;
            UnityAction next = () =>
            {
                if (DialogAnalysis.IsRunningEvent)
                {
                    DialogAnalysis.SwitchDialogEvent(dialog);
                }
                else
                {
                    DialogAnalysis.StartDialogEvent(dialog);
                }
            };
            if (NPCEx.IsZhongYaoNPC(npc, out var key) && !IsFightScene)
            {
                UINPCData.ThreeSceneZhongYaoNPCTalkCache[key] = next;
                return;
            }

            UINPCData.ThreeSceneNPCTalkCache[npc] = next;
        }

        public static void AddNpc(NpcInfo npcInfo, out bool isAdd)
        {
            isAdd = false;
            if (npcInfo.Id <= 0)
            {
                return;
            }

            if (!UINPCJiaoHu.Inst.TNPCIDList.Contains(npcInfo.Id))
            {
                UINPCJiaoHu.Inst.TNPCIDList.Add(npcInfo.Id);
                isAdd = true;
            }

            BindDialogEvent(npcInfo.Id, npcInfo.Dialog);
        }

        public static void RemoveNpc(int npc, out bool isRemove)
        {
            isRemove = false;
            if (UINPCJiaoHu.Inst.TNPCIDList.Contains(npc))
            {
                UINPCJiaoHu.Inst.TNPCIDList.Remove(npc);
                isRemove = true;
            }

            NpcUtils.RemoveBindDialogEvent(npc);
        }

        public static void RemoveBindDialogEvent(int npc)
        {
            if (!UINPCData.ThreeSceneZhongYaoNPCTalkCache.ContainsKey(npc) ||
                !UINPCData.ThreeSceneNPCTalkCache.ContainsKey(npc))
            {
                return;
            }

            if (NPCEx.IsDeath(npc)) return;

            if (NPCEx.IsZhongYaoNPC(npc, out int key))
            {
                UINPCData.ThreeSceneZhongYaoNPCTalkCache.Remove(key);
                return;
            }

            UINPCData.ThreeSceneNPCTalkCache.Remove(npc);
        }
    }

    public class XinQuTypeInfo
    {
        [JsonProperty("id")] public int Id;
        [JsonProperty("name")] public string Name;
    }

    public class XinQuInfo
    {
        public string Raw;
        public string Name => GetTypeName();
        public int Type => GetTypeId();
        public int Percent => GetPercent();
        private int _id = -1;
        public JSONObject XinQu => GetXinQu();
        public string TypeRaw;
        public string PercentRaw;
        private bool _isNumber;
        private readonly bool m_isSep;
        private JObject AllItemLeiXin => jsonData.instance.AllItemLeiXin;

        public Dictionary<int, XinQuTypeInfo> XinQuTypeInfos =>
            AllItemLeiXin.ToObject<Dictionary<int, XinQuTypeInfo>>();

        public XinQuInfo(string raw)
        {
            Raw = raw;
            m_isSep = raw.Contains(":");
            Init();
        }

        public void Init()
        {
            if (m_isSep)
            {
                var split = Raw.Split(':');
                TypeRaw = split[0];
                PercentRaw = split[1];
            }
            else
            {
                TypeRaw = Raw;
                PercentRaw = "";
            }

            _isNumber = int.TryParse(TypeRaw, out _id);
        }

        public int GetPercent()
        {
            return string.IsNullOrWhiteSpace(PercentRaw) ? 100 : Convert.ToInt32(PercentRaw);
        }

        public int GetTypeId()
        {
            var dict = XinQuTypeInfos;
            var id = _id > 0 ? _id : -1;
            if (_isNumber && dict.ContainsKey(id))
            {
                return id;
            }


            foreach (var xinQuType in dict)
            {
                if (xinQuType.Value.Name == TypeRaw)
                {
                    return xinQuType.Value.Id;
                }
            }

            return -1;
        }

        public bool IsValid => Type > 0 && !string.IsNullOrWhiteSpace(Name);

        public string GetTypeName()
        {
            var id = _id > 0 ? _id : -1;
            var dict = XinQuTypeInfos;
            if (_isNumber && dict.ContainsKey(id))
            {
                return dict[id].Name;
            }

            foreach (var xinQuType in dict)
            {
                if (xinQuType.Value.Name == TypeRaw)
                {
                    return xinQuType.Value.Name;
                }
            }


            return "";
        }

        public JSONObject GetXinQu()
        {
            var xinQuType = new JSONObject(JSONObject.Type.OBJECT);
            xinQuType.SetField("type", Type);
            xinQuType.SetField("percent", Percent);
            return xinQuType;
        }
    }
}