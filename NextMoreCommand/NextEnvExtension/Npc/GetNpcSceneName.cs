using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{

    [DialogEnvQuery("GetNpcSceneName")]
    public class GetNpcSceneName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            var npcId = context.GetMyArgs(0,-1);
            if (npcId < 0)
            {
                return "";
            }
            var name = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(NPCEx.NPCIDToNew(npcId));
            Main.LogInfo($"npcId:{npcId.ToString()} name:{name}");
            return name;
        }
    }
}