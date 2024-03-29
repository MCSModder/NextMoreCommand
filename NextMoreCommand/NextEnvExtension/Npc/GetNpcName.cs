﻿using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetNpcName")]
    [DialogEnvQuery("获得角色名字")]
    public class GetNpcName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npcId = context.GetNpcID(0, -1);
            if (npcId < 0)
            {
                return "";
            }

            var name = DialogAnalysis.GetNpcName(npcId);
            MyPluginMain.LogInfo($"npcId:{npcId.ToString()} name:{name}");
            return name;
        }
    }
}