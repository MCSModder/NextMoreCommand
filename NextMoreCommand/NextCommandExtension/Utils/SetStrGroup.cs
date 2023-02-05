using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    
    [DialogEvent("SetStrGroup")]
    [DialogEvent("设置字符串群变量")]
    public class SetStrGroup: IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var group = command.GetStr(0, "");
            var key = command.GetStr(1, "");
            var value = command.GetStr(2, "");
            DialogAnalysis.SetStr(group,key,value);
            callback?.Invoke();
        }
    }
}