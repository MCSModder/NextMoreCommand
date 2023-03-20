using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetPlayTime")]
    [DialogEnvQuery("获得游玩时间")]
    public class GetPlayTime : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return PlayTimeUtils.GetTimeSpan();
        }
    }
}