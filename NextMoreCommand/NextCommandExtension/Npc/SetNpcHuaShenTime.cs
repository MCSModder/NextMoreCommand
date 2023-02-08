using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetNpcHuaShenTime")]
    [DialogEvent("保送角色化神时间")]
    public class SetNpcHuaShenTime : IDialogEvent
    {
        private int npc;
        private int year;
        private int month;
        private int day;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId(0, -1);
            year = command.GetInt(1, 5000);
            month = command.GetInt(2, 1);
            day = command.GetInt(3, 1);
            var time = $"{year:0000}-{month:00}-{day:00}";
            MyLog.Log(command, $"角色ID:{npc} 角色名:{npc.GetNpcName()} 时间:{time}");
            if (npc > 0 && DateTime.TryParse(time, out _))
            {
                MyLog.Log(command, $"开始设置保送化神时间");
                var data = NpcJieSuanManager.inst.GetNpcData(npc);
                data.SetField("HuaShengTime", time);
            }
            else if (npc < 0)
            {
                MyLog.Log(command, $"角色ID:{npc} 不能为小于等于 0", true);
            }
            else
            {
                MyLog.Log(command, $"化神时间格式错误 请你好好的检查", true);
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}