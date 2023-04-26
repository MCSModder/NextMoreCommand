using System;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{

    [DialogEnvQuery("GetUINpcData")]
    [DialogEnvQuery("获取交互角色数据")]
    public class GetUINpcData : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npcId = context.GetNpcID(0, 0);

            if (npcId <= 1) return null;
            var npc = new UINPCData(npcId);
            npc.RefreshData();
            return npc;
        }
    }
}