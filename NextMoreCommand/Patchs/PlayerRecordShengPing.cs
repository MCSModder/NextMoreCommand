using System;
using System.Collections.Generic;
using HarmonyLib;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(PlayerEx),nameof(PlayerEx.RecordShengPing))]
    public static class PlayerRecordShengPing
    {
        public static string shengPing = string.Empty;
        public static void Prefix(string shengPingID, Dictionary<string, string> args)
        {
            shengPing = shengPingID;
            DialogAnalysis.TryTrigger(new[]
            {
                "PlayerRecordShengPing", "shengPing"
            }, null, true);
        }
    }
}