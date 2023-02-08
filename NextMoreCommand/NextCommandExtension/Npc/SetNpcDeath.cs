using System;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using UltimateSurvival;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetNpcDeath")]
    [DialogEvent("设置角色死亡")]
    public class SetNpcDeath : IDialogEvent
    {
        private int _deathType;
        private int _npcId;
        private int _killNpcId;
        private bool _after;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            _deathType = command.GetInt(0);
            _npcId = command.ToNpcId(1);
            _killNpcId = command.ToNpcId(2);
            _after = command.GetBool(3);
            var after = _after ? "是" : "否";
            MyLog.Log($"死亡类型:{_deathType.GetDeathTypeName()} 是否延迟死亡:{after}");
            MyLog.Log($"角色ID:{_npcId} 角色名:{_npcId.GetNpcName()} 凶手ID:{_killNpcId} 凶手名:{_killNpcId.GetNpcName()}");
            DeathUtils.NpcDeathManager.SetNpcDeath(_deathType, _npcId, _killNpcId, _after);

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}