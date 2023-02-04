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
    [DialogEvent("SetAllDaolvFollow")]
    public class SetAllDaolvFollow : IDialogEvent
    {
        public bool m_isAdd;
        public List<NpcInfo> NpcInfos = new List<NpcInfo>();


        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var daolvId = PlayerEx.Player.DaoLvId.ToList();
            foreach (var param in command.ParamList)
            {
                var optionSep = param.Contains(":");
                var npc = param.ToNpcId();
                var dialog = "";
                if (optionSep && daolvId.Contains(npc))
                {
                    var option = param.Split(':');
                    npc = option[0].ToNpcId();
                    dialog = option[1];
                    NpcInfos.Add(new NpcInfo(npc, dialog));
                }
            }

            var isAddNpc = NpcInfos.Select(item => item.Id).ToList();
            foreach (var daolv in daolvId)
            {
                if (!isAddNpc.Contains(daolv))
                {
                    NpcInfos.Add(new NpcInfo(daolv));
                }
            }


            foreach (var npcInfo in NpcInfos)
            {
                NpcUtils.AddNpc(npcInfo, out m_isAdd);
                NpcUtils.SetNpcFollow(npcInfo);
            }

            if (m_isAdd && !UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
            {
                NpcJieSuanManager.inst.isUpDateNpcList = true;
            }

            m_isAdd = false;
            NpcInfos.Clear();
            callback?.Invoke();
        }
    }
}