using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("NpcForceJiaoHu")]
    [DialogEvent("NPC强制交互")]
    public class NpcForceJiaoHu : IDialogEvent
    {
      

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npc = NPCEx.NPCIDToNew(command.GetInt(0, -1));
            if (npc >= 0)
            {
                DialogAnalysis.NpcForceInteract(npc);
            }
         
            callback?.Invoke();
        }

    }
}