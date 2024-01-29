using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("AddTianJieJiaSu")]
    public class AddTianJieJiaSu : IDialogEvent
    {


        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var year = command.GetInt(0);
            TianJieManager.TianJieJiaSu(year);
            callback?.Invoke();
        }
    }
}