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
    /// <summary>
    /// 设置洞府聚灵阵指令<br/>
    /// lua使用方法:
    /// <code>
    /// function XXX(指令,环境)
    /// --指令.设置洞府聚灵阵(洞府id,聚灵阵等级(1-3))
    /// --指令.SetDongFuJuLingZhen(洞府id,聚灵阵等级(1-3))
    /// 指令.设置洞府聚灵阵(1,3)
    /// 指令.SetDongFuJuLingZhen(1,3);
    /// end
    /// </code>
    /// Json使用方法:
    /// <code>
    /// [{
    /// "id": "测试剧情",
    /// "character": {},
    /// "dialog": [
    /// "设置洞府聚灵阵*1#3",
    /// "SetDongFuJuLingZhen*1#3",
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