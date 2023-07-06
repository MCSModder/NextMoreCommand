using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [RegisterCommand]
    [DialogEvent("NpcForceJiaoHu")]
    [DialogEvent("角色强制交互")]
    public class NpcForceJiaoHu : IDialogEvent
    {
        private int _npc;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            _npc = command.ToNpcId();
            if (_npc >= 0)
            {
                MyLog.Log(command, $"开始执行角色强制交互 角色ID:{_npc} 角色名:{_npc.GetNpcName()} ");
                DialogAnalysis.NpcForceInteract(_npc);
            }
            else
            {
                MyLog.Log(command, $"执行失败角色强制交互 角色ID:{_npc} 不能小于等于 0 ");
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}