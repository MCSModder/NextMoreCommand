using SkySwordKill.Next.DialogSystem;
using YSGame.Fight;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("IsPlayerTurn")]
    public class IsPlayerTurn : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            
            return UIFightPanel.Inst.UIFightState != UIFightState.敌人回合;
        }
    }
}