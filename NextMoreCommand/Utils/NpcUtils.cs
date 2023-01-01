using System;
using System.Collections.Generic;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class NpcUtils
    {
        public static Dictionary<int, string> SelfNameDict = new Dictionary<int, string>();

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

            return SetNickname(npcId, name);
        }

        public static bool SetNickname(int id, string name)
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
    }
}