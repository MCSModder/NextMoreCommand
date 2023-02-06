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
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var npc = command.ToNpcId();
            var xinQuList = command.ToListString(1)
                .Select(item => new XinQuInfo(item)).Where(item => item.isValid).ToList();

            var backpack = jsonData.instance.AvatarBackpackJsonData[npc.ToNpcId()];
            if (!backpack.HasField("XinQuType")
                || command.Command == "设置角色兴趣类型"
                || command.Command == "SetNpcXinQuType")
            {
                backpack.SetField("XinQuType", new JSONObject(JSONObject.Type.ARRAY));
            }


            var xinQuType = backpack["XinQuType"];
            var count = xinQuList.Count > 4 ? 4 : xinQuList.Count;
            for (var i = 0; i < count; i++)
            {
                var xinQu = xinQuList[i];
                backpack.Add(xinQu.XinQu);
                MyPluginMain.LogInfo(
                    $"[添加角色兴趣类]ID:{npc} 名字:{DialogAnalysis.GetNpcName(npc)} 兴趣名:{xinQu.Name} 兴趣ID:{xinQu.Type} 兴趣加成:{xinQu.Percent}%");
            }


            callback?.Invoke();
        }
    }
}