using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using MCSSubscribeDependencies;
using script.Steam;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("HasCommand")]
    public class HasCommand : IDialogEnvQuery
    {
        private Dictionary<string, IDialogEvent> _registerEvents;
        public object Execute(DialogEnvQueryContext context)
        {
            _registerEvents ??= Traverse.Create(typeof(DialogAnalysis)).Field<Dictionary<string, IDialogEvent>>("_registerEvents").Value;
            var command = context.GetMyArgs<string>(0);
            return _registerEvents.ContainsKey(command);

        }
    }
}