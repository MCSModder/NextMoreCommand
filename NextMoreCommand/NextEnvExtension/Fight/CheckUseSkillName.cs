using GUIPackage;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("CheckUseSkillName")]
    [DialogEnvQuery("检测使用神通名字")]
    public class CheckUseSkillName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var skillName = context.GetMyArgs(0, "");
            MyLog.FungusLog($"触发GetUseSkillID 神通名字:{skillName.ToString()}");


            var name = SkillComboManager.GetSkillName(OnUseSkill.NowSkill.skill_ID);
            MyLog.FungusLog($"神通名字:{skillName.ToString()} 当前神通名字:{name.ToString()}");
            return (skillName == name) as object;
        }
    }
}