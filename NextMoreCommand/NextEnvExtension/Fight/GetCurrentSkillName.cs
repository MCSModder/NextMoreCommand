using GUIPackage;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.DialogTrigger;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetCurrentSkillName")]
    [DialogEnvQuery("获得当前神通名字")]
    public class GetCurrentSkillName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {


            return SkillComboManager.GetSkillName(OnUseSkill.NowSkill.skill_ID) as object;
        }
    }
}