using Fungus;
using GUIPackage;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("IsFightFeiSheng")]
    public class IsFightFeiSheng : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            
            return Tools.instance.monstarMag.FightType == StartFight.FightEnumType.FeiSheng;
        }
    }
}