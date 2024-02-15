using Fungus;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("IsFightFeiSheng")]
    [DialogEnvQuery("飞升战斗类型")]
    public class IsFightFeiSheng : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            return Tools.instance.monstarMag.FightType == StartFight.FightEnumType.FeiSheng;
        }
    }
}