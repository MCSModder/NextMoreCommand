using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("ShowMenPaiShop")]
[DialogEvent("显示门派商店")]
public class ShowMenPaiShop : IDialogEvent
{
    private string _sceneName;
    private bool _isMoney;
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        _sceneName = command.GetStr(0);
        _isMoney = command.GetBool(1);
        UiUtils.ShowMenPaiShop(_sceneName,_isMoney);
        MyLog.Log(command,$"显示门派商店 商店名:{_sceneName} 是否用灵石购买:{_isMoney.ToString()}");
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}