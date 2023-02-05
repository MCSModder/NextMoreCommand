using Fungus;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
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