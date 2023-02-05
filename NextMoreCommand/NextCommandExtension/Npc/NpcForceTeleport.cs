using System;
using System.Collections.Generic;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("NpcForceTeleport")]
    [DialogEvent("角色强制传送")]
    [DialogEvent("NpcForceMultiTeleport")]
    [DialogEvent("角色多人强制传送")]
    public class NpcForceTeleport : IDialogEvent
    {
        public bool m_isAdd;
        public List<NpcInfo> NpcInfos = new List<NpcInfo>();

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            string dialog;
            int npc;
            switch (command.Command)
            {
                case "NpcForceTeleport":
                case "角色强制传送":
                    npc = NPCEx.NPCIDToNew(command.GetInt(0, -1));
                    dialog = command.GetStr(1);
                    NpcInfos.Add(new NpcInfo(npc, dialog));
                    break;
                case "NpcForceMultiTeleport":
                case "角色多人强制传送":
                    if (command.ParamList.Length == 0) break;
                    foreach (var param in command.ParamList)
                    {
                        var optionSep = param.Contains(":");
                        npc = param.ToNpcId();
                        dialog = "";
                        if (optionSep)
                        {
                            var option = param.Split(':');
                            npc = option[0].ToNpcId();
                            dialog = option[1];
                            NpcInfos.Add(new NpcInfo(npc, dialog));
                        }
                        else if (npc > 0)
                        {
                            NpcInfos.Add(new NpcInfo(npc, dialog));
                        }
                    }

                    break;
            }

            foreach (var npcInfo in NpcInfos)
            {
                NpcUtils.AddNpc(npcInfo, out m_isAdd);
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