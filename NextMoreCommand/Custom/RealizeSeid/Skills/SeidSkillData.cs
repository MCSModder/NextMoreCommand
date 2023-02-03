using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using SkySwordKill.Next.DialogSystem;
using Skill = GUIPackage.Skill;

namespace SkySwordKill.NextMoreCommand.Custom.RealizeSeid.Skills;

public class SeidSkillData:DialogEnvironment
{
    public static SeidSkillData Create(Skill skill, int seid, List<int> damage, int type) =>
        new SeidSkillData(skill, seid, damage, type);

    public SeidSkillData(Skill skill, int seid, List<int> damage, int type)
    {
        _skill = skill;
        _seid = seid;
        _type = type;
        _damage = damage;
       
    }

    public void SetSeidSkillData(Skill skill, int seid, List<int> damage, int type)
    {
        _skill = skill;
        _seid = seid;
        _type = type;
        _damage = damage;
    }

    private Skill _skill;
    private int _seid;
    private int _type;
    private List<int> _damage;
    private Avatar _attaker;
    private Avatar _receiver;

    public int Damage
    {
        get => _damage[0];
        set => _damage[0] = value;
    }
    public int BuffTime
    {
        get => _damage[4];
        set => _damage[4] = value;
    }

    public int SkillID
    {
        get => _damage[1];
    }

    public int ID
    {
        get => _skill.SkillID;
    }

    public string SkillName
    {
        get => _skill.skill_Name.RemoveNumber();
    }

    public SeidSkill SeidSkill;

    public void SetSeidSkill()
    {
        SeidSkill = new SeidSkill();
    }
    
    public void SetAvatar(Entity attaker,
        Entity receiver)
    {
        _attaker = (Avatar)attaker;
        _receiver = (Avatar)receiver;
        
    }
    public void SetDamage(int damage)
    {
        Damage = damage;
    }
    public void AddDamage(int damage)
    {
        Damage += damage;
    }
    public void SetHealth(int health)
    {
        Damage = -health;
    }
    public void AddHealth(int health)
    {
        Damage -= health;
    }
    public void AddLateDamage(int skillId,int damage)
    {
       _skill.LateDamages.Add(new LateDamage()
       {
           Damage = damage,
           SkillId = skillId
       });
    }
}