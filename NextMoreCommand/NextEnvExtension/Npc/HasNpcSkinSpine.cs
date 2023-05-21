using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("HasNpcSkinSpine")]
    [DialogEnvQuery("检测角色皮肤骨骼")]
    public class HasNpcSkinSpine : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, -1);
            string skin;
            switch (context.Args.Length)
            {
                case 1:
                    skin = NpcUtils.GetNpcSkinSpine(npc);
                    return AssetsUtils.CheckSkin(npc, skin);
                case 2:
                    skin = context.GetMyArgs<string>(1);
                    return AssetsUtils.CheckSkin(npc, skin);
            }


            return false;
        }
    }
}