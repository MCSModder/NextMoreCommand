using System;
using System.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("NpcForceRemoveTeleport")]
    [DialogEvent("NPC强制删除传送")]
    public class NpcForceRemoveTeleport : IDialogEvent
    {
        public bool m_isRemove = false;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npcList = command.ParamList.Where(item => item.ToNpcId() > 0).Select(item => item.ToNpcId());
            foreach (var npc in npcList)
            {
                NpcUtils.RemoveNpc(npc, out m_isRemove);
            }

            if (m_isRemove && !UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
            {
                NpcJieSuanManager.inst.isUpDateNpcList = true;
            }

            m_isRemove = false;
            callback?.Invoke();
        }
    }
}