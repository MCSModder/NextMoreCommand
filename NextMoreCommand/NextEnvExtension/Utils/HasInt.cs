using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("HasInt")]
    public class HasInt : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            var key = context.GetMyArgs(0, "");

            return !string.IsNullOrWhiteSpace(key)&&context.Env.GetInt(key) != 0;
        }
    }
}