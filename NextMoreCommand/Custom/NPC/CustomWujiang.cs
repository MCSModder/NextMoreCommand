using System.Collections.Generic;
using JSONClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.Custom.NPC;

[JsonObject]
public class CustomWujiang : CustomBase
{
    [JsonProperty("武将ID")] public int Id { get; set; }
    [JsonProperty("角色ID绑定")] public List<int> Avatar { get; set; } = new List<int>();

    [JsonProperty("开始时间")] public string TimeStart { get; set; } = string.Empty;
    [JsonProperty("结束时间")] public string TimeEnd { get; set; } = string.Empty;

    [JsonProperty("名字", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("称号", NullValueHandling = NullValueHandling.Ignore)]
    public string Title { get; set; }

    [JsonProperty("立绘", NullValueHandling = NullValueHandling.Ignore)]
    public int Image { get; set; } = 0;
    [JsonProperty("拍卖行", NullValueHandling = NullValueHandling.Ignore)] public int PaiMaiHang { get; set; }= 0;

    public override JObject ToJObject()
    {
        return JObject.FromObject(new WuJiangBangDing()
        {
            id = Id,
            avatar = Avatar,
            TimeEnd = TimeEnd,
            TimeStart = TimeStart,
            Name = Name,
            Title = Title,
            Image = Image,
            PaiMaiHang = PaiMaiHang
        });
    }
}