using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    
    [DialogEvent("SetIntGroup")]
    [DialogEvent("设置整数群变量")]
    public class SetIntGroup: IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var group = command.GetStr(0, "");
            var key = command.GetStr(1, "");
            var value = command.GetInt(2, 0);
            DialogAnalysis.SetInt(group,key,value);
            callback?.Invoke();
        }
    }
}