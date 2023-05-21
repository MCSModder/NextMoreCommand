using System;
using System.Collections.Generic;
using Fungus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [RegisterCommand]
    [DialogEvent("SetTeleport")]
    [DialogEvent("设置传送")]
    public class SetTeleport : IDialogEvent
    {
        private List<int> npcIds;
        private int mapIndex;
        private string sceneName;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            mapIndex = command.GetInt(0, -1);
            sceneName = command.GetStr(1, string.Empty);
            npcIds = command.ToNpcListId(2);

            MyLog.Log(command, $"当前场景:{Tools.getScreenName()} 地图索引:{mapIndex}");
            MyLog.Log(command, $"传送场景:{sceneName} 角色列表:{JArray.FromObject(npcIds).ToString(Formatting.None)}");
            if (mapIndex < 0)
            {
                MyLog.Log(command, $"地图索引:{mapIndex} 不能小于0", true);
            }
            else if (string.IsNullOrWhiteSpace(sceneName))
            {
                MyLog.Log(command, $"传送场景:{mapIndex} 不能为空", true);
            }
            else
            {
                foreach (var npc in npcIds)
                {
                    NPCEx.WarpToScene(npc, sceneName);
                    MyLog.Log(command, $"场景名:{sceneName} 角色ID:{npc} 角色名:{npc.GetNpcName()}");
                }

                MyLog.Log(command, $"开始传送场景 传送场景:{sceneName} ");
                AvatarTransfer.Do(mapIndex);
                Tools.instance.loadMapScenes(sceneName);
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}