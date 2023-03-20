using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetIntGroup")]
    [DialogEnvQuery("获得整数群")]
    public class GetIntGroup : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var group = context.GetMyArgs(0,"");
            var key = context.GetMyArgs(1,"");
            return DialogAnalysis.GetInt(group,key);
        }
    }
}
