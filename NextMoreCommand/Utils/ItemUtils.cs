using System.Collections.Generic;
using GUIPackage;
using JSONClass;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class ItemUtils
{
    public static Dictionary<int, _ItemJsonData> ItemJsonData => _ItemJsonData.DataDict;
    public static List<_ItemJsonData> ItemJsonDataList => _ItemJsonData.DataList;

    public static void Get()
    {
    }

    private static readonly List<string> BanListName = new List<string>() { "暂无", "情报玉简", "林府储物袋", "天机阁情报" };

    private static readonly List<string> BanListDesc = new List<string>()
    {
        "此物品已被删除（不要在用修改器了Orz）", "暂无说明", "炼制丹药时药力过剩所产生的药渣，没有什么作用。", "当你的储物袋里出现了这个，就说明宁州告别了持续千年的安定。",
        "当你看到这个东西的时候，你就知道，你修改改出了bug了。=.=", "任务物品，不可出售"
    };

    private static readonly List<int> BanListShopType = new List<int>()
    {
        78, 100
    };

    public static bool HasBanItem(_ItemJsonData itemJsonData)
    {
        var name = itemJsonData.name;
        var banName = string.IsNullOrWhiteSpace(name) || BanListName.Contains(name) || name.Contains("储物袋");
        var banDesc = BanListDesc.Contains(itemJsonData.desc) || BanListDesc.Contains(itemJsonData.desc2);
        var banShop = itemJsonData.CanSale == 1 || BanListShopType.Contains(itemJsonData.ShopType);
        if (banName || banDesc || banShop)
        {
            return true;
        }

        return false;
    }
}