using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    [RegisterCommand]
    [DialogEvent("SetFightFace")]
    [DialogEvent("设置战斗立绘")]
    public class SetFightFace : IDialogEvent
    {
        private int npc;
        private int fightFace;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            fightFace = command.GetInt(1, 0);
            if (npc > 0)
            {
                var data = npc.NPCJson();
                var nowFightFace = data.GetInt("fightFace").ToString();
                MyLog.Log(command, $"角色ID:{npc} 角色名:{npc.GetNpcName()} 当前战斗立绘:{nowFightFace} 设置战斗立绘:{fightFace}");
                MyLog.Log(command, $"");
                data.SetField("fightFace", fightFace);
            }
            else
            {
                MyLog.Log(command, $"角色ID:{npc} 不能为小于等于 0", true);
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}