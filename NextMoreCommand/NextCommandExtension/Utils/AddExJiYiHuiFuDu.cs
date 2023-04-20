using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("AddExJiYiHuiFuDu")]
    public class AddExJiYiHuiFuDu : IDialogEvent
    {


        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var memory = command.GetInt(0);
            env.player.jianLingManager.AddExJiYiHuiFuDu(memory);
     
        }
    }
}