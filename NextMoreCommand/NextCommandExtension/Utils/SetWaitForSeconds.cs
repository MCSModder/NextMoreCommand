using System;
using System.Collections;
using System.Collections.Generic;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("SetWaitForSeconds")]
[DialogEvent("设置等待时间")]
public class SetWaitForSeconds : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        var second = command.GetInt(0);
        if (second > 0)
        {
            MyPluginMain.I.StartCoroutine(Wait(second, callback));
        }

    }
    IEnumerator Wait(int second, Action callback)
    {
        yield return new WaitForSeconds(second); ;
        callback?.Invoke();
    }
}