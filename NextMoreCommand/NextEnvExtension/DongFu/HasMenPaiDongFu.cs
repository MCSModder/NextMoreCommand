using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("HasMenPaiDongFu")]
    [DialogEnvQuery("检测宗门洞府")]
    public class HasMenPaiDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DongFuManager.PlayerHasDongFu(2);
        }
    }
}