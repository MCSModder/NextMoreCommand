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
    [DialogEvent("NpcForceRemoveTeleport")]
    public class NpcForceRemoveTeleport : IDialogEvent
    {
        public bool m_isRemove = false;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npcList = command.ParamList.Where(item => item.ToNpcId() > 0).Select(item => item.ToNpcId());
            foreach (var npc in npcList)
            {
                RemoveNpc(npc);
            }

            if (m_isRemove && !UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
            {
                NpcJieSuanManager.inst.isUpDateNpcList = true;
            }

            m_isRemove = false;
            callback?.Invoke();
        }

        public void RemoveNpc(int npc)
        {
            if (UINPCJiaoHu.Inst.TNPCIDList.Contains(npc))
            {
                UINPCJiaoHu.Inst.TNPCIDList.Remove(npc);
                m_isRemove = true;
            }

            NpcUtils.RemoveBindDialogEvent(npc);
        }
    }
}