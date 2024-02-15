using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetNpcType")]
    public class GetNpcType : IDialogEnvQuery
    {


        public object Execute(DialogEnvQueryContext context)
        {
            var npc  = context.GetNpcID(0);
            var json = npc.NPCJson();

            return json.HasField("Type") ? json.GetField("Type").I.GetTypeName() : "";
        }
    }
}