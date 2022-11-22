using GUIPackage;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("GetUseSkillID")]
    public class GetUseSkillID : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var skillID = context.GetArg(0, -1);
            MyLog.FungusLog($"触发GetUseSkillID skillID:{skillID.ToString()}");
            var customData = context.Env.customData;
            customData.TryGetValue("CurSkill", out object cSkill);
            MyLog.FungusLog($"cSkill:{cSkill.ToString()}");
            var curSkill = cSkill as Skill;
            MyLog.FungusLog($"curSkill:{cSkill.ToString()}");
            if (curSkill == null)
            {
                return false; 
            }
            var skill = curSkill.SkillID;
            MyLog.FungusLog($"skillID:{skillID.ToString()} curSkill:{skill.ToString()}");
            return (skillID == skill) as object;
        }
    }
}