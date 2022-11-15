using GUIPackage;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("GetCurrentSkillID")]
    public class GetCurrentSkillID : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            if (!context.Env.customData.ContainsKey("CurSkill"))
            {
                return -1 as object;
            }

            context.Env.customData.TryGetValue("CurSkill", out object cSkill);
            var curSkill=cSkill as Skill;
            if (curSkill == null)
            {
                return -1 as object;
            }

            var result = curSkill.SkillID;
            return result as object;
        }
    }
}