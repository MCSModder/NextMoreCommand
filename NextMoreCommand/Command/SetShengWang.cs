using SkySwordKill;
using SkySwordKill.Next;
using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("SetShengWang")]
    public class SetShengWang : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var type = command.GetInt(0);
            var add = command.GetInt(1 );
            var show = command.GetBool(2);
            if (type == 0)
            {
                MyLog.FungusLog($"给玩家增加声望{add}");
            }
            else
            {
                MyLog.FungusLog($"给玩家减少声望{add}");
                add = -add;
            }
            PlayerEx.AddShengWang(0,add,show);
           
           
        }
    }
}