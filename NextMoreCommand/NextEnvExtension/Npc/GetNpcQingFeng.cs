using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{

    [DialogEnvQuery("GetNpcQingFeng")]
    [DialogEnvQuery("获得角色情分")]
    public class GetNpcQingFeng : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return NPCEx.GetQingFen(context.GetNpcID(0,-1));
        }
    }
}