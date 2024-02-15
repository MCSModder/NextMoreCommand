using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetFightTypeName")]
    [DialogEnvQuery("获得战斗类型名字")]
    public class GetFightTypeName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            return Tools.instance.monstarMag.FightType.GetName();
        }
    }
}