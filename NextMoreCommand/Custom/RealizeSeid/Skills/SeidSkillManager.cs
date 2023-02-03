using System.Collections.Generic;

namespace SkySwordKill.NextMoreCommand.Custom.RealizeSeid.Skills;

public static class SeidSkillManager
{
    public static readonly Dictionary<string, SeidSkill> SeidSkills = new Dictionary<string, SeidSkill>();

    public static bool TryGet(string key, out SeidSkill seidSkill)
    {
        return SeidSkills.TryGetValue(key, out seidSkill);
    }
}