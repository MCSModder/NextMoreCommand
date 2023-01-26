using HarmonyLib;
using SkySwordKill.Next;
using YSGame.TuJian;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

[HarmonyPatch(typeof(TuJianManager), nameof(TuJianManager.IsUnlockedSpecialEvent))]
public static class TuJianManagerIsUnlockedSpecialEventPatchs
{
    public static void Postfix(string name, ref bool __result)
    {
        if (NextMoreCommand.instance.AchivementDebug ?? false)
        {
            Main.LogInfo($"触发解锁成就{name}");
            __result = true;
        }
    }
}