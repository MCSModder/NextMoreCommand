using HarmonyLib;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Witch
{
    [HarmonyPatch(typeof(BiaoBaiManager), "CalcBiaoBaiScore")]
    static class BiaoBaiManagerCalcBiaoBaiScorePatch
    {
        public static void Postfix()
        {
            if (7200.ToNpcNewId() == UINPCJiaoHu.Inst.NowJiaoHuNPC.ID.ToNpcNewId())
            {
                if ("huaXin".HasInt()
                    || 170.GetGlobalValue("BiaoBaiManager.CalcBiaoBaiScore 获取道侣数量"))
                {
                    200.ReduceScore();
                }
                else if (24001.HasTianFu() && "shenShi".HasInt())
                {
                    30.AddScore();
                }
            }
        }
    }
}