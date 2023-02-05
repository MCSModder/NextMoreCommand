using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("NpcForceJiaoYi")]
    [DialogEvent("NPC强制交易")]
    public class NpcForceJiaoYi : IDialogEvent
    {
      

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npc = NPCEx.NPCIDToNew(command.GetInt(0, -1)) ;
            if (npc >= 0)
            {
                var npcData = new UINPCData(npc);
                npcData.RefreshData();
            
                UINPCJiaoHu.Inst.HideJiaoHuPop();
                UINPCJiaoHu.Inst.NowJiaoHuNPC = npcData;
                UINPCJiaoHu.Inst.IsJiaoYiClicked = true;
                
            }
         
            callback?.Invoke();
        }

    }
}