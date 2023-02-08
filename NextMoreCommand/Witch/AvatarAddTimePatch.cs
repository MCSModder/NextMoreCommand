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
            var isDaoLv = 7200.IsDaoLv();
            if (isDaoLv)
            {
                if (true.CheckCheat()) "chuGui".Set();
                "shouMing".Set();
            }

            if ("chuGui".Check())
            {
                7200.AllDeath();
                if (!isDaoLv) 7200.AddCheat();
            }
        }
    }
}