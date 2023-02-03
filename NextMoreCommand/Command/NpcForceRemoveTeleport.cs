using SkySwordKill;
using SkySwordKill.Next;
using System;
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
    [DialogEvent("NpcForceRemoveTeleport")]
    public class NpcForceRemoveTeleport : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npc = NPCEx.NPCIDToNew(command.GetInt(0, -1));
            if (npc >= 0)
            {
                if (UINPCJiaoHu.Inst.TNPCIDList.Contains(npc))
                {
                    UINPCJiaoHu.Inst.TNPCIDList.Remove(npc);
                    RemoveBindDialogEvent( npc);
                    if (!UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
                    {
                        NpcJieSuanManager.inst.isUpDateNpcList = true;
                    }   
                 
                }
            }

            callback?.Invoke();
        }

        public void RemoveBindDialogEvent(int npc)
        {
         

            if (NPCEx.IsDeath(npc)) return;
       
            if (NPCEx.IsZhongYaoNPC(npc, out int key))
            {
                UINPCData.ThreeSceneZhongYaoNPCTalkCache.Remove(key);
                return;
            }

            UINPCData.ThreeSceneNPCTalkCache.Remove(npc);
        }
    }
}