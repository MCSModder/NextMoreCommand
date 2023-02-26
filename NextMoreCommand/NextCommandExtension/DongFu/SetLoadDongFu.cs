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
    /// <summary>
    /// 加载洞府指令<br/>
    /// lua使用方法:
    /// <code>
    /// function XXX(指令,环境)
    /// --指令.加载洞府(洞府id)
    /// --指令.SetLoadDongFu(洞府id)
    /// 指令.加载洞府(1)
    /// 指令.SetLoadDongFu(1);
    /// end
    /// </code>
    /// Json使用方法:
    /// <code>
    /// [{
    /// "id": "测试剧情",
    /// "character": {},
    /// "dialog": [
    /// "加载洞府*1",
    /// "SetLoadDongFu*1",
    /// ],
    /// "option": []
    /// }]
    /// </code>
    /// </summary>
    /// <param name="command"></param>
    /// <param name="env"></param>
    /// <param name="callback"></param>
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