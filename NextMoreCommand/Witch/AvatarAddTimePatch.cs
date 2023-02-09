using HarmonyLib;
using KBEngine;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Witch
{
    [HarmonyPatch(typeof(Avatar), "AddTime")]
    public static class AvatarAddTimePatch
    {
        public static void Postfix()
        {
            7200.SetHarem();

            3.SetCheat(7200);

            if (WitchUtils.HasWife)
            {
                if (true.CheckCheat()) true.SetCheat();
                true.SetLife();
            }
        }
    }
}