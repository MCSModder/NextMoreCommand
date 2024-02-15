using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("ShowGaoshi")]
[DialogEvent("显示告示")]
public class ShowGaoshi : IDialogEvent
{
    private string _sceneName;
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        _sceneName = command.GetStr(0);
        UiUtils.ShowGaoShi(_sceneName);
        MyLog.Log(command, $"显示告示 告示场景:{_sceneName}");
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}