using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using JetBrains.Annotations;
using JSONClass;
using KBEngine;
using SkySwordKill.Next.DialogSystem;
using YSGame.Fight;
using Skill = GUIPackage.Skill;

namespace SkySwordKill.NextMoreCommand.Custom.SkillCombo;

public static class SkillComboManager
{
    public static readonly Dictionary<string, SkillCombo> SkillCombos = new Dictionary<string, SkillCombo>();
    public static readonly List<CacheSkillCombo> CacheSkillCombos = new List<CacheSkillCombo>();
    private static Tools Tools => Tools.instance;
    private static Avatar Player => Tools.getPlayer();
    private static List<UIFightSkillItem> SkillFight => UIFightPanel.Inst.FightSkills;

    public static int GetSkillId(int skill)
    {
        return Tools.getSkillKeyByID(skill, Player);
    }

    public static int GetSkillKey(int skill)
    {
        return Tools.getSkillIDByKey(skill);
    }

    public static int GetSkillId(string skillName)
    {
        var skill = _skillJsonData.DataList.First((data => GetSkillName(data.Skill_ID) == skillName));
        return skill.Skill_ID;
    }

    public static int GetSkillKey(string skillName)
    {
        return GetSkillKey(GetSkillId(skillName));
    }

    public static string GetSkillName(int skill, bool includeColor = false)
    {
        return Tools.getSkillName(skill, includeColor);
    }

    public static bool ReplaceSkill(string oldSkill, string newSkill)
    {
        return ReplaceSkill(GetSkillId(oldSkill), GetSkillId(newSkill));
    }

    public static bool ReplaceSkill(int oldSkill, int newSkill)
    {
        var index = GetSkillIndex(oldSkill);
        if (index < 0) return false;
        Player.FightClearSkill(index, 1);
        Player.FightAddSkill(newSkill, index, 10);
        return true;
    }

    public static int GetSkillIndex(string oldSkill)
    {
        return GetSkillIndex(GetSkillId(oldSkill));
    }

    public static int GetSkillIndex(int oldSkill)
    {
        var index = 0;
        foreach (var uiFightSkillItem in SkillFight)
        {
            if (uiFightSkillItem.HasSkill)
            {
                var skill = Traverse.Create(uiFightSkillItem).Field<Skill>("nowSkill");
                if (skill.Value.SkillID == oldSkill)
                {
                    return index;
                }
            }

            index++;
        }

        return -1;
    }

    public static bool HasSkill(string skill)
    {
        var index = GetSkillIndex(skill);

        if (index < 0 || !SkillCombos.ContainsKey(skill) || SkillCombos[skill].SkillComboDatas.Count == 0)
        {
            return false;
        }

        var skillCombo = SkillCombos[skill];
        CacheSkillCombos.Add(CacheSkillCombo.Create(index, skillCombo, skillCombo.First()));
        return true;
    }
    public static bool TryTriggerSkill(DialogEnvironment env, bool triggerAll, params string[] param)
    {
        var flag = false;
        foreach (var data in SkillCombos)
        {
            var key = data.Key;
            var skillCombo = data.Value;
            if (param.Contains(skillCombo.TriggerType) && skillCombo.GetCondition(env) )
            {
                HasSkill(key);
                if (!triggerAll) return true;
                flag = true;
            }
        }
        return flag;
    }

    public static bool TryTrigger(DialogEnvironment env, bool triggerAll, params string[] param)
    {
        var flag = false;
        foreach (var skillCombo in CacheSkillCombos)
        {
            var triggerType = skillCombo.SkillComboData.TriggerType;
            var isLastSkill = skillCombo.SkillComboData.LastSkill;
            var isTrigger = param.Contains(triggerType) && skillCombo.SkillComboData.GetCondition(env);
            if (isTrigger && !isLastSkill)
            {
                var oldSkill = skillCombo.SkillComboData;
                var newSkill = oldSkill.NextSkill;
                ReplaceSkill(oldSkill.SkillName, newSkill.SkillName);
                CacheSkillCombos.Remove(skillCombo);
                CacheSkillCombos.Add(CacheSkillCombo.Create(skillCombo.Index, skillCombo.SkillCombo, newSkill));
                if (!triggerAll) return true;
                flag = true;
            }
        }
        return flag;
    }
}