using System;
using System.Collections.Generic;
using System.Linq;
using MCSSubscribeDependencies;
using script.Steam;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("HasMod")]
    public class HasMod : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return ulong.TryParse(context.GetMyArgs<string>(0), out var modId) && (WorkshopUtils.WorkShopItems?.Exists(item => item.PublishedFileId.m_PublishedFileId == modId) ?? false);

        }
    }
}