using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight;

[DialogEnvQuery("GetStaticSkillLevel")]
[DialogEnvQuery("获得功法等级")]
public class GetStaticSkillLevel : IDialogEnvQuery
{
    public object Execute(DialogEnvQueryContext context)
    {
        var skillID = context.GetMyArgs(0, -1);
        if (!context.Env.HasStaticSkill(skillID))
        {
            return 0;
        }

        var staticSkillIDByKey = Tools.instance.getStaticSkillIDByKey(skillID);
        var list = Tools.instance.getPlayer().hasStaticSkillList;
        foreach (var skillItem in list)
        {
            if (staticSkillIDByKey == skillItem.itemId) return skillItem.level;
        }

        return 0;
    }
}