using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("GetAllDongFu")]
    [DialogEnvQuery("获得所有洞府信息")]
    public class GetAllDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DongFuUtils.DongFuInfo;
        }
    }
}