using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetPlayerNames")]
    [DialogEnvQuery("获得玩家名字及感叹号")]
    public class GetPlayerNames : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var player   = context.Env.player;
            var fullname = player.firstName + player.lastName;
            return fullname.TextHelper(1, @char => @char + "!");

        }
    }
}