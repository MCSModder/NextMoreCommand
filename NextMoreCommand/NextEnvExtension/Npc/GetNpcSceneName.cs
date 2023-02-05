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
            var npcId = context.GetMyArgs(0, -1);
            if (npcId <= 0)
            {
                return "";
            }

            var name = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(NPCEx.NPCIDToNew(npcId)) ?? "无";
            MyPluginMain.LogInfo(
                $"[获取场景]场景名字:{name} NpcID:{npcId.ToString()} NpcID:{DialogAnalysis.GetNpcName(npcId)} ");
            return name;
        }
    }
}