using System;
using script.NewLianDan;
using script.Submit;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using Tab;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("CloseAllUI")]
    [DialogEvent("关闭所有UI界面")]
    public class CloseAllUI : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            if (TabUIMag.Instance != null)
            {
                TabUIMag.Instance.TryEscClose();
            }
            if (FpUIMag.inst != null)
            {
                FpUIMag.inst.Close();
            }

            if (UIBiGuanPanel.Inst != null)
            {
                var uiBiGuanPanel = UIBiGuanPanel.Inst;
                uiBiGuanPanel.TryEscClose();

            }

            if (SubmitUIMag.Inst != null)
            {
                SubmitUIMag.Inst.TryEscClose();
            }
            var panelManager = PanelMamager.inst;
            if (PanelMamager.CanOpenOrClose && panelManager.nowPanel != PanelMamager.PanelType.空)
            {
                panelManager.closePanel(panelManager.nowPanel);
            }

            if (UINPCJiaoHu.Inst != null)
            {
                var inst = UINPCJiaoHu.Inst;
                inst.HideJiaoHuPop();
                inst.HideNPCInfoPanel();
                inst.HideNPCSuoQu();
                inst.HideNPCZengLi();
                inst.HideNPCTanChaPanel();
                inst.HideNPCQingJiaoPanel();
                inst.HideNPCShuangXiuAnim();
                inst.HideNPCShuangXiuSelect();
            }
            callback?.Invoke();
        }
    }
}