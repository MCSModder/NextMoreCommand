using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using HarmonyLib;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.HarmonyPatchs;
using Steamworks;
using UnityEngine;

namespace UnsubscribeNextMoreCommandOld
{
    [BepInDependency("skyswordkill.plugin.NextMoreCommand")]
    [BepInDependency("skyswordkill.plugin.Next")]
    [BepInPlugin("UnsubscribeNextMoreCommandOld", "UnsubscribeNextMoreCommandOld", "1.0.0")]
    public class UnsubscribeNextMoreCommandOld : BaseUnityPlugin
    {

        private void Awake()
        {
            new Harmony("UnsubscribeNextMoreCommandOld").PatchAll();
        }
    }

    [HarmonyPatch(typeof(PathsPatch), nameof(PathsPatch.Prefix))]
    static class PathsPatchPrefix
    {
        static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(TuJianManagerIsUnlockedSpecialEventPatch),
        nameof(TuJianManagerIsUnlockedSpecialEventPatch.Postfix))]
    static class TuJianManagerIsUnlockedSpecialEventPatchsPrefix
    {
        static bool Prefix()
        {

            return false;
        }
    }
    [HarmonyPatch(typeof(MainUIMag),(nameof(MainUIMag.OpenMain)))]
    public static class MainUIMagOpenMainPatch
    {
        public static void Postfix(MainUIMag __instance)
        {
            if (SteamAPI.IsSteamRunning())
            {
                Unsubscribe();
                UIPopTip.Inst.Pop("建议游戏重启", PopTipIconType.叹号);
                UCheckBox.Show("修改创意工坊设置后，需要重启生效,点击确定关闭游戏", Application.Quit);
            }
        }
        public static void Subscribe(params ulong[] items)
        {
            var allMod = WorkshopTool
                .GetAllModDirectory()
                .Where(item => Convert.ToUInt64(item.Name) != 0)
                .Select(item => Convert.ToUInt64(item.Name)).ToList();
            foreach (var id in items)
            {
                if (!allMod.Contains(id))
                {
                    if (UIPopTip.Inst == null)
                    {
                        return;
                    }

                    UIPopTip.Inst.Pop($"开始订阅{id.ToString()}Mod");
                    SteamUGC.SubscribeItem(new PublishedFileId_t((ulong)id));
                }
            }
        }
        public static void Unsubscribe()
        {
            var id = 2862679721;
            foreach (var directoryInfo in WorkshopTool.GetAllModDirectory())
            {
                if (directoryInfo.Name == id.ToString())
                {
                    UIPopTip.Inst.Pop("检测到老版Next更多指令Mod");
                    UIPopTip.Inst.Pop("开始取消订阅老版Next更多指令Mod");
           
                    var result = SteamUGC.UnsubscribeItem(new PublishedFileId_t(id));

                    UIPopTip.Inst.Pop("取消订阅老版Next更多指令Mod成功", PopTipIconType.叹号);
                    break;
                }
            }
        }
    }
}