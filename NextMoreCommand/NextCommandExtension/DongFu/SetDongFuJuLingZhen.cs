using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.DongFu;

[DialogEvent("SetDongFuJuLingZhen")]
[DialogEvent("设置洞府聚灵阵")]
public class SetDongFuJuLingZhen : IDialogEvent
{
    private int id;
    private int level;

    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        id = command.GetInt(0);
        level = command.GetInt(1, -1);
        MyLog.Log(command, $"洞府ID:{id}");
        if (DongFuManager.PlayerHasDongFu(id) && level >= 1 && level <= 6)
        {
            var dongFuData = DongFuUtils.GetDongFuData(id);
            MyLog.Log(command, $"洞府ID:{id} 洞府名:{DongFuManager.GetDongFuName(id)} ");
            MyLog.Log(command, $"当前洞府聚灵阵等级:{dongFuData.JuLingZhenLevel} 设置洞府聚灵阵等级:{level} ");
            dongFuData.JuLingZhenLevel = level;
        }
        else
        {
            MyLog.Log(command, $"洞府ID:{id} 不存在", true);
            if (level <= 0 && level > 6)
            {
                MyLog.Log(command, $"设置洞府聚灵阵等级:{level} 要在1-6之间", true, false);
            }
        }

        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}