using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Teleport
{
    [RegisterCommand]
    [DialogEvent("NpcForceRemoveTeleport")]
    [DialogEvent("角色强制删除传送")]
    public class NpcForceRemoveTeleport : IDialogEvent
    {
        public bool m_isRemove = false;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {   MyLog.LogCommand(command);
            var npcList = command.ToNpcListId();
            MyLog.Log(command, $"开始执行角色强制删除传送 角色ID列表:{JArray.FromObject(command.ParamList).ToString(Formatting.None)}");
            MyLog.Log(command, $"开始执行角色强制删除传送 有效角色ID列表:{JArray.FromObject(npcList).ToString(Formatting.None)}");
            foreach (var npc in npcList)
            {
                NpcUtils.RemoveNpc(npc, out m_isRemove);
                MyLog.Log(command, $"角色强制删除传送 角色ID:{npc} 角色名:{npc.GetNpcName()}" );
            }

            if (m_isRemove && !UiNpcJiaoHuRefreshNowMapNpcPatch.m_isRefresh)
            {
                NpcJieSuanManager.inst.isUpDateNpcList = true;
            }
            MyLog.LogCommand(command);
            m_isRemove = false;
            callback?.Invoke();
        }
    }
}