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
    [DialogEvent("NpcForceTeleport")]
    public class NpcForceTeleport : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npc = NPCEx.NPCIDToNew(command.GetInt(0, -1));
            var dialog = command.GetStr(1);
            if (npc >= 0)
            {
                if (!UINPCJiaoHu.Inst.TNPCIDList.Contains(npc))
                {
                    UINPCJiaoHu.Inst.TNPCIDList.Add(npc);
                    BindDialogEvent(npc, dialog);
                    if (!UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
                    {
                 
                    }   NpcJieSuanManager.inst.isUpDateNpcList = true;
                 
                }
            }

            callback?.Invoke();
        }

        public void BindDialogEvent(int npc, string dialog)
        {
            if (string.IsNullOrWhiteSpace(dialog) || !DialogAnalysis.DialogDataDic.ContainsKey(dialog))
            {
                return;
            }

            if (NPCEx.IsDeath(npc)) return;
            UnityAction next = () =>
            {
                if (DialogAnalysis.IsRunningEvent)
                {
                    DialogAnalysis.SwitchDialogEvent(dialog);
                }
                else
                {
                    DialogAnalysis.StartDialogEvent(dialog);
                }
            };
            if (NPCEx.IsZhongYaoNPC(npc, out int key))
            {
                UINPCData.ThreeSceneZhongYaoNPCTalkCache.Add(key, next);
                return;
            }

            UINPCData.ThreeSceneNPCTalkCache.Add(npc, next);
        }
    }
}