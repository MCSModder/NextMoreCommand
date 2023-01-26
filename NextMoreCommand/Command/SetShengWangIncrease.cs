

using SkySwordKill;
using SkySwordKill.Next;
using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("SetShengWangIncrease")]
    public class SetShengWangIncrease : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var add = command.GetInt(0);
            var type = command.GetStr(1, "宁州");

            var show = command.GetBool(2);

            switch (type)
            {
                case "宗门":
                    MyLog.FungusLog($"给玩家增加{type + add.ToString()}声望");
                    PlayerEx.AddShengWang(PlayerEx.Player.menPai, add, show);
                    break;
                case "海域":
                    MyLog.FungusLog($"给玩家增加{type + add.ToString()}声望");
                    PlayerEx.AddShengWang(19, add, show);
                    break;
                case "白帝楼":
                    MyLog.FungusLog($"给玩家增加{type + add.ToString()}声望");
                    PlayerEx.AddShengWang(24, add, show);
                    break;
                case "龙族":
                    MyLog.FungusLog($"给玩家增加{type + add.ToString()}声望");
                    PlayerEx.AddShengWang(23, add, show);
                    break;
                case "风雨楼":
                    MyLog.FungusLog($"给玩家增加{type + add.ToString()}声望");
                    PlayerEx.AddShengWang(10, add, show);
                    break;
                default:
                    MyLog.FungusLog($"给玩家增加宁州{add}声望");
                    PlayerEx.AddShengWang(0, add, show);
                    return;
            }


            callback?.Invoke();
        }
    }
}