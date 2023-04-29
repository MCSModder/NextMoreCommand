using System;
using System.Collections.Generic;
using System.Linq;
using MCSSubscribeDependencies;
using script.Steam;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetModInfo")]
    public class GetModInfo : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            if (ulong.TryParse(context.GetMyArgs<string>(0), out var modId) && WorkshopUtils.WorkShopItemsDict.TryGetValue(modId, out var value))
            {
                return new WorkshopUtils.ModInfo(value);
            }
            return null;

        }
    }
}