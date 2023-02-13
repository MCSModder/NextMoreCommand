using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("GetNowDongFu")]
    [DialogEnvQuery("获得当前洞府信息")]
    public class GetNowDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DongFuUtils.GetDongFuData(DongFuManager.NowDongFuID);
        }
    }
}