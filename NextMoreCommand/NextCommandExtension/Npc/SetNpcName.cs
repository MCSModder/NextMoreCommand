using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc;

[DialogEvent("SetNpcName")]
[DialogEvent("设置角色名字")]
public class SetNpcName : IDialogEvent
{
    private int npcId;
    private DialogCommand _command;

    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        _command = command;
        MyLog.LogCommand(command);
        npcId = command.ToNpcId(0, -1);
        MyLog.Log(command, $"角色ID:{npcId} 环境绑定角色ID:{env.roleID}");
        if (npcId <= 0)
        {
            if (env.roleID <= 0)
            {
                MyLog.Log(command, $"失败替换名字 角色ID:{npcId} 环境绑定角色ID:{env.roleID}  不能为小于等于 0", true);
                MyLog.LogCommand(command, false);
                callback?.Invoke();
                return;
            }

            npcId = env.roleID.ToNpcNewId();
        }

        MyLog.Log(command, $"要替换名字 角色ID:{npcId.ToString()} 角色名:{npcId.GetNpcName()} ");
        MyLog.LogCommand(command, false);
        switch (command.ParamList.Length)
        {
            case 1:
                OpenInput();
                break;
            case 2:
            case 3:
                var surname = command.GetStr(1, null);
                var name = command.GetStr(2, null);

                NpcUtils.SetNpcFullName(npcId, surname, name);

                break;
        }

        callback?.Invoke();
    }

    private void OpenInput()
    {
        UInputBox.Show($"给{DialogAnalysis.GetNpcName(npcId)}设置全名'姓氏 名字'", (fullname) =>
        {
            var length = fullname.Length;
            var str = length.ToString();
            MyLog.LogCommand(_command);
            MyLog.Log(_command, $"开始设置名字为:{fullname} 长度:{str} 角色ID:{npcId}");
            if (length > 16)
            {
                MyLog.Log(_command, $"设置名字长度:{str} 超过16", true);
                MyLog.LogCommand(_command, false);
                UIPopTip.Inst.Pop("称呼太长了", PopTipIconType.叹号);
                OpenInput();

                return;
            }

            MyLog.Log(_command, $"成功设置名字为:{fullname} 长度:{str} 角色ID:{npcId}");
            MyLog.LogCommand(_command, false);
            if (fullname.Contains(" "))
            {
                var split = fullname.Split(' ');;
               var surname = split[0];
               var name = split[1];
               NpcUtils.SetNpcFullName(npcId, surname, name);
            }
            else
            {
                NpcUtils.SetNpcName(npcId, fullname);
            }
    
        });
    }
}