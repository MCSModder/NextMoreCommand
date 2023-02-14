using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetNpcFace")]
    public class GetNpcFace : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, -1);
            var data = NpcJieSuanManager.inst.GetNpcData(npc);
            if (npc > 0 && data.HasField("face"))
            {
                return data.GetField("face").I;
            }

            return -1;
        }
    }
}