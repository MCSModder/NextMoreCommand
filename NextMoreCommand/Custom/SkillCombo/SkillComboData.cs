using Newtonsoft.Json;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Custom.SkillCombo;
[JsonObject]
public class SkillComboData
{
    [JsonProperty("技能名字",Order = 0)]
    public string SkillName;
    [JsonProperty("触发类型",Order = 1)]
    public string TriggerType;
    [JsonProperty("触发条件",Order = 2)]
    public string Condition;
    [JsonIgnore]
    public SkillComboData NextSkill;
    
    [JsonIgnore]
    public bool LastSkill;

    public bool GetCondition(DialogEnvironment env)
    {
        if (string.IsNullOrWhiteSpace(Condition))
        {
            return false;
        }
        return DialogAnalysis.CheckCondition(Condition,env ?? new DialogEnvironment());
    }
}