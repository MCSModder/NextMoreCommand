using SkySwordKill.Next.DialogSystem;
using YSGame.Fight;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("IsMonsterTurn")]
    [DialogEnvQuery("敌方回合")]
    public class IsMonsterTurn : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            return UIFightPanel.Inst.UIFightState == UIFightState.敌人回合;
        }
    }
}