using System;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtesion
{

    [DialogEnvQuery("GetNpcName")]
    public class GetNpcName : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            var npcId = context.GetArg(0,-1);
            if (npcId < 0)
            {
                return "";
            }
            var name = DialogAnalysis.GetNpcName(npcId);
            Main.LogInfo($"npcId:{npcId} name:{name}");
            return name;
        }
    }
}