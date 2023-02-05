using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetDaolvList")]
    public class GetDaolvList : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DaolvUtils.DaolvId.ToList();
        }
    }
}