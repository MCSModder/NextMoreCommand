using System.Collections.Generic;
using Newtonsoft.Json;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    [JsonObject]
    public class MenPaiShopItemInfo
    {
        [JsonProperty("物品编号")]
        public int Id = 0;
        [JsonProperty("物品上限")]
        public int Limit = 0;
        [JsonProperty("物品信息")]
        public Dictionary<string, string> DataInfo = new Dictionary<string, string>();
    }
}