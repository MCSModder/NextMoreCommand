using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{

    [DialogEnvQuery("GetNpcId")]
    public class GetNpcId : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return NPCEx.NPCIDToNew( context.GetMyArgs(0,-1));
        }
    }
}