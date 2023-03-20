using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetNpcSceneName")]
    [DialogEnvQuery("获得角色场景名字")]
    public class GetNpcSceneName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npcId = context.GetNpcID(0, -1);
            if (npcId <= 0)
            {
                return "";
            }

            var name = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId) ?? "无";
            // MyPluginMain.LogInfo($"[获取场景]场景名字:{name} NpcID:{npcId.ToString()} NpcID:{DialogAnalysis.GetNpcName(npcId)} ");
            return name;
        }
    }
}