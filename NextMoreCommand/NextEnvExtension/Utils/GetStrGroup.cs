using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetStrGroup")]
    public class GetStrGroup : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var group = context.GetMyArgs(0, "");
            var key   = context.GetMyArgs(1, "");
            return DialogAnalysis.GetStr(group, key);
        }
    }
}