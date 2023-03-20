using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetFightPlayer")]
    [DialogEnvQuery("获得我方信息")]
    public class GetFightPlayer : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return PlayerEx.Player;
        }
    }
}