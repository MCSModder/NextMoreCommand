using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("SetIntGroup")]
    [DialogEvent("设置整数组变量")]
    public class SetIntGroup : IDialogEvent
    {
        private string group;
        private string key;
        private int    value;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            group = command.GetStr(0, "");
            key = command.GetStr(1,   "");
            value = command.GetInt(2, 0);
            if (string.IsNullOrWhiteSpace(group) && string.IsNullOrWhiteSpace(key))
            {
                MyLog.Log(command, $"设置群变量失败 变量组:{group} 变量键值:{key} 不能为空", true);
            }
            else
            {
                MyLog.Log(command, $"设置群变量成功 变量组:{group} 变量键值:{key} 变量值:{value}");
                DialogAnalysis.SetInt(group, key, value);
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}