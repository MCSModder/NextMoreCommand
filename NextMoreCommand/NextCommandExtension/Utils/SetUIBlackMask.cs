using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("SetUIBlackMask")]
[DialogEvent("设置黑屏")]
public class SetUIBlackMask : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        PanelMamager.inst.UIBlackMaskGameObject.SetActive(false);
        PanelMamager.inst.UIBlackMaskGameObject.SetActive(true);
        callback?.Invoke();
    }
}