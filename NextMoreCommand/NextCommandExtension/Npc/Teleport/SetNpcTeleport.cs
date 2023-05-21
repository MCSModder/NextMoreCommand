using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc.Teleport
{
    [RegisterCommand]
    [DialogEvent("SetNpcTeleport")]
    [DialogEvent("设置角色传送")]
    public class SetNpcTeleport : IDialogEvent
    {
        private string sceneName;
        private List<int> npcIds;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            sceneName = command.GetStr(0, string.Empty);
            npcIds = command.ToNpcListId(1);
            var str = JArray.FromObject(npcIds).ToString(Formatting.None);
            MyLog.Log(command, $"场景名:{sceneName} 角色列表:{str}");


            if (string.IsNullOrWhiteSpace(sceneName))
            {
                MyLog.Log(command, $"场景名:{sceneName} 不能为空", true);
            }
            else if (npcIds.Count == 0)
            {
                MyLog.Log(command, $"角色列表:{str} 不能为空", true);
            }
            else
            {
                foreach (var npc in npcIds)
                {
                    NPCEx.WarpToScene(npc, sceneName);
                    MyLog.Log(command, $"场景名:{sceneName} 角色ID:{npc} 角色名:{npc.GetNpcName()}");
                }

                MyLog.Log(command, $"传送完毕 场景名:{sceneName} 角色列表:{str}");
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}