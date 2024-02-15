using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.StaticFace;

namespace SkySwordKill.NextMoreCommand.Custom.SkillCombo;

public class SkillCombo
{
    [JsonProperty("技能名字", Order = 0)]
    public string SkillName;
    [JsonProperty("触发类型", Order = 1)]
    public string TriggerType;
    [JsonProperty("触发条件", Order = 2)]
    public string Condition;
    [JsonProperty("剧情事件", Order = 3)]
    public string DialogEvent;
    [JsonProperty("执行Lua", Order = 4)]
    public string RunLua;
    [JsonProperty("战斗立绘", Order = 5)]
    public int CustomFace = -1;
    [JsonProperty("技能组合", Order = 6)]
    public List<SkillComboData> SkillComboDatas = new List<SkillComboData>();
    public void Init()
    {
        var count = SkillComboDatas.Count;
        for (var i = 0; i < count; i++)
        {
            var currentSkill = SkillComboDatas[i];
            var next         = i + 1;

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
        return SkillComboDatas[0];
    }
    public bool GetCondition(DialogEnvironment env)
    {
        if (string.IsNullOrWhiteSpace(Condition))
        {
            return false;
        }
        return DialogAnalysis.CheckCondition(Condition, env ?? new DialogEnvironment());
    }
    public bool GetDialogEvent(DialogEnvironment env)
    {
        if (string.IsNullOrWhiteSpace(DialogEvent) || !DialogAnalysis.DialogDataDic.ContainsKey(DialogEvent))
        {
            return false;
        }
        DialogAnalysis.StartDialogEvent(DialogEvent, env);
        return true;
    }
    public bool GetCustomFace(DialogEnvironment env)
    {
        if (StaticFaceUtils.HasFace(CustomFace) || CustomFace == 0)
        {
            DialogAnalysis.StartTestDialogEvent($"SetFightCustomFace*{CustomFace}", env);
            return true;
        }


        return false;
    }
    public bool GetRunLua(DialogEnvironment env)
    {
        if (string.IsNullOrWhiteSpace(RunLua))
        {
            return false;
        }
        DialogAnalysis.StartTestDialogEvent($"RunLua*{RunLua}", env);
        return true;
    }
}