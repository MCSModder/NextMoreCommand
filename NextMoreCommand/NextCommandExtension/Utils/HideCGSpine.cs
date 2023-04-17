using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("HideCGSpine")]
    public class HideCGSpine : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            CGSpineManager.Instance.SetShow(false);
            callback?.Invoke();
        }
    }
}