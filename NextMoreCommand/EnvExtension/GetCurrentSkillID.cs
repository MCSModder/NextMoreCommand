using GUIPackage;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("GetCurrentSkillID")]
    public class GetCurrentSkillID : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            MyLog.FungusLog($"触发GetCurrentSkillID");
            var customData = context.Env.customData;
            customData.TryGetValue("CurSkill", out object cSkill);
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