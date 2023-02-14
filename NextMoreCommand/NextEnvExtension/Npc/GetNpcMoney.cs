using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("GetNpcMoney")]
    public class GetNpcMoney : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npcId = context.GetNpcID(0, -1);
            if (npcId <= 0)
            {
                return 0 as object;
            }

            return jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["money"].I as object;
            ;
        }
    }
}