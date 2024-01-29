using System.Linq;
using JSONClass;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetExp")]
    public class GetExp : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
      

            return (int)context.Env.player.exp;

        }
    }
}