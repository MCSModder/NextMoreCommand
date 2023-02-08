using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("SetUIBlackMask")]
[DialogEvent("设置黑屏")]
public class SetUIBlackMask : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        PanelMamager.inst.UIBlackMaskGameObject.SetActive(false);
        PanelMamager.inst.UIBlackMaskGameObject.SetActive(true);
        MyLog.Log(command, "开始执行黑屏指令");
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}