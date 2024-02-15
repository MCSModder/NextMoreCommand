using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetCGSpineObject")]
    public class GetCGSpineObject : IDialogEnvQuery
    {

        public object Execute(DialogEnvQueryContext context)
        {
            return CGSpineManager.Instance.NowSpineObject;
        }
    }
}