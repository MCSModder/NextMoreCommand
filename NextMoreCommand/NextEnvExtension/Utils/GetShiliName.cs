using System.Collections.Generic;
using JSONClass;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils;

[DialogEnvQuery("GetShiliName")]
[DialogEnvQuery("获得势力名字")]
public class GetShiliName : IDialogEnvQuery
{
    public object Execute(DialogEnvQueryContext context)
    {
        var type = context.GetMyArgs(0, 0);
        return ShiLiHaoGanDuName.DataDict[type].ChinaText;
    }
}