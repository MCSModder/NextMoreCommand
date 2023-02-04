using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("HasNpcFollow")]
    public class HasNpcFollow : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetMyArgs(0,-1);
            return npc > 0 && NpcUtils.HasNpcFollow(npc);
        }
    }
}