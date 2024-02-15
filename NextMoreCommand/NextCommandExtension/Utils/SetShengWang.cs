using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    enum EShengWang
    {
        Increase,
        Decrease
    }

    [RegisterCommand]
    [DialogEvent("SetShengWang")]
    [DialogEvent("设置声望")]
    public class SetShengWang : IDialogEvent
    {
        private int  add;
        private int  type;
        private int  id;
        private bool show;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            add = command.GetInt(0);
            type = command.GetInt(1);
            id = command.GetInt(2);
            show = command.GetBool(3);

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

            PlayerEx.AddShengWang(id, add, show);
            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}