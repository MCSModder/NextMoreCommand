using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("HasYiFengChengDongFu")]
    [DialogEnvQuery("获得逸风城洞府是否存在")]
    public class HasYiFengChengDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DongFuManager.PlayerHasDongFu(1);
        }
    }
}