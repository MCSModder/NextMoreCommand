using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("HasNpcLiHui")]
    [DialogEnvQuery("检测角色立绘")]
    public class HasNpcLiHui : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, -1);
            return npc > 0 && NpcJieSuanManager.inst.GetNpcData(npc).HasField("workshoplihui");
        }
    }
}