using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using HarmonyLib;
using JSONClass;
using KBEngine;
using SkySwordKill.NextMoreCommand.Utils;
using TuPo;
using UnityEngine;
using YSGame.Fight;

namespace Zerxz.BetterSeidOptimization.Patch
{
    [HarmonyPatch(typeof(Spell))]
    public static class SpellPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Spell.addDBuff), new[]
        {
            typeof(int)
        })]
        public static bool Spell_AddOneDBuff(Spell __instance, ref List<int> __result, int buffid)
        {
            __result = new List<int>();
            if (!_BuffJsonData.DataDict.ContainsKey(buffid))
            {
                return true;
            }
            var entity = (Avatar)__instance.entity;
            var spell = entity.spell;
            var buffManager = entity.buffmag;
            var intList1 = new List<int>();
            __instance.getBuffFlag(buffid, intList1);
            spell.onBuffTickByType(11, intList1);
            if (intList1[1] == 0)
            {
                __result = (List<int>)null;
                return false;
            }

            if (buffManager.HasBuffSeid(6))
            {
                if (buffManager.getBuffBySeid(6).Any(intList2 => jsonData.instance.BuffSeidJsonData[6][string.Concat((object)intList2[2])]["value1"].list.Any(jsonObject => (int)jsonObject.n == buffid)))
                {
                    __result = (List<int>)null;
                    return false;
                }
            }
            var buffJsonData = jsonData.instance.BuffJsonData[string.Concat((object)buffid)];
            var intList3 = new List<int>();
            intList3.Add(1);
            intList3.Add(buffJsonData["totaltime"].I);
            intList3.Add(buffid);
            var i = buffJsonData["BuffType"].I;
            var flag1 = false;
            var num1 = 0;
            var buffList = entity.bufflist;
            switch (i)
            {
                case 0:
                    for (var index = 0; index < buffList.Count; ++index)
                    {
                        if (buffList[index][2] != buffid) continue;
                        flag1 = true;
                        buffList[index][1] += intList3[1];
                        num1 = index;
                        break;
                    }
                    break;
                case 1:
                    for (var index = 0; index < buffList.Count; ++index)
                    {
                        if (buffList[index][2] != buffid) continue;
                        flag1 = true;
                        buffList[index][1] = intList3[1];
                        num1 = index;
                        break;
                    }
                    break;
                case 2:
                    flag1 = true;
                    buffList.Add(intList3);
                    break;
                case 3:
                    flag1 = true;
                    num1 = buffList.Count;
                    buffList.Add(intList3);
                    break;
            }

            if (!flag1)
            {
                buffList.Add(intList3);
                num1 = buffList.IndexOf(intList3);
                buffManager.PlayBuffAdd(buffJsonData["skillEffect"].str);
            }
            var buff = buffList[num1];
            var baseShenShi = entity.GetBaseShenShi();
            var baseDunSu = entity.GetBaseDunSu();
            var seid7 = BuffSeidJsonData7.DataDict.TryGetValue(buffid, out var seidJsonData7) ? seidJsonData7.value1 : -1;
            var seid188 = BuffSeidJsonData188.DataDict.TryGetValue(buff[2], out var seidJsonData188) ? seidJsonData188.value1 : -1;;
            foreach (var jsonObject in buffJsonData["seid"].list)
            {
                switch (jsonObject.I)
                {
                    case 7:
                    {
                        if (buff[1] > seid7)
                            buff[1] = seid7;
                        break;
                    }
                    case 188:
                        switch (seid188)
                        {
                            case 1:
                                if (buff[1] > baseShenShi)
                                {
                                    buff[1] = baseShenShi;
                                    continue;
                                }
                                continue;
                            case 2:
                                if (buff[1] > baseDunSu)
                                {
                                    buff[1] = baseDunSu;
                                    continue;
                                }
                                continue;
                            default:
                                continue;
                        }
                }
            }


            var value = buff[1];
            if (value >= 2)
                spell.ONBuffTick(num1, 21);
            if (value >= 3)
                spell.ONBuffTick(num1, 13);
            if (value >= 4)
                spell.ONBuffTick(num1, 14);
            if (value >= 5)
                spell.ONBuffTick(num1, 15);
            if (value >= 6)
                spell.ONBuffTick(num1, 16);
            if (value >= 10)
                spell.ONBuffTick(num1, 17);
            jsonData.instance.Buff.TryGetValue(buff[2],out var buffValue2);
            buffValue2?.onAttach((Entity)entity, buff);
            foreach (var jsonObject in buffJsonData["seid"].list)
            {
                if ((int)jsonObject.n == 48)
                    buffValue2?.ListRealizeSeid48(48, entity, buff, (List<int>)null);
                if ((int)jsonObject.n == 22)
                    buffValue2?.ListRealizeSeid22(22, entity, buff, (List<int>)null);
            }
            var flag2 = new List<int>();
            if ((int)buffJsonData["trigger"].n == 12)
                spell.onBuffTick(num1, flag2);
            spell.onBuffTickByType(36, intList1);
            try
            {
                SteamChengJiu.ints.FightBuffOnceSetStat(entity, buff[2], buff[1]);
            }
            catch (Exception ex)
            {
                Debug.LogError((object)ex);
            }
            if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.HuaShen && entity.isPlayer() && (UnityEngine.Object)BigTuPoResultIMag.Inst == (UnityEngine.Object)null)
            {
                int buffSum1 = buffManager.GetBuffSum(3133);
                int buffSum2 = buffManager.GetBuffSum(3132);
                if (buffSum1 >= 100 || buffSum2 >= 100)
                {
                    var bigTuPoResultIMag = ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>();
                    if (buffSum1 >= 100)
                    {
                        GlobalValue.SetTalk(1, 2, "Spell.addDBuff 化神检测");
                        bigTuPoResultIMag.ShowSuccess(4);
                    }
                    else if (buffSum2 >= 100)
                    {
                        GlobalValue.SetTalk(1, 0, "Spell.addDBuff 化神检测");
                        bigTuPoResultIMag.ShowFail(4);
                    }
                }

            }
            if ((UnityEngine.Object)UIFightPanel.Inst != (UnityEngine.Object)null)
                UIFightPanel.Inst.RefreshCD();
            Event.fireOut("UpdataBuff");
            __result = intList3;
            return false;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Spell.addDBuff), new[]
        {
            typeof(int), typeof(int)
        })]
        public static bool Spell_AddMultDBuff(Spell __instance, int buffid, int time)
        {

            if (!_BuffJsonData.DataDict.ContainsKey(buffid))
            {
                return true;
            }
            var entity = (Avatar)__instance.entity;
            var spell = entity.spell;
            var intList1 = new List<int>();
            __instance.getBuffFlag(buffid, intList1);
            spell.onBuffTickByType(11, intList1);
            if (intList1[1] == 0) return true;
            var buffManager = entity.buffmag;
            if (buffManager.HasBuffSeid(6))
            {
                foreach (var index in from intList2 in buffManager.getBuffBySeid(6) from index in BuffSeidJsonData6.DataDict[intList2[2]].value1 where index == buffid select index)
                {
                    return false;
                }
            }
            MyLog.Log($"buffid:{buffid}");
            var buffJsonData = jsonData.instance.BuffJsonData[string.Concat((object)buffid)];
            var intList3 = new List<int>();
            intList3.Add(time);
            intList3.Add(buffJsonData["totaltime"].I*time);
            intList3.Add(buffid);
            var i = buffJsonData["BuffType"].I;
            var flag1 = false;
            var num1 = 0;
            var buffList = entity.bufflist;
            switch (i)
            {
                case 0:
                    for (var index = 0; index < buffList.Count; index++)
                    {
                        if (buffList[index][2] != buffid) continue;
                        flag1 = true;
                        buffList[index][1] += intList3[1];
                        num1 = index;
                        break;
                    }
                    break;
                case 1:
                    for (var index = 0; index < buffList.Count; index++)
                    {
                        if (buffList[index][2] != buffid) continue;
                        flag1 = true;
                        buffList[index][1] = intList3[1];
                        num1 = index;
                        break;
                    }
                    break;
                case 2:
                    flag1 = true;
                    buffList.Add(intList3);
                    break;
                case 3:
                    flag1 = true;
                    num1 = buffList.Count;
                    buffList.Add(intList3);
                    break;
            }

            if (!flag1)
            {
                buffList.Add(intList3);
                num1 = buffList.IndexOf(intList3);
                buffManager.PlayBuffAdd(buffJsonData["skillEffect"].str);
            }
            var buff = buffList[num1];
            var baseShenShi = entity.GetBaseShenShi();
            var baseDunSu = entity.GetBaseDunSu();
            var seid7 = BuffSeidJsonData7.DataDict.TryGetValue(buffid, out var seidJsonData7) ? seidJsonData7.value1 : -1;
            var seid188 = BuffSeidJsonData188.DataDict.TryGetValue(buff[2], out var seidJsonData188) ? seidJsonData188.value1 : -1;
            foreach (var jsonObject in buffJsonData["seid"].list)
            {
                switch (jsonObject.I)
                {
                    case 7:
                    {
                        if (buff[1] > seid7)
                            buff[1] = seid7;
                        break;
                    }
                    case 188:
                        switch (seid188)
                        {
                            case 1:
                                if (buff[1] > baseShenShi)
                                {
                                    buff[1] = baseShenShi;
                                    continue;
                                }
                                continue;
                            case 2:
                                if (buff[1] > baseDunSu)
                                {
                                    buff[1] = baseDunSu;
                                    continue;
                                }
                                continue;
                            default:
                                continue;
                        }
                }
            }

            var value = buff[1];
            if (value >= 2)
                spell.ONBuffTick(num1, 21);
            if (value >= 3)
                spell.ONBuffTick(num1, 13);
            if (value >= 4)
                spell.ONBuffTick(num1, 14);
            if (value >= 5)
                spell.ONBuffTick(num1, 15);
            if (value >= 6)
                spell.ONBuffTick(num1, 16);
            if (value >= 10)
                spell.ONBuffTick(num1, 17);
            jsonData.instance.Buff.TryGetValue(buff[2],out var buffValue2);
            buffValue2?.onAttach((Entity)entity, buff);
            foreach (var jsonObject in buffJsonData["seid"].list)
            {
                if ((int)jsonObject.n == 48)
                    buffValue2?.ListRealizeSeid48(48, entity, buff, (List<int>)null);
                if ((int)jsonObject.n == 22)
                    buffValue2?.ListRealizeSeid22(22, entity, buff, (List<int>)null);
            }
            var flag2 = new List<int>();
            if ((int)buffJsonData["trigger"].n == 12)
                spell.onBuffTick(num1, flag2);
            spell.onBuffTickByType(36, intList1);
            try
            {
                SteamChengJiu.ints.FightBuffOnceSetStat(entity, buff[2], buff[1]);
            }
            catch (Exception ex)
            {
                Debug.LogError((object)ex);
            }
            if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.HuaShen && entity.isPlayer() && (UnityEngine.Object)BigTuPoResultIMag.Inst == (UnityEngine.Object)null)
            {
                int buffSum1 = buffManager.GetBuffSum(3133);
                int buffSum2 = buffManager.GetBuffSum(3132);
                if (buffSum1 >= 100 || buffSum2 >= 100)
                {
                    var bigTuPoResultIMag = ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>();
                    if (buffSum1 >= 100)
                    {
                        GlobalValue.SetTalk(1, 2, "Spell.addDBuff 化神检测");
                        bigTuPoResultIMag.ShowSuccess(4);
                    }
                    else if (buffSum2 >= 100)
                    {
                        GlobalValue.SetTalk(1, 0, "Spell.addDBuff 化神检测");
                        bigTuPoResultIMag.ShowFail(4);
                    }
                }

            }
            if ((UnityEngine.Object)UIFightPanel.Inst != (UnityEngine.Object)null)
                UIFightPanel.Inst.RefreshCD();
            Event.fireOut("UpdataBuff");

            return false;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Spell.addBuff), new[]
        {
            typeof(int), typeof(int)
        })]
        public static bool Spell_AddMultBuff(Spell __instance, ref List<int> __result, int buffid, int num)
        {
            __result = new List<int>();
            if (!_BuffJsonData.DataDict.ContainsKey(buffid))
            {
                return true;
            }
            var buffType = _BuffJsonData.DataDict[buffid].BuffType;
            switch (buffType)
            {
                case 1:
                    __instance.addDBuff(buffid);
                    __result = (List<int>)null;
                    return false;
                case 3:
                    __instance.addDBuff(buffid, num);
                    __result = (List<int>)null;
                    return false;
            }
            Avatar entity = (Avatar)__instance.entity;
                    var spell = entity.spell;
                    var buffManager = entity.buffmag;
                    List<int> intList1 = new List<int>();
                    __instance.getBuffFlag(buffid, intList1);
                    spell.onBuffTickByType(11, intList1);
                    if (intList1[1] == 0)
                    {

                        __result = (List<int>)null;
                        return false;
                    }

                    if (buffManager.HasBuffSeid(6))
                    {
                        if (buffManager.getBuffBySeid(6).Any(intList2 => jsonData.instance.BuffSeidJsonData[6][string.Concat((object)intList2[2])]["value1"].list.Any(jsonObject => (int)jsonObject.n == buffid)))
                        {
                            __result = (List<int>)null;
                            return false;
                        }
                    }
                    var buffJsonData = jsonData.instance.BuffJsonData[string.Concat((object)buffid)];
                    var intList3 = new List<int>();
                    intList3.Add(num);
                    intList3.Add(num);
                    intList3.Add(buffid);
                    var i = buffJsonData["BuffType"].I;
                    var flag1 = false;
                    var num1 = 0;
                    var buffList = entity.bufflist;
                    switch (i)
                    {
                        case 0:
                            for (var index = 0; index < buffList.Count; ++index)
                            {
                                if (buffList[index][2] != buffid) continue;
                                flag1 = true;
                                buffList[index][1] += intList3[1];
                                num1 = index;
                                break;
                            }
                            break;
                        case 1:
                            for (var index = 0; index < buffList.Count; ++index)
                            {
                                if (buffList[index][2] != buffid) continue;
                                flag1 = true;
                                buffList[index][1] = intList3[1];
                                num1 = index;
                                break;
                            }
                            break;
                        case 2:
                            flag1 = true;
                            buffList.Add(intList3);
                            break;
                        case 3:
                            flag1 = true;
                            num1 = buffList.Count;
                            buffList.Add(intList3);
                            break;
                    }

                    if (!flag1)
                    {
                        buffList.Add(intList3);
                        num1 = buffList.IndexOf(intList3);
                        buffManager.PlayBuffAdd(buffJsonData["skillEffect"].str);
                    }
                    var buff = buffList[num1];
                    var baseShenShi = entity.GetBaseShenShi();
                    var baseDunSu = entity.GetBaseDunSu();
                    var seid7 = BuffSeidJsonData7.DataDict.TryGetValue(buffid, out var seidJsonData7) ? seidJsonData7.value1 : -1;
                    var seid188 = BuffSeidJsonData188.DataDict.TryGetValue(buff[2], out var seidJsonData188) ? seidJsonData188.value1 : -1;
                    foreach (var jsonObject in buffJsonData["seid"].list)
                    {
                        switch (jsonObject.I)
                        {
                            case 7:
                            {
                                if (buff[1] > seid7)
                                    buff[1] = seid7;
                                break;
                            }
                            case 188:
                                switch (seid188)
                                {
                                    case 1:
                                        if (buff[1] > baseShenShi)
                                        {
                                            buff[1] = baseShenShi;
                                            continue;
                                        }
                                        continue;
                                    case 2:
                                        if (buff[1] > baseDunSu)
                                        {
                                            buff[1] = baseDunSu;
                                            continue;
                                        }
                                        continue;
                                    default:
                                        continue;
                                }
                        }
                    }


                    var value = buff[1];
                    if (value >= 2)
                        spell.ONBuffTick(num1, 21);
                    if (value >= 3)
                        spell.ONBuffTick(num1, 13);
                    if (value >= 4)
                        spell.ONBuffTick(num1, 14);
                    if (value >= 5)
                        spell.ONBuffTick(num1, 15);
                    if (value >= 6)
                        spell.ONBuffTick(num1, 16);
                    if (value >= 10)
                        spell.ONBuffTick(num1, 17);
                    jsonData.instance.Buff.TryGetValue(buff[2],out var buffValue2);
                    buffValue2?.onAttach((Entity)entity, buff);
                    foreach (var jsonObject in buffJsonData["seid"].list)
                    {
                        if ((int)jsonObject.n == 48)
                            buffValue2?.ListRealizeSeid48(48, entity, buff, (List<int>)null);
                        if ((int)jsonObject.n == 22)
                            buffValue2?.ListRealizeSeid22(22, entity, buff, (List<int>)null);
                    }
                    var flag2 = new List<int>();
                    if ((int)buffJsonData["trigger"].n == 12)
                        spell.onBuffTick(num1, flag2);
                    spell.onBuffTickByType(36, intList1);
                    try
                    {
                        SteamChengJiu.ints.FightBuffOnceSetStat(entity, buff[2], buff[1]);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError((object)ex);
                    }
                    if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.HuaShen && entity.isPlayer() && (UnityEngine.Object)BigTuPoResultIMag.Inst == (UnityEngine.Object)null)
                    {
                        int buffSum1 = buffManager.GetBuffSum(3133);
                        int buffSum2 = buffManager.GetBuffSum(3132);
                        if (buffSum1 >= 100 || buffSum2 >= 100)
                        {
                            var bigTuPoResultIMag = ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>();
                            if (buffSum1 >= 100)
                            {
                                GlobalValue.SetTalk(1, 2, "Spell.addDBuff 化神检测");
                                bigTuPoResultIMag.ShowSuccess(4);
                            }
                            else if (buffSum2 >= 100)
                            {
                                GlobalValue.SetTalk(1, 0, "Spell.addDBuff 化神检测");
                                bigTuPoResultIMag.ShowFail(4);
                            }
                        }

                    }
                    if ((UnityEngine.Object)UIFightPanel.Inst != (UnityEngine.Object)null)
                        UIFightPanel.Inst.RefreshCD();
                    Event.fireOut("UpdataBuff");
                    __result = intList3;
                    return false;
            }
        }
    }