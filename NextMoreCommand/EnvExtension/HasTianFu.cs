using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("HasTianFu")]
    public class HasTianFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var tianfuId = context.GetMyArgs(0,-1);
            return Tools.instance.CheckHasTianFu(tianfuId) as object;
        }
    }
}