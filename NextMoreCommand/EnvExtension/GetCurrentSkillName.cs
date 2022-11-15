using GUIPackage;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("GetCurrentSkillName")]
    public class GetCurrentSkillName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            if (!context.Env.customData.ContainsKey("CurSkill"))
            {
                return string.Empty as object;
            }

            context.Env.customData.TryGetValue("CurSkill", out object cSkill);
            var curSkill=cSkill as Skill;
            if (curSkill == null)
            {
                return string.Empty as object;
            }

            var result = curSkill.skill_Name;
            return result as object;
        }
    }
}