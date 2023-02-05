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
public class SetFightReplaceSkill : IDialogEvent
{
  

    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        SkillInfoReplace skillInfoReplace = null;
        switch (command.Command)
        {
            case "SetFightReplaceSkill":
                var oldSkillId = command.GetInt(0, -1);
                var newSkillId = command.GetInt(1, -1);
                skillInfoReplace = new SkillInfoReplace(oldSkillId, newSkillId);
                break;
            case "SetFightReplaceSkillName":
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