using System;
using HarmonyLib;
using JSONClass;
using SkySwordKill.Next.DialogSystem;
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
    [HarmonyPatch(typeof(DialogAnalysis),nameof(DialogAnalysis.DealSayText))]
    public static class DialogAnalysisDealSayText{
        public static void Prefix(ref string  text, int sayRoleID)
        {
            var id = sayRoleID.ToNpcNewId();
            if (sayRoleID <= 0 ||!jsonData.instance.AvatarJsonData.HasField(id.ToString()))
            {
                return;
            }
            var npc = new UINPCData(id);
            npc.RefreshData();
            text=  text.ReplaceTalkWord(npc);
        }
    }
}