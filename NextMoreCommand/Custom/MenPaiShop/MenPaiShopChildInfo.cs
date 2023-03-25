using System.Collections.Generic;
using Newtonsoft.Json;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    [JsonObject]
    public class MenPaiShopChildInfo
    {
        [JsonProperty("编号")]
        public string Id = string.Empty;
        [JsonProperty("商店名")]
        public string Title = string.Empty;
        [JsonProperty("商店类型")]
        public string Type = string.Empty;
        [JsonProperty("价格倍率")]
        public float Ratio
        {
            get => _ratio;
            set
            {
                if (value <= 0)
                {
                    _ratio = 1f;
                    return;
                }
                _ratio = value;
            }
        }
        private float _ratio = 1f;
        [JsonProperty("物品栏")]
        public List<MenPaiShopItemInfo> ItemData = new List<MenPaiShopItemInfo>();
    }
}