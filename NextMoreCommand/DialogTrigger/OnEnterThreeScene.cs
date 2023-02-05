﻿using System.Text;
using Fungus;
using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine.SceneManagement;

namespace SkySwordKill.NextMoreCommand.DialogTrigger
{
    [HarmonyPatch(typeof(ThreeSceneMag), nameof(ThreeSceneMag.init))]
    public static class OnEnterThreeScene
    {
        public static void Postfix()
        {
            var env = new DialogEnvironment()
            {
                mapScene = Tools.getScreenName()
            };
            //MyPluginMain.LogInfo("触发ThreeSceneMag");
            if (DialogAnalysis.TryTrigger(new[] { "进入场景后", "AfterEnterScene" }, env, true))
            {
               MyPluginMain.LogInfo("触发进入场景后触发器");
            }

            NpcUtils.AddNpcFollow();
        }
    }

    [HarmonyPatch(typeof(UINPCJiaoHu), nameof(UINPCJiaoHu.RefreshNowMapNPC))]
    public static class UiNpcJiaoHuRefreshNowMapNpcPatch
    {
        public static bool m_isRefresh;

        public static void Prefix()
        {
            m_isRefresh = true;
            var env = new DialogEnvironment()
            {
                mapScene = Tools.getScreenName(),
            };
            if (DialogAnalysis.TryTrigger(new[] { "NPC列表刷新前", "BeforeNpcRefreshNow" }, env, true))
            {
               MyPluginMain.LogInfo("触发NPC列表刷新前触发器");
            }
        }

        public static void Postfix()
        {
            var env = new DialogEnvironment()
            {
                mapScene = Tools.getScreenName()
            };
            if (DialogAnalysis.TryTrigger(new[] { "NPC列表刷新后", "AfterNpcRefreshNow" }, env, true))
            {
               MyPluginMain.LogInfo("触发NPC列表刷新前触发器");
            }

            m_isRefresh = false;
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
        public static void Postfix(ref string name)
        {
          // MyPluginMain.LogInfo($"[加载场景]场景名称:{name}");
            if (name.ToUpper().Contains("YSNEW"))
            {
                UINPCData.ThreeSceneNPCTalkCache.Clear();
                UINPCData.ThreeSceneZhongYaoNPCTalkCache.Clear();
                UINPCJiaoHu.Inst.TNPCIDList.Clear();
                var env = new DialogEnvironment()
                {
                    mapScene = Tools.getScreenName()
                };
                if (DialogAnalysis.TryTrigger(new[] { "进入战斗场景", "EnterFightScene" }, env, true))
                {
                   MyPluginMain.LogInfo("触发进入场景后触发器");
                }

                NpcUtils.AddNpcFollow();
            }
        }
    }

    [HarmonyPatch(typeof(UINPCSVItem), nameof(UINPCSVItem.OnClick))]
    public static class UINPCSVItemOnClickPatch
    {
        public static bool Prefix(ref UINPCSVItem __instance)
        {
            if (NpcUtils.IsFightScene)
            {
                var npc = Traverse.Create(__instance).Field<UINPCData>("npc").Value;
                npc.RefreshData();
               MyPluginMain.LogInfo($"[当前点击的npc]ID:{npc.ID} 名字:{npc.Name}");
                UINPCJiaoHu.Inst.HideJiaoHuPop();
                UINPCJiaoHu.Inst.NowJiaoHuNPC = npc;
                UINPCSVItem.NowSelectedUINPCSVItem = __instance;
                if (UINPCData.ThreeSceneNPCTalkCache.ContainsKey(npc.ID))
                {
                    UINPCData.ThreeSceneNPCTalkCache[npc.ID]();
                }
                else if (UINPCData.ThreeSceneZhongYaoNPCTalkCache.ContainsKey(npc.ID))
                {
                    UINPCData.ThreeSceneZhongYaoNPCTalkCache[npc.ID]();
                }
                else
                {
                    var env = new DialogEnvironment()
                    {
                        
                        bindNpc = npc,
                        roleBindID = npc.ZhongYaoNPCID,
                        roleID = npc.ID,
                        roleName = npc.Name,
                        mapScene = Tools.getScreenName()
                    };
                    
                    if (!DialogAnalysis.TryTrigger(new []{"围观战斗群众"}))
                    {
                        var sb = new StringBuilder($"SetChar*gz#{npc.ID}\n");
                        sb.AppendLine("gz#{xiongdi}加油!!");
                        DialogAnalysis.StartTestDialogEvent(sb.ToString());
                    }
                }

                return false;
            }

            return true;
        }
    }
}