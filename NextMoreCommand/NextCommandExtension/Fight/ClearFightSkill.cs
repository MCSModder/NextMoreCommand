using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fight
{
    [RegisterCommand]
    [DialogEvent("ClearFightSkill")]
    [DialogEvent("清理神通")]
    public class ClearFightSkill : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var player = env.player;
            var index = command.GetInt(0, 0);
            index = index <= 0 ? 0 : index >= 12 ? 12 : index;
            var end = command.GetInt(1, 12);
            end = end <= 0 ? 0 : end >= 12 ? 12 : end;
            player.FightClearSkill(index, end);
            callback?.Invoke();
        }
    }
}