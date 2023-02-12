using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.DongFu;

[DialogEvent("SetNewDongFu")]
[DialogEvent("设置新洞府")]
public class SetNewDongFu : IDialogEvent
{
    private int id;
    private int level;
    private string name;

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