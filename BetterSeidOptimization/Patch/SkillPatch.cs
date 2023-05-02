using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using JSONClass;
using KBEngine;
using Newtonsoft.Json;
using SkySwordKill.NextMoreCommand.Utils;
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
            if (!BetterSeidOptimization.BetterMode)
            {
                return true;
            }
            var seidJson = __instance.GetSeidJson<SkillSeidJsonData4>(seid);
            var xJson = seidJson.value1;
            var yJson = seidJson.value2;
            var num = damage[4] > 1 ? damage[4] : 1;
            for (var index1 = 0; index1 < xJson.Count; index1++)
            {
                var xValue = xJson[index1];
                var yValue = yJson[index1];
                receiver.spell.addBuff(xValue, yValue * num);
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
            MyLog.Log(JsonConvert.SerializeObject(seidJson, Formatting.Indented));
            MyLog.Log(__instance.getSeidJson(seid).ToString(true));
            if (!BetterSeidOptimization.BetterMode)
            {
                return true;
            }

            var xJson = seidJson.value1;
            var yJson = seidJson.value2;
            var num = damage[4] > 1 ? damage[4] : 1;

            for (var index1 = 0; index1 < xJson.Count; index1++)
            {
                var xValue = xJson[index1];
                var yValue = yJson[index1];
                attaker.spell.addBuff(yValue, xValue * num);
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
        private readonly static List<int> BanSeidForeach = new List<int>()
        {
            31,
            4
        };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Skill.triggerBuffEndSeid))]
        public static bool TriggerBuffEndSeid(Skill __instance, List<int> SeidList,
            List<int> infoFlag,
            Entity _attaker,
            Entity _receiver,
            int type)
        {
            if (!BetterSeidOptimization.BetterMode)
            {
                return true;
            }
            List<int> TempSeid = new List<int>();
            SeidList.ForEach((Action<int>) (aa => TempSeid.Add(aa)));
            int _index = 0;
            foreach (int seid in TempSeid)
            {
                try
                {
                    if (infoFlag[2] == 1)
                        break;
                    if (__instance.setSeidNum(TempSeid, infoFlag, _index))
                    {
                        if (BanSeidForeach.Contains(seid))
                        {
                            __instance.realizeFinalSeid(seid, infoFlag, _attaker, _receiver, type);
                        }
                        else
                        {
                            for (int index = 0; index < infoFlag[4]; index++)
                                __instance.realizeFinalSeid(seid, infoFlag, _attaker, _receiver, type);
                        }
                    }
                    else
                        __instance.realizeBuffEndSeid(seid, infoFlag, _attaker, _receiver, type);
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError((object) ("检测到技能错误！错误 SkillID:" + (object) __instance.skill_ID + " 技能特性:" + (object) seid + "额外数据：" + infoFlag.ToString()));
                    UnityEngine.Debug.LogError((object) ex);
                }
                _index++;
            }
            return false;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Skill.triggerSkillFinalSeid))]
        public static bool TriggerSkillFinalSeid(Skill __instance, List<int> SeidList,
            List<int> infoFlag,
            Entity _attaker,
            Entity _receiver,
            int type)
        {
            if (!BetterSeidOptimization.BetterMode)
            {
                return true;
            }
            List<int> TempSeid = new List<int>();
            SeidList.ForEach((Action<int>)(aa => TempSeid.Add(aa)));
            int _index = 0;
            foreach (int seid in TempSeid)
            {
                try
                {
                    if (__instance.setSeidNum(TempSeid, infoFlag, _index))
                    {
                        if (BanSeidForeach.Contains(seid))
                        {
                            __instance.realizeFinalSeid(seid, infoFlag, _attaker, _receiver, type);
                        }
                        else
                        {
                            for (int index = 0; index < infoFlag[4]; index++)
                                __instance.realizeFinalSeid(seid, infoFlag, _attaker, _receiver, type);
                        }
                    }
                    else
                        __instance.realizeFinalSeid(seid, infoFlag, _attaker, _receiver, type);
                    if (infoFlag[2] == 1)
                        break;
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError((object)("检测到技能错误！错误 SkillID:" + (object)__instance.skill_ID + " 技能特性:" + (object)seid + "额外数据：" + infoFlag.ToString()));
                    UnityEngine.Debug.LogError((object)ex);
                }
                _index++;
            }
            return false;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Skill.triggerStartSeid))]
        public static bool TriggerStartSeid(Skill __instance, List<int> SeidList,
            List<int> infoFlag,
            Entity _attaker,
            Entity _receiver,
            int type)
        {
            if (!BetterSeidOptimization.BetterMode)
            {
                return true;
            }
            List<int> tempSeid = new List<int>();
            SeidList.ForEach((Action<int>)(aa => tempSeid.Add(aa)));
            int _index = 0;
            int num = -1;
            foreach (int seid in tempSeid)
            {
                try
                {
                    if (num > 0)
                    {
                        if (tempSeid.IndexOf(seid) >= num)
                        {
                            infoFlag[2] = 0;
                            num = -1;
                        }
                        else
                            continue;
                    }
                    if (__instance.setSeidNum(tempSeid, infoFlag, _index))
                    {
                        if (BanSeidForeach.Contains(seid))
                        {
                            __instance.realizeSeid(seid, infoFlag, _attaker, _receiver, type);
                        }
                        else
                        {
                            for (int index = 0; index < infoFlag[4]; ++index)
                                __instance.realizeSeid(seid, infoFlag, _attaker, _receiver, type);
                        }

                    }
                    else
                        __instance.realizeSeid(seid, infoFlag, _attaker, _receiver, type);
                    if (infoFlag[2] == 1)
                    {
                        if (tempSeid.Contains(117))
                        {
                            for (int index = tempSeid.IndexOf(seid); index < tempSeid.Count; ++index)
                            {
                                if (tempSeid[index] == 117)
                                {
                                    num = index;
                                    break;
                                }
                            }
                        }
                        if (num < 0)
                            break;
                    }
                    else if (_attaker.isPlayer())
                    {
                        if (RoundManager.instance.PlayerSkillCheck != null)
                            RoundManager.instance.PlayerSkillCheck.HasPassSeid.Add(seid);
                    }
                    else if (RoundManager.instance.NpcSkillCheck != null)
                        RoundManager.instance.NpcSkillCheck.HasPassSeid.Add(seid);
                }
                catch (Exception ex)
                {
                    string str = "";
                    for (int index = 0; index < infoFlag.Count; index++)
                        str += string.Format(" {0}", (object)infoFlag[index]);
                    UnityEngine.Debug.LogError((object)("检测到技能错误！错误 SkillID:" + (object)__instance.skill_ID + " 技能特性:" + (object)seid + "额外数据：" + str));
                    UnityEngine.Debug.LogError((object)("异常信息:" + ex.Message + "\n异常位置:" + ex.StackTrace));
                    UnityEngine.Debug.LogError((object)string.Format("{0}", (object)ex));
                }
                _index++;
            }
            return false;
        }
    }

}