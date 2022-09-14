using SkySwordKill;
using SkySwordKill.Next;
using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("SetNpcTeleport")]
    public class SetNpcTeleport : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var sceneName = command.GetStr(0, string.Empty);
            var npcId = command.GetInt(1, -1);
           

            var sceneNameIsEmpty = sceneName == string.Empty;
            if (sceneNameIsEmpty)
            {
                MyLog.FungusLogError($"场景名字不能为空");
            }
            else if(npcId < 1)
            {
                MyLog.FungusLogError($"NPC不能为空或者小于0");
            }
            else
            {
                NPCEx.WarpToScene(npcId, sceneName);
                MyLog.FungusLog($"跳转地图事件 场景名字: {sceneName}  NPCID: {npcId}");
            }
        }
    }
}