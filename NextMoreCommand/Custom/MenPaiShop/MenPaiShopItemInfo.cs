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
    }
}