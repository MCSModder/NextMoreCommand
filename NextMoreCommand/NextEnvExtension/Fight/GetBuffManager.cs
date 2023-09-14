using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using YSGame.Fight;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetBuffManager")]
    [DialogEnvQuery("获得BUFF管理器")]
    public class GetBuffManager : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var target =context.GetMyArgs(0, 0);
            var avatar = target == 0 ? context.Env.player : context.Env.player.OtherAvatar;
            var buffMag = avatar.buffmag;
            return buffMag;
        }
    }
}