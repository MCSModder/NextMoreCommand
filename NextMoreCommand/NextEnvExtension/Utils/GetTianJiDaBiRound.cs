using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Patchs;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetTianJiDaBiRound")]
    public class GetTianJiDaBiRound : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return TianJiDaBiMatch.nowRoundIndex;
        }
    }
}