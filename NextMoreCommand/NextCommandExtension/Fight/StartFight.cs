using System;
using GUIPackage;
using HarmonyLib;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.DialogTrigger;
using UnityEngine.SceneManagement;
using Fight;
using Fungus;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils.Fight;
using YSGame;
using StartFight = Fungus.StartFight;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fight
{
    [RegisterCommand]
    [DialogEvent("StartFightNext")]
    public class StartFightNext : IDialogEvent
    {


        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            FightManager.SetFightInfo(command);
        }


        [HarmonyPatch(typeof(FightResultMag), "ShowVictory")]
        public class VictoryPatch
        {
            [HarmonyPrefix]
            public static void Prefix()
            {
                FightManager.SetVictory();
            }
        }

        [HarmonyPatch(typeof(UIDeath), "BackToMainMenu")]
        public class DeathPatch
        {
            [HarmonyPrefix]
            public static void Prefix()
            {
                FightManager.ResetEventFight();
            }
        }

        // [HarmonyPatch(typeof(UI_Manager),"Update")]
        // public class AfterBattleDialogPatch
        // {
        //     [HarmonyPrefix]
        //     public static void Prefix()
        //     {
        //         if(Tools.instance.getPlayer() == null)
        //             return;
        //             
        //         string sceneName = SceneManager.GetActiveScene().name;
        //         var IsEventFight = DialogAnalysis.GetInt("Fight", "IsEventFight") == 1;
        //         var IsInBattle = sceneName == "YSNewFight";
        //         if(Tools.instance.isNeedSetTalk && IsEventFight)
        //         {
        //             if (Tools.instance.loadSceneType == 0)
        //             {
        //                 // 使用NextScene跳转 跳转到战斗场景
        //                 if (sceneName.Equals(Tools.instance.ohtherSceneName) && IsInBattle)
        //                 {
        //                     OnFightStart.Trigger();
        //                 }
        //             }
        //             else 
        //             {
        //                 // 直接跳转 跳转到大地图
        //                 if (sceneName.Equals(Tools.jumpToName))
        //                 {
        //                     Tools.instance.CanShowFightUI = 0;
        //                         
        //                     var VictoryEvent = DialogAnalysis.GetStr("Fight", "VictoryEvent");
        //                     var DefeatEvent = DialogAnalysis.GetStr("Fight", "DefeatEvent");
        //                     var IsVictory = DialogAnalysis.GetInt("Fight", "IsVictory") == 1;
        //
        //                     if (IsVictory && !string.IsNullOrEmpty(VictoryEvent))
        //                     {
        //                         DialogAnalysis.StartDialogEvent(VictoryEvent);
        //                     }
        //                     else if (!IsVictory && !string.IsNullOrEmpty(DefeatEvent))
        //                     {
        //                         DialogAnalysis.StartDialogEvent(DefeatEvent);
        //                     }
        //                     ResetEventFight();
        //                 }
        //             }
        //         }
        //     }
        // }
        // [HarmonyPatch(typeof(RoundManager),"Awake")]
        // public class BackgroundPatch
        // {
        //     [HarmonyPostfix]
        //     public static void Postfix(RoundManager __instance)
        //     {
        //         if (!RoundManager.TuPoTypeList.Contains(Tools.instance.monstarMag.FightType))
        //         {
        //             if (Tools.instance.monstarMag.FightImageID != 0)
        //             {
        //                 if (Main.Res.TryGetAsset($"Assets/Fightimage/{Tools.instance.monstarMag.FightImageID}.png", out var asset))
        //                 {
        //                     if (asset is Texture2D texture)
        //                     {
        //                         Sprite sprite = Main.Res.GetSpriteCache(texture);
        //                         __instance.BackGroud.sprite = sprite;
        //                     }
        //                 }
        //             }
        //         }
        //     }
        // }
    }
}