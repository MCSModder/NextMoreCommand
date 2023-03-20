using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("HasYiFengChengDongFu")]
    [DialogEnvQuery("检测逸风城洞府")]
    public class HasYiFengChengDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DongFuManager.PlayerHasDongFu(1);
        }
    }
}