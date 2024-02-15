using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using JSONClass;
using KBEngine;
using SkySwordKill.Next.DialogSystem;
using YSGame.Fight;
using Skill = GUIPackage.Skill;

namespace SkySwordKill.NextMoreCommand.Custom.SkillCombo;

public class SkillInfoReplace
{
    public readonly int    OldSkillId;
    public readonly int    NewSkillId;
    public readonly int    Index;
    public readonly string OldSkillName;
    public readonly string NewSkillName;

    public SkillInfoReplace(int oldSkill, int newSkill)
    {
        OldSkillId = oldSkill;
        NewSkillId = newSkill;
        OldSkillName = SkillComboManager.GetSkillName(oldSkill);
        NewSkillName = SkillComboManager.GetSkillName(newSkill);
        Index = SkillComboManager.GetSkillIndex(oldSkill);
    }

    public SkillInfoReplace(string oldSkill, string newSkill)
    {
        OldSkillId = SkillComboManager.GetSkillId(oldSkill);
        NewSkillId = SkillComboManager.GetSkillId(newSkill);
        OldSkillName = oldSkill;
        NewSkillName = newSkill;
        Index = SkillComboManager.GetSkillIndex(OldSkillId);
    }

    public bool IsValid()
    {
        return OldSkillId > 0 && NewSkillId > 0 && Index is <= 10 and >= 0;
    }

    public void SetReplace()
    {
        if (SkillComboManager.ReplaceSkill(this))
        {
            GetInfo();
        }
        else
        {
            GetError();
        }
    }

    public void GetError()
    {
        MyPluginMain.LogError($"[技能替换失败]老技能:{OldSkillName} 新技能:{NewSkillName} 位置:{Index}");
        MyPluginMain.LogError($"[技能替换失败]老技能:{OldSkillId} 新技能:{NewSkillId} 位置:{Index}");
    }

    public void GetInfo()
    {
        MyPluginMain.LogInfo($"[技能替换]老技能:{OldSkillName} 新技能:{NewSkillName} 位置:{Index}");
        MyPluginMain.LogInfo($"[技能替换]老技能:{OldSkillId} 新技能:{NewSkillId} 位置:{Index}");
    }
}

public static class SkillComboManager
{
    public static readonly Dictionary<string, SkillCombo> SkillCombos      = new Dictionary<string, SkillCombo>();
    public static readonly Dictionary<string, int>        SkillName        = new Dictionary<string, int>();
    public static readonly List<CacheSkillCombo>          CacheSkillCombos = new List<CacheSkillCombo>();
    public static          Skill                          ChoiceSkill => RoundManager.instance.ChoiceSkill ?? RoundManager.instance.CurSkill;
    private static         Tools                          Tools       => Tools.instance;
    private static         Avatar                         Player      => Tools.getPlayer();
    private static         List<UIFightSkillItem>         SkillFight  => UIFightPanel.Inst.FightSkills;

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
        if (SkillName.TryGetValue(skillName, out var value))
        {
            return GetSkillId(value);
        }

        foreach (var item in _skillJsonData.DataList)
        {
            if (item.name.RemoveNumber() == skillName)
            {
                MyPluginMain.LogInfo($"技能名字:{item.name} 技能ID:{item.id} 技能ID:{item.Skill_ID}");
                SkillName[skillName] = item.Skill_ID;
                return GetSkillId(SkillName[skillName]);
            }
        }

        return -1;
    }

    public static int GetSkillKey(string skillName)
    {
        var id = GetSkillId(skillName);
        return GetSkillKey(GetSkillId(id));
    }

    public static string GetSkillName(int skill, bool includeColor = false)
    {
        MyPluginMain.LogInfo($"skill:{skill}");

        return skill < 0 ? "" : Tools.getSkillName(skill, includeColor);
    }

    public static bool ReplaceSkill(string oldSkill, string newSkill)
    {
        MyPluginMain.LogInfo($"替换技能 老:{oldSkill} 新:{newSkill}");
        return ReplaceSkill(GetSkillId(oldSkill), GetSkillId(newSkill));
    }

    public static bool ReplaceSkill(SkillInfoReplace skillInfoReplace)
    {
        return ReplaceSkill(skillInfoReplace.OldSkillId, skillInfoReplace.NewSkillId, skillInfoReplace.Index);
    }

    public static bool ReplaceSkill(string oldSkill, string newSkill, int index)
    {
        MyPluginMain.LogInfo($"替换技能 老:{oldSkill} 新:{newSkill} index:{index}");
        return ReplaceSkill(GetSkillId(oldSkill), GetSkillId(newSkill), index);
    }

    public static bool ReplaceSkill(int oldSkill, int newSkill)
    {
        var index = GetSkillIndex(oldSkill);
        return ReplaceSkill(oldSkill, newSkill, index);
    }

    public static bool ReplaceSkill(int oldSkill, int newSkill, int index)
    {
        if (index < 0) return false;
        MyPluginMain.LogInfo($"成功替换技能 老:{oldSkill} 新:{newSkill} index:{index}");
        Player.FightClearSkill(index, index + 1);
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
                if (skill.Value.SkillID == GetSkillKey(oldSkill))
                {
                    return index;
                }
            }

            index++;
        }

        return -1;
    }

    public static bool SetSkill(string skill, int index)
    {
        var skillCombo = SkillCombos[skill];
        CacheSkillCombos.Add(CacheSkillCombo.Create(index, skillCombo, skillCombo.First()));
        ReplaceSkill(skillCombo.SkillName, skillCombo.First().SkillName, index);
        return true;
    }

    private static bool IsMonsterTurn => UIFightPanel.Inst.UIFightState == UIFightState.敌人回合;

    public static bool TryTriggerSkill(DialogEnvironment env, bool triggerAll, string[] param)
    {
        if (!RoundManager.instance || ChoiceSkill == null || IsMonsterTurn) return false;
        MyPluginMain.LogInfo($"[进入触发技能组合]");
        var name = GetSkillName(ChoiceSkill.skill_ID);
        if (!SkillCombos.ContainsKey(name)) return false;

        var skillCombo = SkillCombos[name];
        if (param.Contains(skillCombo.TriggerType) && skillCombo.GetCondition(env))
        {
            MyPluginMain.LogInfo($"进行替换");
            var index = GetSkillIndex(name);
            if (SetSkill(name, index))
            {
                skillCombo.GetDialogEvent(env);
                skillCombo.GetRunLua(env);
                skillCombo.GetCustomFace(env);
                return true;
            }
        }

        return false;
    }

    public static bool TryTrigger(DialogEnvironment env, bool triggerAll, string[] param)
    {
        if (!RoundManager.instance || ChoiceSkill == null || IsMonsterTurn) return false;
        MyPluginMain.LogInfo($"[进入触发缓存技能组合]");
        foreach (var skillCombo in CacheSkillCombos)
        {
            var name = GetSkillName(ChoiceSkill.skill_ID);

            if (skillCombo.Name != name)
            {
                continue;
            }

            var oldSkill    = skillCombo.SkillComboData;
            var triggerType = oldSkill.TriggerType;
            var isLastSkill = oldSkill.LastSkill;
            var isTrigger   = param.Contains(triggerType) && oldSkill.GetCondition(env);
            MyPluginMain.LogInfo($"选中技能名字:{name} 遍历技能名字:{skillCombo.Name}");
            MyPluginMain.LogInfo($"触发器:{triggerType}");
            if (isTrigger)
            {
                MyPluginMain.LogInfo($"[进入触发缓存技能组合]");

                if (isLastSkill)
                {
                    CacheSkillCombos.Remove(skillCombo);
                    ReplaceSkill(oldSkill.SkillName, skillCombo.SkillCombo.SkillName, skillCombo.Index);
                }
                else
                {
                    var newSkill = oldSkill.NextSkill;
                    CacheSkillCombos.Remove(skillCombo);
                    ReplaceSkill(oldSkill.SkillName, newSkill.SkillName, skillCombo.Index);
                    CacheSkillCombos.Add(CacheSkillCombo.Create(skillCombo.Index, skillCombo.SkillCombo, newSkill));
                }

                oldSkill.GetDialogEvent(env);
                oldSkill.GetRunLua(env);
                oldSkill.GetCustomFace(env);
                return true;
            }
        }

        return false;
    }
}