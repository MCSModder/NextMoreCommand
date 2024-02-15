using System.Collections.Generic;
using System.Linq;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextSeachNpcExtension
{

    [SearchNpcMatch("搜索物品")]
    public class SearchItem : ISearchNpcMatch
    {

        public List<string> Alias { get; } = new List<string>()
        {
            "item",
            "it",
            "物",
            "物品"
        };
        public bool Match(SearchNpcDataInfo searchNpcDataInfo)
        {
            var itemList = searchNpcDataInfo.GetItemList().Select(item => item.itemName);
            var itemMatch = searchNpcDataInfo.ValueSplit('|').Where(text => !string.IsNullOrWhiteSpace(text)).Select(text =>
            {
                if (int.TryParse(text, out var id) && SearchNpcDataManager.TryGetItem(id, out var item))
                {
                    return item.itemName;
                }
                return text;
            }).ToList();
            foreach (var itemName in itemList)
            {
                foreach (var match in itemMatch.Where(match => itemName.Contains(match)))
                {
                    MyLog.Log("触发NPC搜索匹配", $"[匹配字段:{match} 物品:{itemName}]");
                    return true;
                }

            }
            return false;
        }
    }
}