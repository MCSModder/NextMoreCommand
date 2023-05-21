using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("HasNpcDefaultSkinSpine")]
    [DialogEnvQuery("检测角色默认皮肤骨骼")]
    public class HasNpcDefaultSkinSpine : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, -1);


            return NpcUtils.HasNpcDefaultSkinSpine(npc);
        }
    }
}