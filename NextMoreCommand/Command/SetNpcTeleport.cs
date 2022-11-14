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
    [DialogEvent("SetNpcTeleport")]
    public class SetNpcTeleport : IDialogEvent
    {
        private string _sceneName;
        private List<int> _npcIds = new List<int>();
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            _sceneName = command.GetStr(0, string.Empty);
            if (command.ParamList.Length > 2)
            {
                for (int i = 0; i < command.ParamList.Length; i++)
                {
                    if (i != 0)
                    {
                        var npc = command.GetInt(i, -1);
                        if (npc >= 0)
                        {
                            _npcIds.Add(NPCEx.NPCIDToNew(npc));
                        }
                    }
                }
            }
            else
            {
                var npcId = command.GetStr(1, string.Empty);
                var npcArr = npcId.Split(',') ;
                foreach (var npc in npcArr)
                {
                    _npcIds.Add(NPCEx.NPCIDToNew(Convert.ToInt32(npc)));
                }
            }
           
           

            var sceneNameIsEmpty = _sceneName == string.Empty;
            if (sceneNameIsEmpty)
            {
                MyLog.FungusLogError($"场景名字不能为空");
            }
            else if(_npcIds.Count == 0)
            {
                MyLog.FungusLogError($"NPC不能为空");
            }
            else
            {
                
                foreach (var npc in _npcIds)
                {
                    NPCEx.WarpToScene(npc, _sceneName);
                }
                MyLog.FungusLog($"跳转地图事件 场景名字: {_sceneName}  NPCID: {_npcIds}");
            }
        }
    }
}