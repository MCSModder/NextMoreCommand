using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetFightMonster")]
    [DialogEnvQuery("获得敌方信息")]
    public class GetFightMonster : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return RoundManager.instance.GetMonstar();
        }
    }
}