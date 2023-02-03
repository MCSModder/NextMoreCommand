using Fungus;
using GUIPackage;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.Utils;
using YSGame.Fight;

namespace SkySwordKill.NextMoreCommand.EnvExtension
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