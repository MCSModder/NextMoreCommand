using KBEngine;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension;


    [DialogEnvQuery("GetStaticSkillLevel")]
    public class GetStaticSkillLevel : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var skillID = context.GetMyArgs(0, -1);
            if (!context.Env.HasStaticSkill(skillID))
            {
                return 0 as object;
            }
            int staticSkillIDByKey = Tools.instance.getStaticSkillIDByKey(skillID);
            int level = 0;
            foreach (SkillItem skillItem in Tools.instance.getPlayer().hasStaticSkillList)
            {
                if (staticSkillIDByKey == skillItem.itemId)
                {
                    level = skillItem.level;
                    break;
                }
            }
            return level as object; ;
        }
    }
