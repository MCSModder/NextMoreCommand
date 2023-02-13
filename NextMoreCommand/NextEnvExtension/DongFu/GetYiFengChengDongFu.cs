using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("GetYiFengChengDongFu")]
    [DialogEnvQuery("获得逸风城洞府信息")]
    public class GetYiFengChengDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DongFuUtils.GetDongFuData(1);
        }
    }
}