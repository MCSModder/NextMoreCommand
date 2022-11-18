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
    enum EShengWang
    {
        Increase,
        Decrease
    }
    [RegisterCommand]
    [DialogEvent("SetShengWang")]
    public class SetShengWang : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var add = command.GetInt(0);
            var type = command.GetInt(1);

            var show = command.GetBool(2);
          
            switch ((EShengWang)type)
            {
                case EShengWang.Increase:
                    MyLog.FungusLog($"给玩家增加声望{add}");
                    break;
                case EShengWang.Decrease:
                    MyLog.FungusLog($"给玩家减少声望{add}");
                    add = -add;
                    break;
                default:
                    MyLog.FungusLogError("请输入正确整数 [0]增加声望 或者 [1]减少声望");
                    return;
            }

            PlayerEx.AddShengWang(0, add, show);
            callback?.Invoke();
        }
    }
}