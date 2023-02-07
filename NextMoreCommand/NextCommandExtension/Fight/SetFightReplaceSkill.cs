using System;
using KBEngine;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.StaticFace;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.Utils;
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
        MyLog.LogCommand(command);
        SkillInfoReplace skillInfoReplace = new SkillInfoReplace(-1,-1);
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

        if (skillInfoReplace.IsValid())
        {
            MyLog.Log(command,$"开始替换技能 老技能名:{skillInfoReplace.OldSkillName} 新技能名:{skillInfoReplace.NewSkillName}");
            MyLog.Log(command,$"开始替换技能 老技能Id:{skillInfoReplace.OldSkillId} 新技能Id:{skillInfoReplace.NewSkillId}");
            skillInfoReplace.SetReplace();
            MyLog.Log(command,$"成功替换技能 老技能名:{skillInfoReplace.OldSkillName} 新技能名:{skillInfoReplace.NewSkillName}");
            MyLog.Log(command,$"成功替换技能 老技能Id:{skillInfoReplace.OldSkillId} 新技能Id:{skillInfoReplace.NewSkillId}");
        }
        else
        {
            MyLog.Log(command,$"失败替换技能 老技能名:{skillInfoReplace.OldSkillName} 新技能Id:{skillInfoReplace.NewSkillName} 不存在或者输入错误",true);
            MyLog.Log(command,$"失败替换技能 老技能Id:{skillInfoReplace.OldSkillId} 新技能Id:{skillInfoReplace.NewSkillId} 不存在或者输入错误",true,false);
        }
        MyLog.LogCommand(command,false);
        callback?.Invoke();
    }
}