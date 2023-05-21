using System;
using System.Collections.Generic;
using System.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc.Teleport
{
    [RegisterCommand]
    [DialogEvent("SetAllDaolvFollow")]
    [DialogEvent("设置所有道侣跟随")]
    public class SetAllDaolvFollow : IDialogEvent
    {
        public bool m_isAdd;
        public List<NpcInfo> NpcInfos = new List<NpcInfo>();


        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            var daolvId = PlayerEx.Player.DaoLvId.ToList();
            foreach (var param in command.ParamList)
            {
                NpcInfos.Add(new NpcInfo(param));
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
                MyLog.Log(command, $"添加道侣跟随 角色ID:{npcInfo.Id} 角色名:{npcInfo.Name} 剧情ID:{npcInfo.GetDialogName()}");
            }

            if (m_isAdd && !UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
            {
                NpcJieSuanManager.inst.isUpDateNpcList = true;
            }

            m_isAdd = false;
            NpcInfos.Clear();
            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}