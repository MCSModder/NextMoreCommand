using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{

    [DialogEnvQuery("GetNpcFollowDialog")]
    public class GetNpcFollowDialog : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetMyArgs(0,-1);
            if (npc > 0 && NpcUtils.HasNpcFollow(npc))
            {
                return NpcUtils.GetNpcFollow(npc);
            }
            return "";
        }
    }
}