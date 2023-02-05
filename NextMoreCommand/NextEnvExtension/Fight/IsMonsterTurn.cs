using SkySwordKill.Next.DialogSystem;
using YSGame.Fight;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("IsMonsterTurn")]
    public class IsMonsterTurn : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            
            return UIFightPanel.Inst.UIFightState == UIFightState.敌人回合;
        }
    }
}