using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{

    [DialogEnvQuery("GetNpcMoney")]
    public class GetNpcMoney : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            var npcId = context.GetArg(0,-1);
            if (npcId < 0)
            {
                return 0 as object;
            }
            return   jsonData.instance.AvatarBackpackJsonData[NPCEx.NPCIDToNew(npcId).ToString()]["money"].I as object;;
        }
    }
}