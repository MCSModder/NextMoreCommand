using GUIPackage;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("GetCurrentSkill")]
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