using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("GetNowDongFuId")]
    [DialogEnvQuery("获得当前洞府ID")]
    public class GetNowDongFuId : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DongFuManager.NowDongFuID;
        }
    }
}