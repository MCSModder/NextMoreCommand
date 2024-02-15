using System;
using System.Collections.Generic;
using System.Linq;
using MCSSubscribeDependencies;
using script.Steam;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("HasModActive")]
    public class HasModActive : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {

            if (ulong.TryParse(context.GetMyArgs<string>(0), out var modId) && WorkshopUtils.WorkShopItemsDict.ContainsKey(modId))
            {
                return !WorkshopTool.CheckModIsDisable(modId.ToString());
            }
            return false;

        }
    }
}