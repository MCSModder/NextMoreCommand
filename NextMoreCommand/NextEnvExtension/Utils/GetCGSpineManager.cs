using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetCGSpineManager")]
    public class GetCGSpineManager : IDialogEnvQuery
    {

        public object Execute(DialogEnvQueryContext context)
        {
            return CGSpineManager.Instance;
        }
    }
}