using GUIPackage;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetUseSkillID")]
    public class GetUseSkillID : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var skillID = context.GetMyArgs(0, -1);
            MyLog.FungusLog($"触发GetUseSkillID skillID:{skillID.ToString()}");
            var customData = context.Env.customData;
            customData.TryGetValue("CurSkill", out object cSkill);
            var curSkill = cSkill as Skill;
            if (cSkill == null ||curSkill == null )
            {
                return false; 
            }
          
            var skill = SkillComboManager.GetSkillId(curSkill.skill_ID);
            MyLog.FungusLog($"skillID:{skillID.ToString()} curSkill:{skill.ToString()}");
            return (skillID == skill) as object;
        }
    }
}