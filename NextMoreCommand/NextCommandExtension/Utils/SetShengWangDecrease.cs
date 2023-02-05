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
    [DialogEvent("SetShengWangDecrease")]
    [DialogEvent("设置声望减少")]
    public class SetShengWangDecrease : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var add = -command.GetInt(0);
            var type = command.GetStr(1, "宁州");
            var show = command.GetBool(2);

            var list = ShiLiHaoGanDuName.DataList.Select(item => item.ChinaText).ToList();
            if (type == "宗门")
            {
                MyLog.FungusLog($"给玩家减少{type + add.ToString()}声望");
                PlayerEx.AddShengWang(PlayerEx.Player.menPai, add, show);
                callback?.Invoke();
                return;
            }
            else if (type == "宁州" ||
                     !list.Contains(type))
            {
                MyLog.FungusLog($"给玩家减少宁州{add.ToString()}声望");
                PlayerEx.AddShengWang(0, add, show);
                callback?.Invoke();
                return;
            }
            foreach (var item in ShiLiHaoGanDuName.DataList)
            {
                if (item.ChinaText == type)
                {
                    MyLog.FungusLog($"给玩家减少{type +add.ToString()}声望");
                    PlayerEx.AddShengWang(item.id, add, show);
                    callback?.Invoke();
                    return;
                }
            }
    
        }
    }
}