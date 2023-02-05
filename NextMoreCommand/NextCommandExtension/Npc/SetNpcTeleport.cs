using System;
using System.Collections.Generic;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetNpcTeleport")]
    [DialogEvent("设置NPC传送")]
    public class SetNpcTeleport : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
          
            var sceneName = command.GetStr(0, string.Empty);
            List<int> npcIds = NpcUtils.GetNpcList(command,2);

            var sceneNameIsEmpty = sceneName == string.Empty;
            if (sceneNameIsEmpty)
            {
                MyLog.FungusLogError($"场景名字不能为空");
            }
            else if(npcIds.Count == 0)
            {
                MyLog.FungusLogError($"NPC不能为空");
            }
            else
            {
                
                foreach (var npc in npcIds)
                {
                    NPCEx.WarpToScene(npc, sceneName);
                }
                MyLog.FungusLog($"跳转地图事件 场景名字: {sceneName}  NPCID: {npcIds}");
            }
            callback?.Invoke();
        }
    }
}