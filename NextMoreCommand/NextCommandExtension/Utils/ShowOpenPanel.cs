using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("ShowOpenPanel")]
[DialogEvent("显示面板")]
public class ShowOpenPanel : IDialogEvent
{
    private int _panelType;
    private int _type;
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        _panelType = command.GetInt(0, -1);
        _type = command.GetInt(1);
        if (_panelType is > 5 or < 0 )
        {
            _panelType = 5;
        }
        var panelType = (PanelMamager.PanelType)_panelType;
        PanelMamager.inst.OpenPanel(panelType, _type);
      
        MyLog.Log(command, $"显示面板 显示面板:{panelType.GetName()} 类型:{_type} ");
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}