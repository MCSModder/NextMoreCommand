using GUIPackage;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension
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