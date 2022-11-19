using SkySwordKill;
using SkySwordKill.Next;
using System;
using System.Collections.Generic;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("SetTeleport")]
    public class SetTeleport : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var mapId = command.GetInt(0, -1);
            var sceneName = command.GetStr(1, string.Empty);
            List<int> npcIds = NpcUtils.GetNpcList(command,2,3);
          
           


            var sceneNameIsEmpty = sceneName == string.Empty;
            if (mapId < 1)
            {
                MyLog.FungusLogError($"地图ID不能为空 {mapId}");
            }
            else if (sceneNameIsEmpty)
            {
                MyLog.FungusLogError($"场景名字不能为空");
            }
            else
            {
                var msg = string.Empty;
                if (npcIds.Count != 0)
                {
                  
         
                    msg += $"NPCID: [{npcIds}]";
                    foreach (var npc in npcIds)
                    {
                        NPCEx.WarpToScene(npc, sceneName);
                    }
                }

                MyLog.FungusLog($"跳转地图事件 场景名字: {sceneName} 地图ID: {mapId} {msg}");
                AvatarTransfer.Do(mapId);
                Tools.instance.loadMapScenes(sceneName);
            }
            callback?.Invoke();
        }
    }
}