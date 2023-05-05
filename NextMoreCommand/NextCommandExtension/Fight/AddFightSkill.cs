using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fight
{
    [RegisterCommand]
    [DialogEvent("AddFightSkill")]
    [DialogEvent("增加神通")]
    public class AddFightSkill : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var player = env.player;
            var skill = command.GetStr(0);
            var index = command.GetInt(1, 0);
            index = index <= 0 ? 0 : index >= 12 ? 12 : index;
            var end = command.GetInt(2, 12);
            end = end <= 0 ? 0 : end >= 12 ? 12 : end;
            if (!string.IsNullOrWhiteSpace(skill))
            {
                var skillId = int.TryParse(skill, out var result) ? SkillComboManager.GetSkillKey(result) : SkillComboManager.GetSkillKey(skill);
                player.FightAddSkill(skillId, index, end);
            }
            callback?.Invoke();
        }
    }
}