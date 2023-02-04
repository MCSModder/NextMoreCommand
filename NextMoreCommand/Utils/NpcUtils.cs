using System;
using System.Collections.Generic;
using System.Linq;
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

        public NpcInfo(int id, string dialog = "")
        {
            Dialog = dialog;
            Id = id;
        }
    }

    public static class ExtendsNpc
    {
        public static int ToNpcId(this string instance)
        {
            return NPCEx.NPCIDToNew(Convert.ToInt32(instance));
        }

        public static string ToNpcId(this int instance)
        {
            return NPCEx.NPCIDToNew(instance).ToString();
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

        public static void AddNpcFollow()
        {
            if (NpcFollowGroup.Count == 0)
            {
                return;
            }

            Main.LogInfo("开始添加npc跟随");
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
                    Main.LogInfo($"添加NPCID: [{npc}]");
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
                        Main.LogInfo($"添加NPCID: [{npc}]");
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
            if (NPCEx.IsZhongYaoNPC(npc, out var key))
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
}