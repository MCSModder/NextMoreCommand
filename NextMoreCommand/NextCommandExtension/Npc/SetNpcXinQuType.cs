using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetNpcXinQuType")]
    [DialogEvent("设置角色兴趣类型")]
    [DialogEvent("AddNpcXinQuType")]
    [DialogEvent("添加角色兴趣类型")]
    public class SetNpcXinQuType : IDialogEvent
    {
        private int npc;
        private List<string> list;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            list = command.ToListString(1);
            MyLog.Log(command,
                $"角色ID:{npc} 角色名:{npc.GetNpcName()} 兴趣列表:{JArray.FromObject(list).ToString(Formatting.None)}");
            if (npc > 0)
            {
                var tempList = list
                    .Select(item => new XinQuInfo(item));
                var xinQuInfos = tempList.ToList();
                var xinQuList = xinQuInfos.Where(item => item.IsValid).ToList();
                MyLog.Log(command,
                    $"角色ID:{npc} 角色名:{npc.GetNpcName()} 兴趣列表:{JArray.FromObject(xinQuList.Select(item => item.Name)).ToString(Formatting.None)}");

                var backpack = jsonData.instance.AvatarBackpackJsonData[npc.ToNpcId()];
                if (!backpack.HasField("XinQuType")
                    || command.Command == "设置角色兴趣类型"
                    || command.Command == "SetNpcXinQuType")
                {
                    MyLog.Log(command, $"创建新兴趣列表");
                    backpack.SetField("XinQuType", new JSONObject(JSONObject.Type.ARRAY));
                }


                var xinQuType = backpack["XinQuType"];
                for (var i = 0; i < xinQuList.Count; i++)
                {
                    var xinQu = xinQuList[i];
                    xinQuType.Add(xinQu.XinQu);
                    MyLog.Log(command,
                        $"角色ID:{npc} 角色名:{npc.GetNpcName()} 兴趣名:{xinQu.Name} 兴趣ID:{xinQu.Type} 兴趣加成:{xinQu.Percent}%");
                }
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