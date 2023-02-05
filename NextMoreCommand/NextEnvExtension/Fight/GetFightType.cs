using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
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