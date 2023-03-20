using SkySwordKill.Next.DialogSystem;
using YSGame.Fight;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("IsPlayerTurn")]
    [DialogEnvQuery("我方回合")]
    public class IsPlayerTurn : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            
            return UIFightPanel.Inst.UIFightState != UIFightState.敌人回合;
        }
    }
}