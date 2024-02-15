using System;
using System.Linq;
using JSONClass;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [RegisterCommand]
    [DialogEvent("SetShengWangIncrease")]
    [DialogEvent("设置声望增加")]
    public class SetShengWangIncrease : IDialogEvent
    {
        private string type;
        private int    add;
        private bool   show;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            add = command.GetInt(0);
            type = command.GetStr(1, "宁州");

            show = command.GetBool(2);

            var list = ShiLiHaoGanDuName.DataList.Select(item => item.ChinaText).ToList();
            if (type == "宗门")
            {
                MyLog.Log(command, $"给玩家减少宗门{add.ToString()}声望");
                MyLog.LogCommand(command, false);
                PlayerEx.AddShengWang(PlayerEx.Player.menPai, add, show);
                callback?.Invoke();
                return;
            }
            if (type == "宁州" ||
                !list.Contains(type))
            {
                MyLog.Log(command, $"给玩家减少宁州{add.ToString()}声望");
                MyLog.LogCommand(command, false);
                PlayerEx.AddShengWang(0, add, show);
                callback?.Invoke();
                return;
            }

            foreach (var item in ShiLiHaoGanDuName.DataList)
            {
                if (item.ChinaText == type)
                {
                    MyLog.Log(command, $"给玩家减少{type + add.ToString()}声望");
                    MyLog.LogCommand(command, false);
                    PlayerEx.AddShengWang(item.id, add, show);
                    callback?.Invoke();
                    return;
                }
            }
        }
    }
}