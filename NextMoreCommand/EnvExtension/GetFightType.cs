using GUIPackage;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("GetFightType")]
    public class GetFightType : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            
            return (int)Tools.instance.monstarMag.FightType;
        }
    }
}