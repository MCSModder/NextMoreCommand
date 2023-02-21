using JSONClass;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc.Wudao
{
    [DialogEnvQuery("GetWudaoIdInfo")]
    [DialogEnvQuery("获得悟道ID信息")]
    public class GetWudao : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0,1);
            var wudaoID = context.GetMyArgs(1, -1);
            if (WuDaoAllTypeJson.DataDict.ContainsKey(wudaoID) && npc > 0)
            {
                switch (npc)
                {
                    case 1:

                        return WuDaoUtils.GetWudao(npc, wudaoID);
                    default:
                        return WuDaoUtils.GetWudao(npc, wudaoID);
                }
             
            }
            return null;
        }
    }
}