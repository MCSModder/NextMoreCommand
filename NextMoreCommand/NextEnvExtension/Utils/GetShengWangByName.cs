using System.Collections.Generic;
using JSONClass;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils;

[DialogEnvQuery("GetShengWangByName")]
[DialogEnvQuery("获得势力声望")]
public class GetShengWangByName : IDialogEnvQuery
{
    public Dictionary<string, int> Shili = new();
    public object Execute(DialogEnvQueryContext context)
    {
        var type = context.GetMyArgs(0, "宁州");
        if (Shili.Count == 0)
        {
            foreach (var shiLi in ShiLiHaoGanDuName.DataList)
            {
                Shili.Add(shiLi.ChinaText, shiLi.id);
            }
        }
        var id = 0;
        if (type == "宗门")
        {
            id = PlayerEx.Player.menPai;
        }
        else if (Shili.ContainsKey(type))
        {
            id = Shili[type];
        }
        return PlayerEx.GetShengWang(id);
    }
}