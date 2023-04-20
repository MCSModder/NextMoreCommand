using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("ToCNNumber")]
    public class ToCNNumber : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return context.GetMyArgs(0, 0).ToCNNumber();

        }
    }
}