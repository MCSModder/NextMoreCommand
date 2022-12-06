using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("GetNpcName")]
    public class GetNpcName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            var npcId = context.GetArg(0,-1);
            if (npcId < 0)
            {
                return "";
            }
            var name = DialogAnalysis.GetNpcName(NPCEx.NPCIDToNew(npcId));
            Main.LogInfo($"npcId:{npcId.ToString()} name:{name}");
            return name;
        }
    }
}