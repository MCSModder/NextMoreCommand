using System;
using HarmonyLib;
using JSONClass;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(NPCTalkCmdHelper), nameof(NPCTalkCmdHelper.ReplaceTalkWord), new Type[] { typeof(string), typeof(UINPCData) })]
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

    [HarmonyPatch(typeof(Say), nameof(Say.Execute))]
    public static class DialogEventSayPatch
    {
        public static DialogCommand     Command { get; private set; }
        public static DialogEnvironment Env     { get; private set; }
        public static void Prefix(DialogCommand command, DialogEnvironment env)
        {
            Command = command;
            Env = env;
        }
        public static void Postfix()
        {
            Command = null;
            Env = null;
        }
    }

    [HarmonyPatch(typeof(DialogAnalysis), nameof(DialogAnalysis.DealSayText))]
    public static class DialogAnalysisDealSayText
    {
        public static void Postfix(ref string text, int sayRoleID, ref string __result)
        {
            if (sayRoleID == 0)
            {
                return;
            }
            var env   = DialogEventSayPatch.Env ?? new DialogEnvironment();
            var npc   = env.bindNpc;
            var npcId = env.roleID.ToNpcNewId();
            if (sayRoleID == 1)
            {
                if (npc == null && npcId <= 0)
                {
                    return;
                }
                npc ??= new UINPCData(npcId);
                npc.RefreshData();
                __result = __result.ReplacePlayerTalkToNPCWord(npc);
                return;
            }
            var id = sayRoleID.ToNpcNewId();
            if (sayRoleID <= 0 || !jsonData.instance.AvatarJsonData.HasField(id.ToString()))
            {
                return;
            }
            npc = new UINPCData(id);
            npc.RefreshData();
            __result = __result.ReplaceTalkWord(npc);
        }
    }
}