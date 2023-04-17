using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("HasCGSpineName")]
    public class HasCGSpineName:IDialogEnvQuery
    {

        public object Execute(DialogEnvQueryContext context)
        {
             
            return CGSpineManager.Instance.nowSpine == context.GetMyArgs(0,"");
        }
    }
}