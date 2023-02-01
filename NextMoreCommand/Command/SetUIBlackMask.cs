using System;
using System.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Command;

[DialogEvent("SetUIBlackMask")]
public class SetUIBlackMask : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        PanelMamager.inst.UIBlackMaskGameObject.SetActive(false);
        PanelMamager.inst.UIBlackMaskGameObject.SetActive(true);
        callback?.Invoke();
    }
}