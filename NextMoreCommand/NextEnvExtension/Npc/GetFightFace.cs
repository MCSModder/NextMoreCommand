using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetFightFace")]
    [DialogEnvQuery("获得战斗立绘")]
    public class GetFightFace : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, -1);
            var face = context.GetMyArgs(1, -1);
            var data = NpcJieSuanManager.inst.GetNpcData(npc);
            return npc > 0 ? data.GetField("fightFace").I : 0;
        }
    }
}