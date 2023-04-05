using System;
using HarmonyLib;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("SetRefreshMapPlayer")]
[DialogEvent("刷新地图玩家骨骼")]
public class SetRefreshMapPlayer : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        var normalShow = MapPlayerController.Inst.NormalShow;
        Traverse.Create(normalShow).Field<string>("nowSpineName").Value = "";
        normalShow.Refresh();
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}