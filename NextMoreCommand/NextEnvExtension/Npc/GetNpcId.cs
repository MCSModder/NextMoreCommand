using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{

    [DialogEnvQuery("GetNpcId")]
    [DialogEnvQuery("获得角色ID")]
    public class GetNpcId : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return context.GetNpcID(0,-1);
        }
    }
}