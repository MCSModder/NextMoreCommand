﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fungus;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// using ProGif.GifManagers;
// using ProGif.Lib;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.FCanvas;
using SkySwordKill.Next.Patch;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Npc.Teleport;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Fight;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SkySwordKill.NextMoreCommand.DialogTrigger
{


    [HarmonyPatch(typeof(ThreeSceneMag))]
    public static class OnEnterThreeScene
    {
        // public static void Prefix()
        // {
        //
        //     var transform = NextMoreCommand.Instance.transform;
        //     var count = transform.childCount;
        //     for (var i = 0; i < count; i++)
        //     {
        //         var child = transform.GetChild(i);
        //         var go = child.gameObject;
        //         var component = go.GetComponent<ProGifPlayerComponent>();
        //         if (component == null) continue;
        //         go.SetActive(true);
        //         component.Clear();
        //         go.SetActive(false);
        //     }
        //
        //
        // }
        private static List<Flowchart> _flowchartsInScene = new List<Flowchart>();
        private static List<Flowchart> _flowchartsInPatch = new List<Flowchart>();
        public static  ThreeSceneMag   ThreeSceneMag;
        // [HarmonyP(nameof(ThreeSceneMag.init))]
        [HarmonyPostfix]
        [HarmonyPatch("OnDestroy")]
        public static void OnEnterThreeSceneOnDestroy()
        {
            ThreeSceneMag = null;
        }
        private static string[] _triggerTypeEnterScenePostfix = { "进入场景后", "AfterEnterScene" };
        [HarmonyPostfix]
        [HarmonyPatch(nameof(ThreeSceneMag.init))]
        public static void OnEnterThreeScene_Postfix(ThreeSceneMag __instance)
        {
            ThreeSceneMag = __instance;
            var env = new DialogEnvironment()
            {
                mapScene = Tools.getScreenName()
            };
            //MyPluginMain.LogInfo("触发ThreeSceneMag");
            NpcForceTeleport.NotDialogNpcInfos.Clear();
            // var strGroup=DialogAnalysis.AvatarNextData.StrGroup;
            // if (strGroup.HasGroup("CG_SPINE"))
            // {
            //     var group = strGroup.GetGroup("CG_SPINE");
            //     var spineObjects= CGSpineManager.Instance.SpineObjects;
            //     foreach (var cgSpine in group.Where(spine=> !string.IsNullOrWhiteSpace(spine.Key) &&!spineObjects.ContainsKey(spine.Key)))
            //     {
            //         JsonConvert.DeserializeObject<PrepareCGSpineInfo>(cgSpine.Value)?.Prepare();
            //     }
            // }
            if (DialogAnalysis.TryTrigger(_triggerTypeEnterScenePostfix, env, true))
            {
                MyPluginMain.LogInfo("触发进入场景后触发器");
            }

            NpcUtils.AddNpcFollow();
            FungusPatch();
            FightManager.RunEvent();
        }

        public static void FungusPatch()
        {
            _flowchartsInScene.Clear();
            _flowchartsInPatch.Clear();
            var patchTag = Resources.FindObjectsOfTypeAll<PatchTag>()
                .Where(item =>
                {
                    var flowchart = item.GetComponentsInChildren<Flowchart>(true);
                    return flowchart != null && flowchart.Length != 0;
                })
                .Select(item => item.GetComponentsInChildren<Flowchart>(true)).ToList();
            foreach (var tags in patchTag)
            {
                foreach (var flowchart in tags)
                {
                    _flowchartsInPatch.Add(flowchart);
                }
            }

            foreach (var rootGameObject in Resources.FindObjectsOfTypeAll<Flowchart>())
            {
                rootGameObject.GetComponentsInChildren(false, _flowchartsInScene);
                foreach (var flowchart in _flowchartsInScene)
                {

                    if (_flowchartsInPatch.Contains(flowchart))
                    {
                        // MyLog.Log("Patch已经存在", $"流程名:{fFlowchart.Name}");
                        continue;
                    }

                    try
                    {
                        // MyLog.Log("开始添加Patch", $"流程名:{fFlowchart.Name}");
                        Main.FPatch.PatchFlowchart(flowchart);
                    }
                    catch (Exception)
                    {
                        //Main.LogError(e);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(UINPCJiaoHu), nameof(UINPCJiaoHu.RefreshNowMapNPC))]
    public static class UiNpcJiaoHuRefreshNowMapNpcPatch
    {
        public static  bool     m_isRefresh;
        private static string[] _triggerTypeNpcRefreshBefore = { "NPC列表刷新前", "BeforeNpcRefreshNow" };
        public static void Prefix()
        {
            m_isRefresh = true;
            var env = new DialogEnvironment()
            {
                mapScene = Tools.getScreenName(),
            };
            if (DialogAnalysis.TryTrigger(_triggerTypeNpcRefreshBefore, env, true))
            {
                MyPluginMain.LogInfo("触发NPC列表刷新前触发器");
            }
        }

        public static string NowSceneName => SceneManager.GetActiveScene().name;
        public static List<string> BanScene = new List<string>()
        {
            "MainMenu",
            "LoadingScreen",
            "NextScene"
        };
        private static string[] _triggerTypeNpcRefreshAfter = { "NPC列表刷新后", "AfterNpcRefreshNow" };
        public static void Postfix()
        {
            var env = new DialogEnvironment()
            {
                mapScene = Tools.getScreenName()
            };
            if (DialogAnalysis.TryTrigger(_triggerTypeNpcRefreshAfter, env, true))
            {
                MyPluginMain.LogInfo("触发NPC列表刷新前触发器");
            }

            m_isRefresh = false;

            if (!NpcUtils.IsFightScene && !BanScene.Contains(NowSceneName))
            {
                foreach (var npcInfo in NpcForceTeleport.NotDialogNpcInfos)
                {
                    NpcUtils.AddNotDialogNpc(npcInfo);
                }

                NpcUtils.AddNpcNotDialogFollow();
            }
        }
    }

    [HarmonyPatch(typeof(UINPCJiaoHuPop), nameof(UINPCJiaoHuPop.CanShow))]
    public static class UiNpcJiaoHuPopCanShowPatch
    {
        public static bool Prefix(ref bool __result)
        {
            if (UINPCJiaoHu.Inst.TNPCIDList.Count > 0 && NpcUtils.IsFightScene)
            {
                __result = true;
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(UINPCLeftList), nameof(UINPCLeftList.CanShow))]
    public static class UiNpcLeftListCanShowPatch
    {
        public static bool Prefix(ref bool __result)
        {
            if (UINPCJiaoHu.Inst.TNPCIDList.Count > 0 && NpcUtils.IsFightScene)
            {
                __result = true;
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(Tools), nameof(Tools.loadOtherScenes))]
    public static class SceneManagerLoadScenePatch
    {
        private static string[] _triggerTypeEnterFightScene = new[] { "进入战斗场景", "EnterFightScene" };
        public static void Postfix(ref string name)
        {
            if (name.ToUpper().StartsWith("YSNEW"))
            {
                UINPCData.ThreeSceneNPCTalkCache.Clear();
                UINPCData.ThreeSceneZhongYaoNPCTalkCache.Clear();
                UINPCJiaoHu.Inst.TNPCIDList.Clear();
                var env = new DialogEnvironment()
                {
                    mapScene = Tools.getScreenName()
                };
                if (DialogAnalysis.TryTrigger(_triggerTypeEnterFightScene, env, true))
                {
                    MyPluginMain.LogInfo("触发进入场景后触发器");
                }
            }
        }
    }

    [HarmonyPatch(typeof(UINPCSVItem), nameof(UINPCSVItem.OnClick))]
    public static class UINPCSVItemOnClickPatch
    {
        private static string[] _triggerTypeNpcSVItemOnClick = new[] { "点击角色列表", "NPCSVItemOnClick" };
        private static string[] _triggerType                 = new[] { "围观战斗群众" };
        public static bool Prefix(ref UINPCSVItem __instance)
        {
            var npc = Traverse.Create(__instance).Field<UINPCData>("npc").Value;
            var env = new DialogEnvironment()
            {
                bindNpc = npc,
                roleBindID = npc.ZhongYaoNPCID,
                roleID = npc.ID,
                roleName = npc.Name,
                mapScene = Tools.getScreenName()
            };
            if (DialogAnalysis.TryTrigger(_triggerTypeNpcSVItemOnClick, env)) return false;
            if (!NpcUtils.IsFightScene) return true;
            npc.RefreshData();
            MyPluginMain.LogInfo($"[当前点击的npc]ID:{npc.ID} 名字:{npc.Name}");
            UINPCJiaoHu.Inst.HideJiaoHuPop();
            UINPCJiaoHu.Inst.NowJiaoHuNPC = npc;
            UINPCSVItem.NowSelectedUINPCSVItem = __instance;
            if (UINPCData.ThreeSceneNPCTalkCache.TryGetValue(npc.ID, out var value1))
            {
                value1.Invoke();
            }
            else if (UINPCData.ThreeSceneZhongYaoNPCTalkCache.TryGetValue(npc.ID, out var value))
            {
                value.Invoke();
            }
            else
            {


                if (DialogAnalysis.TryTrigger(_triggerType, env)) return false;
                var sb = new StringBuilder($"SetChar*gz#{npc.ID}\n");
                sb.AppendLine("gz#{daoyou}加油ヾ(◍°∇°◍)ﾉﾞ!!");
                DialogAnalysis.StartTestDialogEvent(sb.ToString(), env);
            }

            return false;

        }
    }
}