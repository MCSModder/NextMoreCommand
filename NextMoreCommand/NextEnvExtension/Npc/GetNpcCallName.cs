using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetNpcCallName")]
    [DialogEnvQuery("获得角色称呼")]
    public class GetNpcCallName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npcId = context.GetNpcID(0, -1);
            if (npcId <= 0)
            {
                return "";
            }

            var name = NpcUtils.GetCallName(npcId);
            MyPluginMain.LogInfo($"npcId:{npcId.ToString()} 称呼:{name}");
            return name;
        }
    }
}