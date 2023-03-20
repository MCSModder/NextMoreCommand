using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetPlayTimeMinutes")]
    [DialogEnvQuery("获得游玩分钟")]
    public class GetPlayTimeMinutes : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return PlayTimeUtils.GetMinutes();
        }
    }
}