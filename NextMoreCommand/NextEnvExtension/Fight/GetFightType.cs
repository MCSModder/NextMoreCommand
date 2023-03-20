using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetFightType")]
    [DialogEnvQuery("获得战斗类型")]
    public class GetFightType : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            
            return (int)Tools.instance.monstarMag.FightType;
        }
    }
}