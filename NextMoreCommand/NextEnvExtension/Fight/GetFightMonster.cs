using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetFightMonster")]
    public class GetFightMonster : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return RoundManager.instance.GetMonstar();
        }
    }
}