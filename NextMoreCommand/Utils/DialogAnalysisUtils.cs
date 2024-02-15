using System.Collections.Generic;
using HarmonyLib;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class DialogAnalysisUtils
    {
        private static Dictionary<string, IDialogEvent> _registerEvents;
        public static Dictionary<string, IDialogEvent> RegisterEvents
        {
            get
            {

                _registerEvents ??= Traverse.Create(typeof(DialogAnalysis)).Field<Dictionary<string, IDialogEvent>>("_registerEvents").Value;
                return _registerEvents;
            }
        }
        public static bool HasCommand(string command)
        {

            return RegisterEvents.ContainsKey(command);
        }
        public static IDialogEvent GetCommand(string command)
        {

            return RegisterEvents.TryGetValue(command, out var dialogEvent) ? dialogEvent : null;
        }
    }
}