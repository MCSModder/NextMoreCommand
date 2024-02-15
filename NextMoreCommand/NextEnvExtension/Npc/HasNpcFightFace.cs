using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("HasNpcFightFace")]
    [DialogEnvQuery("检测角色战斗立绘")]
    public class HasNpcFightFace : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc  = context.GetNpcID(0, -1);
            var face = context.GetMyArgs(1, -1);
            var data = NpcJieSuanManager.inst.GetNpcData(npc);
            if (npc > 0)
            {
                return data.GetField("fightFace").I == face;
            }
            return false;
        }
    }
}