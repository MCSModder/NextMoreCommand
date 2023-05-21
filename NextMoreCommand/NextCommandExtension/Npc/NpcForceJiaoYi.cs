using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [RegisterCommand]
    [DialogEvent("NpcForceJiaoYi")]
    [DialogEvent("角色强制交易")]
    public class NpcForceJiaoYi : IDialogEvent
    {
        private int _npc;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            _npc = command.ToNpcId();
            if (_npc > 0)
            {
                MyLog.Log(command, $"开始执行角色强制交易 角色ID:{_npc} 角色名:{_npc.GetNpcName()} ");

                var npcData = new UINPCData(_npc);
                npcData.RefreshData();

                UINPCJiaoHu.Inst.HideJiaoHuPop();
                UINPCJiaoHu.Inst.NowJiaoHuNPC = npcData;
                UINPCJiaoHu.Inst.IsJiaoYiClicked = true;
            }
            else
            {
                MyLog.Log(command, $"执行失败角色强制交易 角色ID:{_npc} 不能小于等于 0 ");
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}