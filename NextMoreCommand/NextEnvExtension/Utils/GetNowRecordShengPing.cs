using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Patchs;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetNowRecordShengPing")]
    public class GetNowRecordShengPing : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return PlayerRecordShengPing.shengPing;
        }
    }
}