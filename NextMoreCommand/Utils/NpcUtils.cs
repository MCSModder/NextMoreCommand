using System;
using System.Collections.Generic;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class NpcUtils
    {
        public static List<int> GetNpcList(DialogCommand command)
        {
            var list = new List<int>();
            var paramCount = command.ParamList.Length;
            if (paramCount <2)
            {
                return list;
            }
            if (paramCount == 2)
            {
                var npcId = command.GetStr(1, string.Empty);
                var npcArr = npcId.Split(',') ;
                foreach (var npc in npcArr)
                {
                    list.Add(NPCEx.NPCIDToNew(Convert.ToInt32(npc)));
                }
            }
            else
            {
                for (int i = 1; i < command.ParamList.Length; i++)
                {
                
                    var npc = NPCEx.NPCIDToNew(command.GetInt(i, -1));
                    if (npc >= 0)
                    {
                        list.Add(npc);
                    }
                    
                }
            }

            return list;
        }
    }
}