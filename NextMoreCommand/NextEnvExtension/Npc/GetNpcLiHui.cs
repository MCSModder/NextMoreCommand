using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetNpcLiHui")]
    public class GetNpcLiHui : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, -1);
            var data = NpcJieSuanManager.inst.GetNpcData(npc);
            if (npc > 0 && data.HasField("workshoplihui"))
            {
                return data.GetField("workshoplihui").str;
            }

            return string.Empty;
        }
    }
}