using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetNpcDefaultSkinSpine")]
    [DialogEnvQuery("获得角色默认皮肤骨骼")]
    public class GetNpcDefaultSkinSpine : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, -1);
            return NpcUtils.GetNpcDefaultSkinSpine(npc);
        }
    }
}