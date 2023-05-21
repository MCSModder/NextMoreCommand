using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc;

[DialogEvent("SetNpcCallName")]
[DialogEvent("设置角色称呼")]
public class SetNpcCallName : IDialogEvent
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
                MyLog.Log(command, $"失败替换称呼 角色ID:{npcId} 环境绑定角色ID:{env.roleID}  不能为小于等于 0", true);
                MyLog.LogCommand(command, false);
                callback?.Invoke();
                return;
            }

            npcId = env.roleID;
        }
        switch (command.ParamList.Length)
        {
            case 1:
                OpenInput();
                break;
            case 2:
                var name = command.GetStr(1, "");
                MyLog.Log(_command, $"成功设置称呼为:{name} 角色ID:{npcId}");
                NpcUtils.SetCallName(npcId, name);
                break;
            case 3:
                var man = command.GetStr(1, "");
                var woman = command.GetStr(2, "");
                name = env.player.Sex == 2 ? woman : man;
                MyLog.Log(_command, $"成功设置称呼为:{name} 角色ID:{npcId}");
                NpcUtils.SetCallName(npcId, name);
                break;
        }
        if (command.ParamList.Length  > 1)
        {
      
        }
        else
        {
          
        }
       
        MyLog.Log(command,
            $"要替换称呼 角色ID:{npcId} 角色名:{npcId.GetNpcName()} 称呼:{NpcUtils.GetCallName(npcId)} ");
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }

    private void OpenInput()
    {
        UInputBox.Show($"设定{DialogAnalysis.GetNpcName(npcId)}的称呼", (name) =>
        {
            var length = name.Length;
            MyLog.LogCommand(_command);
            MyLog.Log(_command, $"开始设置称呼为:{name} 长度:{length} 角色ID:{npcId}");

            if (length > 16)
            {
                MyLog.Log(_command, $"设置称呼长度:{length} 超过16", true);
                MyLog.LogCommand(_command, false);
                UIPopTip.Inst.Pop("称呼太长了", PopTipIconType.叹号);
                OpenInput();
                return;
            }

            MyLog.Log(_command, $"成功设置称呼为:{name} 长度:{length} 角色ID:{npcId}");
            MyLog.LogCommand(_command, false);
            NpcUtils.SetCallName(npcId, name);
        });
    }
}