using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("HasDongFu")]
    [DialogEnvQuery("检测洞府")]
    public class HasDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            
            var dongfuID = context.GetMyArgs(0, 0);
            return DongFuManager.PlayerHasDongFu(dongfuID);
        }
    }
}