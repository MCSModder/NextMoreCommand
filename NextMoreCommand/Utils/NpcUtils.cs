using System;
using System.Collections.Generic;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class NpcUtils
    {
        public static List<int> GetNpcList(DialogCommand command, int count) => GetNpcList(command, count, count);
        public static List<int> GetNpcList(DialogCommand command,int minCount,int restCount)
        {
            var list = new List<int>();
            var paramCount = command.ParamList.Length;
            if (paramCount <minCount)
            {
                return list;
            }
            if (paramCount == restCount)
            {
                var npcId = command.GetStr(1, string.Empty);
                var npcArr = npcId.Split(',') ;
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