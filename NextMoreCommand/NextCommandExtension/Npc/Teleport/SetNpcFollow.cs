using System;
using System.Collections.Generic;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc.Teleport
{
    [RegisterCommand]
    [DialogEvent("SetNpcFollow")]
    [DialogEvent("设置角色跟随")]
    [DialogEvent("SetNpcMultiFollow")]
    [DialogEvent("设置角色多人跟随")]
    public class SetNpcFollow : IDialogEvent
    {
        public bool m_isAdd;
        public List<NpcInfo> NpcInfos = new List<NpcInfo>();

        private string dialog;
        private int npc;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            dialog = "";
            npc = -1;
            switch (command.Command)
            {
                case "SetNpcFollow":
                case "设置角色跟随":
                    npc = command.ToNpcId();
                    dialog = command.GetStr(1);
                    NpcInfos.Add(new NpcInfo(npc, dialog));
                    break;
                case "SetNpcMultiFollow":
                case "设置角色多人跟随":
                    if (command.ParamList.Length == 0) break;
                    foreach (var param in command.ParamList)
                    {
                        NpcInfos.Add(new NpcInfo(param));
                    }

                    break;
            }


            foreach (var npcInfo in NpcInfos)
            {
                if (!npcInfo.IsEmpty)
                {
                    NpcUtils.AddNpc(npcInfo, out m_isAdd);
                }
                else
                {
                    m_isAdd = true;
                }
                
                NpcUtils.SetNpcFollow(npcInfo);
                MyLog.Log(command, $"添加角色跟随 角色ID:{npcInfo.Id} 角色名:{npcInfo.Name} 剧情ID:{npcInfo.GetDialogName()}");
            }

            if (m_isAdd && !UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
            {
                NpcJieSuanManager.inst.isUpDateNpcList = true;
            }

            MyLog.LogCommand(command, false);
            m_isAdd = false;
            NpcInfos.Clear();
            callback?.Invoke();
        }
    }
}