using System;
using System.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Command;
[DialogEvent("SetAllDaolvDeath")]
public class SetAllDaolvDeath:IDialogEvent
{
    public int GetNpcID(string npc) => NPCEx.NPCIDToNew(Convert.ToInt32(npc));
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        var npcId = command.ParamList.Select(GetNpcID).Where(item => item > 0).ToArray();
        MyLog.FungusLog("开始触发所有道侣死亡指令");
        DaolvUtils.SetAllDaolvDeath(npcId);
        callback?.Invoke();
    }
}