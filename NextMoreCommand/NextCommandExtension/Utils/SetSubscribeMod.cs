using System;
using MCSSubscribeDependencies;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("SetSubscribeMod")]
[DialogEvent("订阅模组")]
public class SetSubscribeMod : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);

        WorkshopUtils.Subscribe(command.ToListULong().ToArray());
        MyLog.Log(command, "开始订阅模组");
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}