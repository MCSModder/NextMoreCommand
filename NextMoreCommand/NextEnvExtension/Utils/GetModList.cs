using System.Collections.Generic;
using System.Linq;
using MCSSubscribeDependencies;
using script.Steam;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetModList")]
    public class GetModList : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return WorkshopUtils.WorkShopItems;
        }
    }
}