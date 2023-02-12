using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.DongFu
{
    [DialogEnvQuery("GetDongFu")]
    [DialogEnvQuery("获得洞府信息")]
    public class GetDongFu : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            
            var dongfuID = context.GetMyArgs(0, 0);
            return DongFuUtils.GetDongFuData(dongfuID);
        }
    }
}