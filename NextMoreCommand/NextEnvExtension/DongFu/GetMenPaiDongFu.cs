using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("GetMenPaiDongFu")]
    [DialogEnvQuery("获得宗门洞府信息")]
    public class GetMenPaiDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DongFuUtils.GetDongFuData(2);
        }
    }
}