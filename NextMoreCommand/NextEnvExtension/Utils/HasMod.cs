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
            var modId = context.GetMyArgs<ulong>(0);
            return WorkshopUtils.WorkShopItems?.Exists(item => item.PublishedFileId.m_PublishedFileId == modId);
        }
    }
}