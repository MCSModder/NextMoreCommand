using System;
using System.Collections.Generic;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("SetRecordShengPing")]
[DialogEvent("设置记录生平")]
public class SetRecordShengPing : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        var shengPingId = command.GetStr(0);
        var dict = new Dictionary<string, string>();
        MyLog.Log(command, $"记录生平ID:{shengPingId}");
        var list = command.ParamList;
        for (var i = 1; i < list.Length; i++)
        {
            var param = list[i];
            if (!param.Contains(":")) continue;
            var split = param.Split(':');
            var key = split[0];
            var value = split[1];
            dict.Add(key,value);
            MyLog.Log(command, $"记录生平 参数名:{key} 参数值:{value} ");
        }
        PlayerEx.RecordShengPing(shengPingId,dict);
        
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}