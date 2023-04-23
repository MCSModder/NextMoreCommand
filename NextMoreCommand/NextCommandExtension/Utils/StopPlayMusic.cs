using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using YSGame;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("StopPlayMusic")]
public class StopPlayMusic : IDialogEvent
{
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        MusicMag.instance.stopMusic();
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}