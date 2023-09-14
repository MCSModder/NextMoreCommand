using System.Collections.Generic;
using System.IO;
using HarmonyLib;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

[HarmonyPatch(typeof(YSNewSaveSystem), nameof(YSNewSaveSystem.AutoLoad))]
public static class YsNewSaveSystemLoadPatch
{
    public static bool Prefix()
    {
        var cgSpineManager = CGSpineManager.Instance;
        if (cgSpineManager.SpineObjects.Count == 0)
        {
            return true;
        }
        cgSpineManager.Reset();
        cgSpineManager.LoadSave();
        return false;
    }
}

// [HarmonyPatch(typeof(AuToSLMgr), nameof(AuToSLMgr.CanSave))]
// public static class YsNewSaveSystemSavePatch
// {
//     public static bool Prefix(ref bool __result)
//     {
//         if (!DialogAnalysis.IsRunningEvent) return true;
//         UIPopTip.Inst.Pop("当前运行MOD剧情状态禁止存档", PopTipIconType.叹号);
//         UIPopTip.Inst.Pop("如果存档不了,按F4键找[剧情调试]点击[重置事件状态]", PopTipIconType.叹号);
//         __result = false;
//         return false;
//     }
// }