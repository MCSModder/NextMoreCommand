using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetDaolvList")]
    [DialogEnvQuery("获得道侣ID列表")]
    public class GetDaolvList : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DaolvUtils.DaolvId.ToList();
        }
    }
}