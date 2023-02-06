using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension;

[DialogEvent("SetNpcName")]
[DialogEvent("设置角色名字")]
public class SetNpcName : IDialogEvent
{


    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        var npcId = command.ToNpcId(0,-1);
        if (npcId <= 0)
        {
            if (env.roleID <= 0)
            {
                callback?.Invoke();
                return;
            }

            npcId = env.roleID;
        }

        npcId = NPCEx.NPCIDToNew(npcId);
        OpenInput(npcId);
        callback?.Invoke();
    }

    private void OpenInput(int npcId)
    {
        UInputBox.Show($"给{DialogAnalysis.GetNpcName(npcId)}设置名字", (name) =>
        {
            var lenght = name.Length;

            if (lenght > 16)
            {
                UIPopTip.Inst.Pop("称呼太长了", PopTipIconType.叹号);
                OpenInput(npcId);
                return;
            }

            NpcUtils.SetNpcName(npcId, name);

        });
    }

}