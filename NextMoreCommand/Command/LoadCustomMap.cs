using SkySwordKill;
using SkySwordKill.Next;
using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.CustomMap;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("LoadCustomMap")]
    public class LoadCustomMap : IDialogEvent
    {
      

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var mapID = command.GetInt(0, -1);
            var position = command.GetInt(1);
            var isForce = command.GetBool(2,false);
            CustomMapManager.GetPlayer().LoadMap(mapID,position,isForce);
            callback?.Invoke();
        }

    }
}