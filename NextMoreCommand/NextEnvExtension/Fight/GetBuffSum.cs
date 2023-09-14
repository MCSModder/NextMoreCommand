using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using YSGame.Fight;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetBuffSum")]
    [DialogEnvQuery("获得BUFF总量")]
    public class GetBuffSum : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var buffID =context.GetMyArgs(0, 0);
            var target =context.GetMyArgs(1, 0);
            var avatar = target == 0 ? context.Env.player : context.Env.player.OtherAvatar;
            var buffMag = avatar.buffmag;
            return buffMag.GetBuffSum(buffID);
        }
    }
}