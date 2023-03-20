using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("HasDaolv")]
    [DialogEnvQuery("检测道侣")]
    public class HasDaolv : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return PlayerEx.IsDaoLv(context.GetNpcID(0, -1));
        }
    }
}