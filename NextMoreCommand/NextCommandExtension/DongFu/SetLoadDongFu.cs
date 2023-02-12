using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.DongFu;

[DialogEvent("SetLoadDongFu")]
[DialogEvent("加载洞府")]
public class SetLoadDongFu : IDialogEvent
{
    private int id;

    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        id = command.GetInt(0);
        MyLog.Log(command, $"洞府ID:{id}");
        if (DongFuManager.PlayerHasDongFu(id))
        {
          
            MyLog.Log(command, $"洞府ID:{id} 洞府名:{DongFuManager.GetDongFuName(id)} ");
            DongFuManager.LoadDongFuScene(id);
        }
        else
        {
            MyLog.Log(command, $"洞府ID:{id} 不存在", true);
        }

        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}