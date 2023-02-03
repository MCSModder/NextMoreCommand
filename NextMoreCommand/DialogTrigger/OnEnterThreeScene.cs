using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.DialogTrigger
{
    [HarmonyPatch(typeof(ThreeSceneMag),nameof(ThreeSceneMag.init))]
    public static class OnEnterThreeScene
    {
        public static void Postfix()
        {
            var env = new DialogEnvironment()
            {
                mapScene = Tools.getScreenName()
            };
            Main.LogInfo("触发ThreeSceneMag");
            if (DialogAnalysis.TryTrigger(new[] { "进入场景后","AfterEnterScene" }, env, true))
            {
                Main.LogInfo("触发进入场景后触发器");
            }
        }
    }
    [HarmonyPatch(typeof(UINPCJiaoHu),nameof(UINPCJiaoHu.RefreshNowMapNPC))]
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
            if (   DialogAnalysis.TryTrigger(new[] { "NPC列表刷新前","BeforeNpcRefreshNow" }, env, true))
            {
                Main.LogInfo("触发NPC列表刷新前触发器");
            }
         
        }
        public static void Postfix()
        {
            var env = new DialogEnvironment()
            {
                mapScene = Tools.getScreenName()
            };
            if (   DialogAnalysis.TryTrigger(new[] { "NPC列表刷新后","AfterNpcRefreshNow" }, env, true))
            {
                Main.LogInfo("触发NPC列表刷新前触发器");
            }
            m_isRefresh = false;
        }
    }
}
