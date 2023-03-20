using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetCurrentSkill")]
    [DialogEnvQuery("获得当前神通")]
    public class GetCurrentSkill : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            if (!context.Env.customData.ContainsKey("CurSkill"))
            {
                return null;
            }
            
            context.Env.customData.TryGetValue("CurSkill", out object cSkill);
            return cSkill;
        }
    }
}