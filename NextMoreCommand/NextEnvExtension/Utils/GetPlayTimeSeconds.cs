using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetPlayTimeSeconds")]
    [DialogEnvQuery("获得游玩秒数")]
    public class GetPlayTimeSeconds : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return PlayTimeUtils.GetSeconds();
        }
    }
}