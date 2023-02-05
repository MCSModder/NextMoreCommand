using GUIPackage;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
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

            context.Env.customData.TryGetValue("CurSkill", out var cSkill);
            var curSkill=cSkill as Skill;
            if (curSkill == null)
            {
                return string.Empty as object;
            }

    
            return SkillComboManager.GetSkillName(curSkill.skill_ID) as object;
        }
    }
}