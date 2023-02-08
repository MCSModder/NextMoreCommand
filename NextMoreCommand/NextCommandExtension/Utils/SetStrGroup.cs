using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("SetStrGroup")]
    [DialogEvent("设置字符串组变量")]
    public class SetStrGroup : IDialogEvent
    {
        private string group;
        private string key;
        private string value;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            group = command.GetStr(0, "");
            key = command.GetStr(1, "");
            value = command.GetStr(2, "");
            if (string.IsNullOrWhiteSpace(group) && string.IsNullOrWhiteSpace(key))
            {
            }
            else
            {
                DialogAnalysis.SetStr(group, key, value);
            }

            callback?.Invoke();
        }
    }
}