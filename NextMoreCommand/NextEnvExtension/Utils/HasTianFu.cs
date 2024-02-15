using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("HasTianFu")]
    [DialogEnvQuery("检测天赋")]
    public class HasTianFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var tianfuId = context.GetMyArgs(0, -1);
            return Tools.instance.CheckHasTianFu(tianfuId) as object;
        }
    }
}