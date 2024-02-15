using HarmonyLib;
using SkySwordKill.Next.DialogSystem;
using YSGame.TianJiDaBi;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(Match), nameof(Match.RecordFight))]
    public static class TianJiDaBiMatch
    {
        public static  int      nowRoundIndex                     = 0;
        private static string[] _triggerTypesTianJiDaBiNpcFail    = new[] { "天机大比角色失败", "TianJiDaBiNpcFail" };
        private static string[] _triggerTypesTianJiDaBiPlayerWin  = new[] { "天机大比玩家胜利", "TianJiDaBiPlayerWin"};
        private static string[] _triggerTypesTianJiDaBiNpcWin     = new[] { "天机大比角色胜利", "TianJiDaBiNpcWin"  };
        private static string[] _triggerTypesTianJiDaBiPlayerFail = new[] { "天机大比玩家失败", "TianJiDaBiPlayerFail"  };
        public static void Postfix(int RoundIndex, DaBiPlayer win, DaBiPlayer fail)
        {
            nowRoundIndex = RoundIndex;
            var npc = new UINPCData(fail.ID);
            npc.RefreshData();
            var env = new DialogEnvironment()
            {
                bindNpc = npc,
                roleBindID = npc.ZhongYaoNPCID,
                roleID = npc.ID,
                roleName = npc.Name,
                mapScene = Tools.getScreenName()
            };


            DialogAnalysis.TryTrigger(_triggerTypesTianJiDaBiNpcFail, env, true);
            if (win.IsWanJia)
            {


                DialogAnalysis.TryTrigger(_triggerTypesTianJiDaBiPlayerWin, env, true);
            }
            npc.SetID(win.ID);
            env.bindNpc = npc;
            env.roleBindID = npc.ZhongYaoNPCID;
            env.roleID = npc.ID;
            env.roleName = npc.Name;
            DialogAnalysis.TryTrigger(_triggerTypesTianJiDaBiNpcWin, env, true);
            if (fail.IsWanJia)
            {


                DialogAnalysis.TryTrigger(_triggerTypesTianJiDaBiPlayerFail, env, true);
            }

        }
    }
}