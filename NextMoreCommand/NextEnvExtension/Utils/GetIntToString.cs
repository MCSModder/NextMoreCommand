using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetIntToString")]
    [DialogEnvQuery("获得整数转字符串")]
    public class GetIntToString : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var key = context.GetMyArgs(0,"");
            return DialogAnalysis.GetInt(key).ToString();
        }
    }
}
