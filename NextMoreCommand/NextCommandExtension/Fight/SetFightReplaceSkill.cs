using System;
using KBEngine;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.StaticFace;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using GameObject = UnityEngine.GameObject;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fight;

[RegisterCommand]
[DialogEvent("SetFightReplaceSkill")]
[DialogEvent("SetFightReplaceSkillName")]
[DialogEvent("技能替换")]
[DialogEvent("技能名字替换")]
public class SetFightReplaceSkill : IDialogEvent
{
  

    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        SkillInfoReplace skillInfoReplace = null;
        switch (command.Command)
        {
            case "SetFightReplaceSkill":
            case "技能替换":
                var oldSkillId = command.GetInt(0, -1);
                var newSkillId = command.GetInt(1, -1);
                skillInfoReplace = new SkillInfoReplace(oldSkillId, newSkillId);
                break;
            case "SetFightReplaceSkillName":
            case "技能名字替换":
                var oldSkillName = command.GetStr(0, "");
                var newSkillName = command.GetStr(1, "");
                skillInfoReplace = new SkillInfoReplace(oldSkillName, newSkillName);
                break;
        }

        if (skillInfoReplace?.IsValid() ?? false)
        {
            skillInfoReplace.SetReplace();
        }
        callback?.Invoke();
    }
}