using GUIPackage;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetCurrentSkillID")]
    [DialogEnvQuery("获得当前神通ID")]
    public class GetCurrentSkillID : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            MyLog.FungusLog($"触发GetCurrentSkillID");
            var id = SkillComboManager.GetSkillId(OnUseSkill.NowSkill.SkillID);
            MyLog.FungusLog($"SkillID:{id}");
            return id  as object;
        }
    }
}