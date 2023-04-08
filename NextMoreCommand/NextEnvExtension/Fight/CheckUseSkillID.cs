using GUIPackage;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("CheckUseSkillID")]
    [DialogEnvQuery("检测使用神通ID")]
    public class GetUseSkillID : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var skillID = context.GetMyArgs(0, -1);
            MyLog.FungusLog($"触发GetUseSkillID skillID:{skillID.ToString()}");

            var skill = SkillComboManager.GetSkillId(OnUseSkill.NowSkill.skill_ID);
            MyLog.FungusLog($"skillID:{skillID.ToString()} curSkill:{skill.ToString()}");
            return (skillID == skill) as object;
        }
    }
}