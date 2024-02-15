using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.DongFu;

[DialogEvent("SetNewDongFu")]
[DialogEvent("设置新洞府")]
public class SetNewDongFu : IDialogEvent
{
    private int    id;
    private int    level;
    private string name;
    /// <summary>
    /// 设置新洞府<br/>
    /// lua使用方法:
    /// <code>
    /// function XXX(指令,环境)
    /// --指令.设置新洞府(洞府id,灵眼等级(1-3),洞府名字)
    /// --指令.SetNewDongFu(洞府id,灵眼等级(1-3),洞府名字)
    /// 指令.设置新洞府(1,3,"新洞府")
    /// 指令.SetNewDongFu(1,3,"新洞府");
    /// end
    /// </code>
    /// Json使用方法:
    /// <code>
    /// [{
    /// "id": "测试剧情",
    /// "character": {},
    /// "dialog": [
    /// "设置新洞府*1#3#新洞府",
    /// "SetNewDongFu*1#3#新洞府",
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
        level = command.GetInt(1);
        name = command.GetStr(2);
        MyLog.Log(command, $"洞府ID:{id} 洞府名:{name} 洞府等级:{level}");
        if (DongFuManager.PlayerHasDongFu(id))
        {
            MyLog.Log(command, $"洞府ID:{id} 洞府名:{DongFuManager.GetDongFuName(id)} 已经存在", true);
        }
        else if (id < 0)
        {
            MyLog.Log(command, $"洞府ID:{id} 不能小于0", true);
        }
        else if (level <= 0 || level > 3)
        {
            MyLog.Log(command, $"洞府等级:{level} 要1-3之间数字", true);
        }
        else
        {
            DongFuUtils.CreateDongFu(id, level, name);
        }

        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}