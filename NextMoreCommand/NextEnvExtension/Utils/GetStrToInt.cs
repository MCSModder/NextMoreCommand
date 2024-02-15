using System;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetStrToInt")]
    public class GetStrToInt : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var key = context.GetMyArgs(0, "");
            return Convert.ToInt32(DialogAnalysis.GetStr(key));
        }
    }
}