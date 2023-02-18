using HarmonyLib;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(DongFuManager), nameof(DongFuManager.CreateDongFu))]
public static class DongFuCreatePatch
{
    public static void Prefix(int dongFuID, ref int level)
    {
        MyLog.Log("创建洞府", $"洞府ID:{dongFuID} 灵眼:{level}");
        if (dongFuID == 2 && DongFuManager.PlayerHasDongFu(dongFuID))
        {
            var dongfu = DongFuUtils.GetDongFuData(dongFuID);
            if (dongfu.LingYanLevel > level)
            {
                level = dongfu.LingYanLevel;
                MyLog.Log("修改创建洞府", $"洞府ID:{dongFuID} 灵眼:{level}");
            }
        }
    }
}