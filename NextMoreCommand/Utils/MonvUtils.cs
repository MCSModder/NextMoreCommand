// using HarmonyLib;
// using KBEngine;
// using SkySwordKill.Next.DialogSystem;
//
// namespace SkySwordKill.NextMoreCommand.Utils;
//
// [HarmonyPatch(typeof(Avatar), "AddTime")]
// public static class MonvUtils
// {
//     public static void Postfix()
//     {
//         var isDaolv = IsDaolv;
//         var chuGui = CheckChuGui();
//         var count = DaolvUtils.DaolvId.Count;
//
//         if (isDaolv)
//         {
//             if (!chuGui && count > 1 ) SetChuGui();
//             SetShouMing();
//         }
//
//         if (chuGui)
//         {
//             selectBox._instence.setChoice("");
//             DaolvUtils.SetAllDaolvDeath(7200);
//             if (!isDaolv) DaolvUtils.DaolvId.Add(npcId);
//         }
//     }
//
//     public const int ID = 7200;
//     public static int npcId => NPCEx.NPCIDToNew(ID);
//     public static bool IsDaolv => PlayerEx.IsDaoLv(npcId) && HasInt("shengSi");
//     public static Avatar Player => PlayerEx.Player;
//
//     public static bool CheckChuGui()
//     {
//         var count = DaolvUtils.DaolvId.Count;
//         var isDaolv = IsDaolv;
//         var isHarem = isDaolv && count > 1;
//         var isChuGui = !isDaolv && count >= 1;
//         return HasInt("daHun") && isHarem || isChuGui;
//     }
//
//     public static void SetChuGui()
//     {
//         var key = "chuGui";
//         var result = GetInt(key) + 1;
//         SetInt(key, result);
//         if (result > 3)
//         {
//             DaolvUtils.SetAllDaolvDeath(7200);
//         }
//     }
//
//     public static bool CheckShouMing() => (Player.shouYuan - Player.age) <= 10;
//     public static void SetShouMing() => SetInt("shouMing", !CheckShouMing() ? 0 : 1);
//     public static bool HasInt(string name) => DialogAnalysis.GetInt(name) == 1;
//     public static int GetInt(string name) => DialogAnalysis.GetInt(name);
//     public static void SetInt(string name, int value) => DialogAnalysis.SetInt(name, value);
// }