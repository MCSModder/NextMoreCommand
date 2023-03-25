using Newtonsoft.Json;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    [JsonObject]
    public class MenPaiShopInfo
    {
        public string Id= string.Empty;
        [JsonProperty("商店名",Required = Required.Always)]
        public string Title = string.Empty;
        [JsonProperty("商店右边",NullValueHandling = NullValueHandling.Ignore)]
        public string ShopLeft = string.Empty;
        [JsonProperty("商店中间",NullValueHandling = NullValueHandling.Ignore)]
        public string ShopMiddle = string.Empty;
        [JsonProperty("商店左边",NullValueHandling = NullValueHandling.Ignore)]
        public string ShopRight = string.Empty;
        [JsonProperty("货币",NullValueHandling = NullValueHandling.Ignore)]
        public int MoneyType = 0;
    }
}