using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetPlayTimeHours")]
    [DialogEnvQuery("获得游玩小时")]
    public class GetPlayTimeHours : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return PlayTimeUtils.GetHours();
        }
    }
}