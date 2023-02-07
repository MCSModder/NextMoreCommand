using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("NpcForceJiaoYi")]
    [DialogEvent("角色强制交易")]
    public class NpcForceJiaoYi : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            var npc = command.ToNpcId();
            if (npc > 0)
            {
                MyLog.Log(command, $"开始执行角色强制交易 角色ID:{npc} 角色名:{npc.GetNpcName()} ");

                var npcData = new UINPCData(npc);
                npcData.RefreshData();

                UINPCJiaoHu.Inst.HideJiaoHuPop();
                UINPCJiaoHu.Inst.NowJiaoHuNPC = npcData;
                UINPCJiaoHu.Inst.IsJiaoYiClicked = true;
            }
            else
            {
                MyLog.Log(command, $"执行失败角色强制交易 角色ID:{npc} 不能小于等于 0 ");
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}