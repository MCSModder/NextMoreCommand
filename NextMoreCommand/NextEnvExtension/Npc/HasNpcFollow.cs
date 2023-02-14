using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{

    [DialogEnvQuery("HasNpcFollow")]
    public class HasNpcFollow : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0,-1);
            return npc > 0 && NpcUtils.HasNpcFollow(npc);
        }
    }
}