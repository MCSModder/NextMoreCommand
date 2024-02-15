using System.Collections.Generic;
using JSONClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.Custom.NPC;

[JsonObject]
public class CustomBackPack : CustomBase
{
    [JsonProperty("ID", NullValueHandling = NullValueHandling.Ignore)]
    public int Id { get; set; }

    [JsonProperty("角色ID", NullValueHandling = NullValueHandling.Ignore)]
    public int AvatarId { get; set; }

    [JsonProperty("背包名字", NullValueHandling = NullValueHandling.Ignore)]
    public string BackpackName { get; set; } = string.Empty;

    [JsonProperty("背包类型")] public int Type    { get; set; }
    [JsonProperty("品阶")]   public int Quality { get; set; }

    [JsonProperty("物品ID")]     public List<int> ItemID      { get; set; } = new List<int>();
    [JsonProperty("随机概率")]     public List<int> RandomNum   { get; set; } = new List<int>();
    [JsonProperty("是否售价")]     public int       CanSell     { get; set; }
    [JsonProperty("出售物品固定系数")] public int       SellPercent { get; set; }
    [JsonProperty("是否掉落")]     public int       CanDrop     { get; set; }

    public override JObject ToJObject()
    {
        return JObject.FromObject(new BackpackJsonData()
        {
            AvatrID = AvatarId,
            id = Id,
            BackpackName = BackpackName,
            Type = Type,
            quality = Quality,
            ItemID = ItemID,
            randomNum = RandomNum,
            CanDrop = CanDrop,
            CanSell = CanSell,
            SellPercent = SellPercent
        });
    }
}