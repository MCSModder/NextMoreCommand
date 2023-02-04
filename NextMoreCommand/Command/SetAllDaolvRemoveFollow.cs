using SkySwordKill;
using SkySwordKill.Next;
using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine.Events;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("SetAllDaolvRemoveFollow")]
    public class SetAllDaolvRemoveFollow : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var daolvId = PlayerEx.Player.DaoLvId.ToList().ToArray();
            NpcUtils.RemoveNpcFollow(daolvId);
            callback?.Invoke();
        }
    }
}