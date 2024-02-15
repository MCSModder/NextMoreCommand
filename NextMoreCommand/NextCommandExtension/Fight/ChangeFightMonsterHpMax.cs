using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fight;

[RegisterCommand]
[DialogEvent("ChangeFightMonsterHpMax")]
[DialogEvent("修改战斗对象的血量上限")]
public class ChangeFightMonsterHpMax : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        var value = command.GetInt(0, 0);
        var monster = PlayerEx.Player.OtherAvatar;
        Tools.instance.MonstarID.AddNpcHp(value);
        monster.HP_Max += value;

        callback?.Invoke();
    }
}