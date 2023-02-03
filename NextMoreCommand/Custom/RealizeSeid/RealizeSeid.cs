// using System.Collections.Generic;
// using System.Net.NetworkInformation;
// using GUIPackage;
// using HarmonyLib;
// using KBEngine;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using SkySwordKill.Next;
// using SkySwordKill.NextMoreCommand.Custom.RealizeSeid.Skills;
// using Skill = GUIPackage.Skill;
//
// namespace SkySwordKill.NextMoreCommand.Custom.RealizeSeid;
//
// public static class RealizeSeid
// {
// }
//
// [HarmonyPatch(typeof(Skill), nameof(Skill.realizeSeid))]
// public static class SkillRealizeSeidPatch
// {
//     public static Skill CurrentSkill;
//     public static SeidSkillData seidSkillData;
//
//     public static void Prefix(ref Skill __instance, ref int seid,
//         ref List<int> damage,
//         ref Entity _attaker,
//         ref Entity _receiver,
//         ref int type)
//     {
//         CurrentSkill = __instance;
//         if (seidSkillData == null)
//         {
//             seidSkillData = new SeidSkillData(__instance, seid, damage, type);
//             seidSkillData.SetSeidSkill();
//             Main.Lua.RunFunc("TestReailzed", "Init", new object[] { seidSkillData.SeidSkill });
//         }
//         else
//         {
//             seidSkillData.SetSeidSkillData(__instance, seid, damage, type);
//         }
//
//         seidSkillData.SetAvatar(_attaker, _receiver);
//         Main.LogInfo(
//             $"[开始触发神通Seid]seid:{seid} damage:{JArray.FromObject(damage).ToString(Formatting.None)} type:{type}");
//         Main.LogInfo($"技能名字:{__instance.skill_Name}");
//         
//         Main.LogInfo(
//             $"[开始触发神通Seid]seid:{seid} damage:{JArray.FromObject(damage).ToString(Formatting.None)} type:{type}");
//         Main.LogInfo($"技能名字:{__instance.skill_Name}");
//     }
//
//     public static void Postfix(ref Skill __instance, ref int seid,
//         ref List<int> damage,
//         ref Entity _attaker,
//         ref Entity _receiver,
//         ref int type, ref int __result)
//     {
//         Main.LogInfo(
//             $"[结束触发神通Seid]seid:{seid} damage:{JArray.FromObject(damage).ToString(Formatting.None)} type:{type} result:{__result}");
//         Main.LogInfo($"技能名字:{__instance.skill_Name}");
//         Main.LogInfo(
//             $"[结束触发神通Seid]seid:{seid} damage:{JArray.FromObject(damage).ToString(Formatting.None)} type:{type} result:{__result}");
//         Main.LogInfo($"技能名字:{__instance.skill_Name}");
//         if (seidSkillData != null)
//         {
//             __result = seidSkillData.Damage;
//         }
//     }
// }
// [HarmonyPatch(typeof(Skill), nameof(Skill.PutingSkill))]
// public static class SkillPutingSkillPatch
// {
//     
//
//     public static void Postfix()
//     {
//         var seidSkillData = SkillRealizeSeidPatch.seidSkillData;
//         if (seidSkillData != null)
//         {
//             SkillRealizeSeidPatch.seidSkillData = null;
//         }
//     }
// }
// // [HarmonyPatch(typeof(StaticSkill), nameof(StaticSkill.realizeSeid))]
// // public static class StaticSkillRealizeSeidPatch
// // {
// //     public static void Prefix(ref StaticSkill __instance, ref int seid,
// //         ref List<int> flag,
// //         ref Entity _attaker,
// //         ref Entity _receiver,
// //         ref int type)
// //     {
// //         Main.LogInfo($"[结束触发功法Seid]seid:{seid} flag:{JArray.FromObject(flag).ToString(Formatting.None)} type:{type}");
// //         Main.LogInfo($"技能名字:{__instance.skill_Name}");
// //     }
// //
// //     public static void Postfix(ref StaticSkill __instance, ref int seid,
// //         ref List<int> flag,
// //         ref Entity _attaker,
// //         ref Entity _receiver,
// //         ref int type)
// //     {
// //         Main.LogInfo($"[结束触发功法Seid]seid:{seid} flag:{JArray.FromObject(flag).ToString(Formatting.None)} type:{type}");
// //         Main.LogInfo($"技能名字:{__instance.skill_Name}");
// //     }
// // }
// // [HarmonyPatch(typeof(Buff), nameof(Buff.CanRealizeSeid))]
// // static class BuffRealizeSeidPatch
// // {
// //     public static void Prefix(ref Buff __instance)
// //     {
// //     }
// //
// //     public static void Postfix(ref Buff __instance)
// //     {
// //     }
// // }
//
// // [HarmonyPatch(typeof(item), nameof(item.realizeSeid))]
// // static class ItemRealizeSeidPatch
// // {
// //     public static void Prefix(ref item __instance)
// //     {
// //     }
// //
// //     public static void Postfix(ref item __instance)
// //     {
// //     }
// // }