using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.DongFu;

[DialogEvent("SetDongFuJuLingZhen")]
[DialogEvent("设置洞府聚灵阵")]
public class SetDongFuJuLingZhen : IDialogEvent
{
    private int _id;
    private int _level;

    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        _id = command.GetInt(0);
        _level = command.GetInt(1, -1);
        MyLog.Log(command, $"洞府ID:{_id}");
        if (DongFuManager.PlayerHasDongFu(_id) && _level >= 1 && _level <= 6)
        {
            var dongFuData = DongFuUtils.GetDongFuData(_id);
            MyLog.Log(command, $"洞府ID:{_id} 洞府名:{DongFuManager.GetDongFuName(_id)} ");
            MyLog.Log(command, $"当前洞府聚灵阵等级:{dongFuData.JuLingZhenLevel} 设置洞府聚灵阵等级:{_level} ");
            dongFuData.JuLingZhenLevel = _level;
        }
        else
        {
            MyLog.Log(command, $"洞府ID:{_id} 不存在", true);
            if (_level <= 0 && _level > 6)
            {
                MyLog.Log(command, $"设置洞府聚灵阵等级:{_level} 要在1-6之间", true, false);
            }
        }

        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}