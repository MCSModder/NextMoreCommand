using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("CheckTaskError")]
[DialogEvent("检测任务错误")]
public class CheckTaskError : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        TaskUtils.CheckTaskError();
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}