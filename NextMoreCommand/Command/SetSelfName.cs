using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Command;

[DialogEvent("SetSelfName")]
public class SetSelfName : IDialogEvent
{
    private int _npcId;
    private UINPCData _uinpcData;

    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        _npcId = command.GetInt(0, -1);
        if (_npcId <= 0)
        {
            if (env.roleID <= 0)
            {
                Clear();
                callback?.Invoke();
                return;
            }

            _npcId = env.roleID;
        }

        _npcId = NPCEx.NPCIDToNew(_npcId);
        _uinpcData = new UINPCData(_npcId);
        OpenInput();
        callback?.Invoke();
    }

    private void OpenInput()
    {
        UInputBox.Show($"设定{_uinpcData.Name}的自称", (name) =>
        {
            var lenght = name.Length;

            if (lenght == 0 || lenght > 16)
            {
                if (lenght > 16) UIPopTip.Inst.Pop("称呼太长了", PopTipIconType.叹号);
                OpenInput();
                return;
            }

            NpcUtils.SelfNameDict[_npcId] = name;
            Clear();
        });
    }

    private void Clear()
    {
        _npcId = 0;
        _uinpcData = null;
    }
}