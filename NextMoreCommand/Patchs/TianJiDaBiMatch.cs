using HarmonyLib;
using SkySwordKill.Next.DialogSystem;
using YSGame.TianJiDaBi;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(Match),nameof(Match.RecordFight))]
    public static class TianJiDaBiMatch
    {
        public static int nowRoundIndex = 0;
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


            DialogAnalysis.TryTrigger(new[]
            {
                "天机大比角色失败", "TianJiDaBiNpcFail"
            }, env, true);
            if (win.IsWanJia)
            {


                DialogAnalysis.TryTrigger(new[]
                {
                    "天机大比玩家胜利", "TianJiDaBiPlayerWin"
                }, env, true);
            }
            npc.SetID(win.ID);
            env.bindNpc = npc;
            env.roleBindID = npc.ZhongYaoNPCID;
            env.roleID = npc.ID;
            env.roleName = npc.Name;
            DialogAnalysis.TryTrigger(new[]
            {
                "天机大比角色胜利", "TianJiDaBiNpcWin"
            }, env, true);
            if (fail.IsWanJia)
            {


                DialogAnalysis.TryTrigger(new[]
                {
                    "天机大比玩家失败", "TianJiDaBiPlayerFail"
                }, env, true);
            }

        }
    }
}