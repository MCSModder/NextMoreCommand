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
            var memory          = command.GetInt(0);
            var isAdd           = command.GetBool(1, true);
            var jianLingManager = env.player.jianLingManager;
            if (isAdd)
            {
                memory += jianLingManager.GetJiYiHuiFuDu();
            }
            jianLingManager.AddExJiYiHuiFuDu(memory);
            callback?.Invoke();


        }
    }
}