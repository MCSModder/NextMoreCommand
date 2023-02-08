using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension;

[DialogEvent("SetNpcSelfName")]
[DialogEvent("设置角色自称")]
public class SetNpcSelfName : IDialogEvent
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
                MyLog.Log(command, $"失败替换自称 角色ID:{npcId} 环境绑定角色ID:{env.roleID}  不能为小于等于 0", true);
                MyLog.LogCommand(command, false);
                callback?.Invoke();
                return;
            }

            npcId = env.roleID;
        }

        OpenInput();
        MyLog.Log(command,
            $"要替换自称 角色ID:{npcId} 角色名:{npcId.GetNpcName()} 自称:{DialogAnalysis.GetStr(NpcUtils.SelfName, npcId.ToNpcId())} ");
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }

    private void OpenInput()
    {
        UInputBox.Show($"设定{DialogAnalysis.GetNpcName(npcId)}的自称", (name) =>
        {
            var length = name.Length;
            MyLog.LogCommand(_command);
            MyLog.Log(_command, $"开始设置自称为:{name} 长度:{length} 角色ID:{npcId}");

            if (length > 16)
            {
                MyLog.Log(_command, $"设置自称长度:{length} 超过16", true);
                MyLog.LogCommand(_command, false);
                UIPopTip.Inst.Pop("称呼太长了", PopTipIconType.叹号);
                OpenInput();
                return;
            }

            MyLog.Log(_command, $"成功设置自称为:{name} 长度:{length} 角色ID:{npcId}");
            MyLog.LogCommand(_command, false);
            DialogAnalysis.SetStr(NpcUtils.SelfName, npcId.ToNpcId(), name);
        });
    }
}