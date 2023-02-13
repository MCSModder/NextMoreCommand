using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("HasMenPaiDongFu")]
    [DialogEnvQuery("获得宗门洞府是否存在")]
    public class HasMenPaiDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DongFuManager.PlayerHasDongFu(2);
        }
    }
}