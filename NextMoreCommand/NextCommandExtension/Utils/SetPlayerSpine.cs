using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("SetPlayerSpine")]
    public class SetPlayerSpine : IDialogEvent
    {


        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var spine = command.GetStr(0, "");
            var spineSkin = command.GetStr(1, "default");
            DialogAnalysis.SetStr("PLAYER_SPINE", spine);
            DialogAnalysis.SetStr("PLAYER_SPINE_SKIN", spineSkin);
            var inst = UIHeadPanel.Inst;
            if (inst != null)
            {
                inst.Face.setFace();
            }

            callback?.Invoke();
        }
    }
}