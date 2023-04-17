using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetCGSpineName")]
    public class GetCGSpineName:IDialogEnvQuery
    {

        public object Execute(DialogEnvQueryContext context)
        {
            return CGSpineManager.Instance.nowSpine;
        }
    }
}