using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

public enum EJiaoHuBtn
{
    交谈 ,
    论道,
    交易,
    切磋,
    赠礼,
    请教,
    探查,
    威胁,
    截杀,
    离开
}

public enum EJiaoHuBtnState
{
    普通,
    灰色,
    红色,
    关闭
}

public class UIJiaoHuBtnInfo
{
    public EJiaoHuBtn Type { get; }
    public EJiaoHuBtnState State { get; }
    public bool IsValid { get; }
    public UIJiaoHuBtnInfo(string input)
    {
        IsValid = false;
        if (!input.Contains(":"))
        {
            return;
        }
        var split = input.Split(new[]
        {
            ':'
        }, 2);
        if (int.TryParse(split[0], out var btn))
        {
            Type = (EJiaoHuBtn)btn;
        }
        else if (Enum.TryParse(split[0], out EJiaoHuBtn type))
        {
            Type = type;
        }
        else
        {
            return;
        }
        if (int.TryParse(split[1], out btn))
        {
            State = (EJiaoHuBtnState)btn;
        }
        else if (Enum.TryParse(split[1], out EJiaoHuBtnState state))
        {
            State = state;
        }
        else
        {
            return;
        }
        IsValid = true;

    }
    public void SetBtn()
    {
        switch (State)
        {

            case EJiaoHuBtnState.普通:
                UINPCJiaoHuPopExtends.SetBtnNormal(Type);
                break;
            case EJiaoHuBtnState.灰色:
                UINPCJiaoHuPopExtends.SetGreyButton(Type);
                break;
            case EJiaoHuBtnState.红色:
                UINPCJiaoHuPopExtends.SetDangerBtnNormal(Type);
                break;
            case EJiaoHuBtnState.关闭:
                UINPCJiaoHuPopExtends.SetBtnClose(Type);
                break;
            default:
                return;
        }
    }
}

public static class UINPCJiaoHuPopExtends
{
    public static UINPCJiaoHu UiNPCJiaoHu => UINPCJiaoHu.Inst;
    public static UINPCJiaoHuPop UiNPCJiaoHuPop => UiNPCJiaoHu.JiaoHuPop;
    public static Dictionary<EJiaoHuBtn, Button> JiaoHuBtn = new Dictionary<EJiaoHuBtn, Button>();
    public static void InitButton(this UINPCJiaoHuPop inst)
    {
        JiaoHuBtn[EJiaoHuBtn.交谈] = inst.JiaoTanBtn;
        JiaoHuBtn[EJiaoHuBtn.论道] = inst.LunDaoBtn;
        JiaoHuBtn[EJiaoHuBtn.交易] = inst.JiaoYiBtn;
        JiaoHuBtn[EJiaoHuBtn.切磋] = inst.QieCuoBtn;
        JiaoHuBtn[EJiaoHuBtn.赠礼] = inst.ZengLiBtn;
        JiaoHuBtn[EJiaoHuBtn.请教] = inst.QingJiaoBtn;
        JiaoHuBtn[EJiaoHuBtn.探查] = inst.TanChaBtn;
        JiaoHuBtn[EJiaoHuBtn.威胁] = inst.SuoQuBtn;
        JiaoHuBtn[EJiaoHuBtn.截杀] = inst.JieShaBtn;
        JiaoHuBtn[EJiaoHuBtn.离开] = inst.LiKaiBtn;
    }
    public static void SetGreyButton(EJiaoHuBtn jiaoHuBtn)
    {
        var btn = JiaoHuBtn[jiaoHuBtn];
        UiNPCJiaoHu.SetBtnGreyColor(btn.transform);
        btn.enabled = true;
    }
    public static void SetBtnNormal(EJiaoHuBtn jiaoHuBtn)
    {
        var btn = JiaoHuBtn[jiaoHuBtn];
        UiNPCJiaoHu.SetBtnNormalColor(btn.transform);
        btn.enabled = true;
    }
    public static void SetDangerBtnNormal(EJiaoHuBtn jiaoHuBtn)
    {
        var btn = JiaoHuBtn[jiaoHuBtn];
        UiNPCJiaoHu.SetBtnDangerColor(btn.transform);
        btn.enabled = true;
    }
    public static void SetBtnClose(EJiaoHuBtn jiaoHuBtn)
    {
        var btn = JiaoHuBtn[jiaoHuBtn];
        UiNPCJiaoHu.SetBtnGreyColor(btn.transform);
        btn.enabled = false;
    }
}

[DialogEvent("SetUIJiaoHuBtn")]
public class SetUIJiaoHuBtn : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        var list = command.ToList(text => new UIJiaoHuBtnInfo(text)).Where(info => info.IsValid);
        foreach (var info in list)
        {
            info.SetBtn();
        }
        callback?.Invoke();
    }

}