using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using JSONClass;
using KBEngine;
using Newtonsoft.Json;
using Skill = GUIPackage.Skill;

namespace Zerxz.BetterSeidOptimization.Patch
{
    [HarmonyPatch(typeof(Skill))]
    public static class SkillPatch
    {
        // [HarmonyPrefix]
        // [HarmonyPatch(nameof(Skill.realizeSeid102))]
        // public static bool RealizeSeid102(Skill __instance,int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
        // {
        //     var seidJson = SkillSeidJsonData102.DataDict[__instance.skill_ID];
        //     var xJson = seidJson.value1;
        //     var yJson = seidJson.value2;
        //     var buffList = attaker.bufflist.Select(buff => buff[2]).ToList();
        //     var count = buffList.Count;
        //     for (var i = 0; i < count; i++)
        //     {
        //         var buff = buffList[i];
        //         if (!xJson.Contains(buff)) continue;
        //         foreach (var index in yJson)
        //         {
        //             for (var j = 0; j < index; j++)
        //             {
        //                 attaker.spell.onBuffTick(i, new List<int>
        //                 {
        //                     0
        //                 }, 0);
        //             }
        //         }
        //     }
        //
        //     return false;
        // }
        public static T GetSeidJson<T>(this Skill instance, int seid) where T : IJSONClass, new()
        {
            var json = instance.getSeidJson(seid);
            return json == null ? new T() : JsonConvert.DeserializeObject<T>(json.ToString());
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Skill.realizeSeid4))]
        public static bool RealizeSeid4(Skill __instance, int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
        {

            var seidJson = __instance.GetSeidJson<SkillSeidJsonData4>(seid);
            var xJson = seidJson.value1;
            var yJson = seidJson.value2;
            for (var index1 = 0; index1 < xJson.Count; index1++)
            {
                var xValue = xJson[index1];
                var yValue = yJson[index1];
                receiver.spell.addBuff(xValue, yValue);
                // if (yValue >= 100)
                // {
                //     receiver.spell.addBuff(xValue, yValue);
                // }
                // else
                // {
                //     for (int index2 = 0; index2 < yValue; ++index2)
                //         receiver.spell.addDBuff(xValue);
                // }
            }
            return false;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Skill.realizeSeid31))]
        public static bool RealizeSeid31(Skill __instance, int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
        {
            var seidJson = __instance.GetSeidJson<SkillSeidJsonData31>(seid);
            var xJson = seidJson.value1;
            var yJson = seidJson.value2;

            for (var index1 = 0; index1 < xJson.Count; index1++)
            {
                var xValue = xJson[index1];
                var yValue = yJson[index1];
                attaker.spell.addBuff(xValue, yValue);
                // if (yValue >= 100)
                // {
                //     receiver.spell.addBuff(xValue, yValue);
                // }
                // else
                // {
                //     for (int index2 = 0; index2 < yValue; ++index2)
                //         receiver.spell.addDBuff(xValue);
                // }
            }
            return false;
        }
         // [HarmonyPrefix]
         // [HarmonyPatch(nameof(Skill.realizeSeid152))]
         // public static bool RealizeSeid152(Skill __instance, int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
         // {
         //     var seidJson = __instance.GetSeidJson<SkillSeidJsonData152>(seid);
         //     var xJson = seidJson.value1;
         //     var yJson = seidJson.value2;
         //
         //     for (var index1 = 0; index1 < xJson.Count; index1++)
         //     {
         //         var xValue = xJson[index1];
         //         var yValue = yJson[index1];
         //         attaker.spell.addBuff(xValue, yValue);
         //         // if (yValue >= 100)
         //         // {
         //         //     receiver.spell.addBuff(xValue, yValue);
         //         // }
         //         // else
         //         // {
         //         //     for (int index2 = 0; index2 < yValue; ++index2)
         //         //         receiver.spell.addDBuff(xValue);
         //         // }
         //     }
         //     return false;
         // }
    }

}