using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Custom.SkillCombo;

public class SkillCombo
{
    [JsonProperty("技能名字",Order = 0)]
    public string SkillName;
    [JsonProperty("触发类型",Order = 1)]
    public string TriggerType;
    [JsonProperty("触发条件",Order = 2)]
    public string Condition;
    [JsonProperty("技能组合",Order = 3)]
    public List<SkillComboData> SkillComboDatas = new List<SkillComboData>();
    public void Init()
    {
        var count = SkillComboDatas.Count;
        for (var i = 0; i < count; i++)
        {
            var currentSkill = SkillComboDatas[i];
            var next = i + 1;
            
            if (next < count)
            {
                var nextSkill = SkillComboDatas[next];
                currentSkill.NextSkill = nextSkill;
            }
            
            if (next == count)
            {
                currentSkill.LastSkill = true;
            }
        }
    }

    public SkillComboData First()
    {
        return SkillComboDatas.First();
    }
    public bool GetCondition(DialogEnvironment env)
    {
        if (string.IsNullOrWhiteSpace(Condition))
        {
            return false;
        }
        return DialogAnalysis.CheckCondition(Condition,env ?? new DialogEnvironment());
    }
}