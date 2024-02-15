using HarmonyLib;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

namespace SkySwordKill.NextMoreCommand.DialogTrigger
{
    [HarmonyPatch(typeof(UINPCJiaoHuPop))]
    public static class OnJiaoHuPop
    {
        public static UINPCData NowJiaoHuNpc => UINPCJiaoHu.Inst.NowJiaoHuNPC;
        private static DialogEnvironment GetNpcEnv
        {
            get
            {
                var npc = NowJiaoHuNpc;
                return new DialogEnvironment()
                {
                    bindNpc = npc,
                    roleBindID = npc.ZhongYaoNPCID,
                    roleID = npc.ID,
                    roleName = npc.Name,
                    mapScene = Tools.getScreenName()
                };
            }
        }
        private static string[] _triggerTypeUinpcJiaoHuPop = new[] { "角色交互界面", "UINPCJiaoHuPop" };
        [HarmonyPostfix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.RefreshUI))]
        public static void RefreshUI()
        {

            DialogAnalysis.TryTrigger(_triggerTypeUinpcJiaoHuPop, GetNpcEnv, true);
            UINPCJiaoHuPopExtends.UiNPCJiaoHuPop.InitButton();
        }
        private static string[] _triggerTypeBreakJiaoYiBtnClick = new[] { "打断交易按钮", "BreakJiaoYiBtnClick" };

        private static string[] _triggerTypeOnJiaoYiBtnClick = new[] { "点击交易按钮", "OnJiaoYiBtnClick" };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnJiaoYiBtnClick))]
        public static bool OnJiaoTanBtnClick()
        {
            var npcEnv = GetNpcEnv;
            if (DialogAnalysis.TryTrigger(_triggerTypeBreakJiaoYiBtnClick, npcEnv, true))
            {
                return false;
            }

            DialogAnalysis.TryTrigger(_triggerTypeOnJiaoYiBtnClick, npcEnv, true);
            return true;
        }
        private static string[] _triggerTypeBreakQingJiaoBtnClick = new[] { "打断请教按钮", "BreakQingJiaoBtnClick" };

        private static string[] _triggerTypeOnQingJiaoBtnClick = new[] { "点击请教按钮", "OnQingJiaoBtnClick" };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnQingJiaoBtnClick))]
        public static bool OnQingJiaoBtnClick()
        {
            var npcEnv = GetNpcEnv;
            if (DialogAnalysis.TryTrigger(_triggerTypeBreakQingJiaoBtnClick, npcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(_triggerTypeOnQingJiaoBtnClick, npcEnv, true);
            return true;
        }
        private static string[] _triggerTypeBreakJieShaBtnClick = new[] { "打断截杀按钮", "BreakJieShaBtnClick" };

        private static string[] _triggerTypeOnJieShaBtnClick = new[] { "点击截杀按钮", "OnJieShaBtnClick" };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnJieShaBtnClick))]
        public static bool OnJieShaBtnClick()
        {
            var npcEnv = GetNpcEnv;
            if (DialogAnalysis.TryTrigger(_triggerTypeBreakJieShaBtnClick, npcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(_triggerTypeOnJieShaBtnClick, npcEnv, true);
            return true;
        }
        private static string[] _triggerTypeBreakLiKaiBtnClick = new[] { "打断离开按钮", "BreakLiKaiBtnClick" };

        private static string[] _triggerTypeOnLiKaiBtnClick = new[] { "点击离开按钮", "OnLiKaiBtnClick" };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnLiKaiBtnClick))]
        public static bool OnLiKaiBtnClick()
        {
            var npcEnv = GetNpcEnv;
            if (DialogAnalysis.TryTrigger(_triggerTypeBreakLiKaiBtnClick, npcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(_triggerTypeOnLiKaiBtnClick, npcEnv, true);
            return true;
        }
        private static string[] _triggerTypeBreakLunDaoBtnClick = new[] { "打断论道按钮", "BreakLunDaoBtnClick" };

        private static string[] _triggerTypeOnLunDaoBtnClick = new[] { "点击论道按钮", "OnLunDaoBtnClick" };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnLunDaoBtnClick))]
        public static bool OnLunDaoBtnClick()
        {
            var npcEnv = GetNpcEnv;
            if (DialogAnalysis.TryTrigger(_triggerTypeBreakLunDaoBtnClick, npcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(_triggerTypeOnLunDaoBtnClick, npcEnv, true);
            return true;
        }
        private static string[] _triggerTypeBreakQieCuoBtnClick = new[] { "打断切磋按钮", "BreakQieCuoBtnClick" };

        private static string[] _triggerTypeOnQieCuoBtnClick = new[] { "点击切磋按钮", "OnQieCuoBtnClick" };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnQieCuoBtnClick))]
        public static bool OnQieCuoBtnClick()
        {
            var npcEnv = GetNpcEnv;
            if (DialogAnalysis.TryTrigger(_triggerTypeBreakQieCuoBtnClick, npcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(_triggerTypeOnQieCuoBtnClick, npcEnv, true);
            return true;
        }
        private static string[] _triggerTypeBreakSuoQuBtnClick = new[] { "打断索取按钮", "BreakSuoQuBtnClick" };

        private static string[] _triggerTypeOnSuoQuBtnClick = new[] { "点击索取按钮", "OnSuoQuBtnClick" };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnSuoQuBtnClick))]
        public static bool OnSuoQuBtnClick()
        {
            var npcEnv = GetNpcEnv;
            if (DialogAnalysis.TryTrigger(_triggerTypeBreakSuoQuBtnClick, npcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(_triggerTypeOnSuoQuBtnClick, npcEnv, true);
            return true;
        }
        private static string[] _triggerTypeBreakTanChaBtnClick = new[] { "打断探查按钮", "BreakTanChaBtnClick" };

        private static string[] _triggerOnTanChaBtnClick = new[] { "点击探查按钮", "OnTanChaBtnClick" };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnTanChaBtnClick))]
        public static bool OnTanChaBtnClick()
        {
            var npcEnv = GetNpcEnv;
            if (DialogAnalysis.TryTrigger(_triggerTypeBreakTanChaBtnClick, npcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(_triggerOnTanChaBtnClick, npcEnv, true);
            return true;
        }
        private static string[] _triggerTypeBreakZengLiBtnClick = new[] { "打断赠礼按钮", "BreakZengLiBtnClick" };

        private static string[] _triggerTypeOnZengLiBtnClick = new[] { "点击赠礼按钮", "OnZengLiBtnClick" };
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnZengLiBtnClick))]
        public static bool OnZengLiBtnClick()
        {
            var npcEnv = GetNpcEnv;
            if (DialogAnalysis.TryTrigger(_triggerTypeBreakZengLiBtnClick, npcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(_triggerTypeOnZengLiBtnClick, npcEnv, true);
            return true;
        }
    }
}