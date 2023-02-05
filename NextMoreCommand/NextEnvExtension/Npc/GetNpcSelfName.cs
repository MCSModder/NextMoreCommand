using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{

    [DialogEnvQuery("GetNpcSelfName")]
    public class GetNpcSelfName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            var npcId = context.GetMyArgs(0,-1);
            if (npcId <= 0)
            {
                return "";
            }
            var name = NpcUtils.GetSelfName(NPCEx.NPCIDToNew(npcId));
           MyPluginMain.LogInfo($"npcId:{npcId.ToString()} 自称:{name}");
            return name;
        }
    }
}