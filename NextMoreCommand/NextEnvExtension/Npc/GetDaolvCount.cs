using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetDaolvCount")]
    [DialogEnvQuery("获得道侣数量")]
    public class GetDaolvCount : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DaolvUtils.DaolvId.Count;
        }
    }
}