using GUIPackage;
using SkySwordKill.Next.DialogSystem;
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
            var customData = context.Env.customData;
            customData.TryGetValue("CurSkill", out var cSkill);
            var curSkill=cSkill as Skill;
            if (curSkill == null)
            {
                return -1;
            }
            MyLog.FungusLog($"SkillID:{curSkill.SkillID}");
            return curSkill.SkillID  as object;
        }
    }
}