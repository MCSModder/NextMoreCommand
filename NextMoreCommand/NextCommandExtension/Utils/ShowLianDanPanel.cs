using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("ShowLianDanPanel")]
    [DialogEvent("显示炼丹界面")]
    public class ShowLianDanPanel : IDialogEvent
    {
        private int _type;
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            _type = command.GetInt(0);
            const PanelMamager.PanelType panelType = (PanelMamager.PanelType)0;
            PanelMamager.inst.OpenPanel(panelType, _type);

            MyLog.Log(command, $"显示面板 显示面板:{panelType.GetName()} 类型:{_type} ");
            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}