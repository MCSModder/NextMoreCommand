using System;
using System.Collections.Generic;
using HarmonyLib;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("TryRunCommand")]
    public class TryRunCommand : IDialogEvent
    {

        private Dictionary<string, IDialogEvent> _registerEvents;
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            _registerEvents ??= Traverse.Create(typeof(DialogAnalysis)).Field<Dictionary<string, IDialogEvent>>("_registerEvents").Value;
            var commandName = command.GetStr(0, null);
            commandName = commandName.Contains("*") ? commandName.Split('*')[0] : "";
            if (!string.IsNullOrWhiteSpace(commandName) && _registerEvents.ContainsKey(commandName))
            {
                var result = string.Join("#", command.ParamList);
                // string.Join()
                MyLog.Log(result);
                DialogAnalysis.RunDialogEventCommand(new DialogCommand(result, command.BindEventData, env, command.IsEnd), env, callback);
            }
            else
            {
                callback?.Invoke();
            }

        }
    }
}