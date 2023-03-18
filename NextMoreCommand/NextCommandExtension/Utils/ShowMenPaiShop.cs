using System;
using System.Globalization;
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
    private float _ratio;
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        _sceneName = command.GetStr(0);
        _isMoney = command.GetBool(1);
        _ratio = command.GetFloat(2,1f);
        UiUtils.ShowMenPaiShop(_sceneName,_isMoney,_ratio);
        MyLog.Log(command,$"显示门派商店 商店名:{_sceneName} 商店价格倍率:{_ratio.ToString(CultureInfo.InvariantCulture)} 是否用灵石购买:{_isMoney.ToString()}");
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}