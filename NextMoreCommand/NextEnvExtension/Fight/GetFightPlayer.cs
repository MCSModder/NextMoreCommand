using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetFightPlayer")]
    public class GetFightPlayer : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return PlayerEx.Player;
        }
    }
}