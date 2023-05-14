using HarmonyLib;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

namespace SkySwordKill.NextMoreCommand.DialogTrigger
{
    [HarmonyPatch(typeof(UINPCJiaoHuPop))]
    public static class OnJiaoHuPop
    {
        public static UINPCData NowJiaoHuNPC => UINPCJiaoHu.Inst.NowJiaoHuNPC;
        private static DialogEnvironment GetNpcEnv
        {
            get
            {
                var npc = NowJiaoHuNPC;
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
        [HarmonyPostfix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.RefreshUI))]
        public static void RefreshUI()
        {

            DialogAnalysis.TryTrigger(new[]
            {
                "角色交互界面", "UINPCJiaoHuPop"
            }, GetNpcEnv, true);
            UINPCJiaoHuPopExtends.UiNPCJiaoHuPop.InitButton();
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnJiaoYiBtnClick))]
        public static bool OnJiaoTanBtnClick()
        {
            if (DialogAnalysis.TryTrigger(new[]
                {
                    "打断交易按钮", "BreakJiaoYiBtnClick"
                }, GetNpcEnv, true))
            {
                return false;
            }

            DialogAnalysis.TryTrigger(new[]
            {
                "点击交易按钮", "OnJiaoYiBtnClick"
            }, GetNpcEnv, true);
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnQingJiaoBtnClick))]
        public static bool OnQingJiaoBtnClick()
        {
            if (DialogAnalysis.TryTrigger(new[]
                {
                    "打断请教按钮", "BreakQingJiaoBtnClick"
                }, GetNpcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(new[]
            {
                "点击请教按钮", "OnQingJiaoBtnClick"
            }, GetNpcEnv, true);
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnJieShaBtnClick))]
        public static bool OnJieShaBtnClick()
        {
            if (DialogAnalysis.TryTrigger(new[]
                {
                    "打断截杀按钮", "BreakJieShaBtnClick"
                }, GetNpcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(new[]
            {
                "点击截杀按钮", "OnJieShaBtnClick"
            }, GetNpcEnv, true);
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnLiKaiBtnClick))]
        public static bool OnLiKaiBtnClick()
        {
            if (DialogAnalysis.TryTrigger(new[]
                {
                    "打断离开按钮", "BreakLiKaiBtnClick"
                }, GetNpcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(new[]
            {
                "点击离开按钮", "OnLiKaiBtnClick"
            }, GetNpcEnv, true);
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnLunDaoBtnClick))]
        public static bool OnLunDaoBtnClick()
        {
            if (DialogAnalysis.TryTrigger(new[]
                {
                    "打断论道按钮", "BreakLunDaoBtnClick"
                }, GetNpcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(new[]
            {
                "点击论道按钮", "OnLunDaoBtnClick"
            }, GetNpcEnv, true);
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnQieCuoBtnClick))]
        public static bool OnQieCuoBtnClick()
        {
            if (DialogAnalysis.TryTrigger(new[]
                {
                    "打断切磋按钮", "BreakQieCuoBtnClick"
                }, GetNpcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(new[]
            {
                "点击切磋按钮", "OnQieCuoBtnClick"
            }, GetNpcEnv, true);
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnSuoQuBtnClick))]
        public static bool OnSuoQuBtnClick()
        {
            if (DialogAnalysis.TryTrigger(new[]
                {
                    "打断索取按钮", "BreakSuoQuBtnClick"
                }, GetNpcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(new[]
            {
                "点击索取按钮", "OnSuoQuBtnClick"
            }, GetNpcEnv, true);
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnTanChaBtnClick))]
        public static bool OnTanChaBtnClick()
        {
            if (DialogAnalysis.TryTrigger(new[]
                {
                    "打断探查按钮", "BreakTanChaBtnClick"
                }, GetNpcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(new[]
            {
                "点击探查按钮", "OnTanChaBtnClick"
            }, GetNpcEnv, true);
            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(UINPCJiaoHuPop.OnZengLiBtnClick))]
        public static bool OnZengLiBtnClick()
        {
            if (DialogAnalysis.TryTrigger(new[]
                {
                    "打断赠礼按钮", "BreakZengLiBtnClick"
                }, GetNpcEnv, true))
            {
                return false;
            }
            DialogAnalysis.TryTrigger(new[]
            {
                "点击赠礼按钮", "OnZengLiBtnClick"
            }, GetNpcEnv, true);
            return true;
        }
    }
}