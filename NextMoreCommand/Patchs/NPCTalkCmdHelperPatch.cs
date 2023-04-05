using System;
using HarmonyLib;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(NPCTalkCmdHelper), nameof(NPCTalkCmdHelper.ReplaceTalkWord), new Type[]
    {
        typeof(string), typeof(UINPCData)
    })]
    public static class NPCTalkCmdHelperPatch
    {
        public static void Prefix(ref string str, UINPCData npc)
        {
            var name = NpcUtils.GetCallName(npc.ID);
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }
            str = str.Replace("{daoyou}", name).Replace("{qianbei}", name);
        }
    }
}