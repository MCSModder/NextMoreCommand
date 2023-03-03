using HarmonyLib;
using YSGame.TuJian;

namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(TuJianManager), nameof(TuJianManager.IsUnlockedSpecialEvent))]
public static class TuJianManagerIsUnlockedSpecialEventPatchs
{
    public static void Postfix(string name, ref bool __result)
    {
        if (NextMoreCommand.Instance.AchivementDebug ?? false)
        {
            MyPluginMain.LogInfo($"触发解锁成就{name}");
            __result = true;
        }
    }
}