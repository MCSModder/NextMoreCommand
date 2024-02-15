using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{

    [DialogEnvQuery("GetNpcFollowDialog")]
    [DialogEnvQuery("获得跟随角色剧情ID")]
    public class GetNpcFollowDialog : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, -1);
            if (npc > 0 && NpcUtils.HasNpcFollow(npc))
            {
                return NpcUtils.GetNpcFollow(npc);
            }
            return "";
        }
    }
}