using System.Collections.Generic;
using JSONClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.Custom.NPC;

[JsonObject]
public class CustomNpcImportantDate : CustomBase
{
    [JsonProperty("流派")] public int LiuPai { get; set; } = 1;

    [JsonProperty("性格")] public int XingGe { get; set; } = 1;

    [JsonProperty("绑定称号")] public int ChengHao { get; set; } = 1;

    [JsonProperty("绑定标签")] public int NpcTag { get; set; } = 1;

    [JsonProperty("突破筑基时间")] public string ZhuJiTime { get; set; } = string.Empty;

    [JsonProperty("突破金丹时间")] public string JinDanTime { get; set; } = string.Empty;

    [JsonProperty("突破元婴时间")] public string YuanYingTime { get; set; } = string.Empty;

    [JsonProperty("突破化神时间")] public string HuaShengTime { get; set; } = string.Empty;

    [JsonProperty("大师兄")] public int DaShiXiong { get; set; }

    [JsonProperty("掌门")] public int ZhangMeng { get; set; }

    [JsonProperty("长老")] public int ZhangLao { get; set; }

    [JsonProperty("截杀变量")] public List<int> EventValue { get; set; } = new List<int>();


    [JsonProperty("变量关系")] public string Fuhao   { get; set; } = string.Empty;
    [JsonIgnore]           public int    Id      { get; set; }
    [JsonProperty("出场境界")] public int    Level   { get; set; } = 1;
    [JsonIgnore]           public int    SexType { get; set; } = 1;
    [JsonProperty("出场资质")] public int    ZiZhi   { get; set; } = 15;
    [JsonProperty("出场年龄")] public int    Age     { get; set; } = 16;
    [JsonProperty("出场悟性")] public int    WuXin   { get; set; } = 15;

    public override JObject ToJObject()
    {
        return JObject.FromObject(new NPCImportantDate()
        {
            id = Id,
            level = Level,
            sex = SexType,
            zizhi = ZiZhi,

            nianling = Age,
            wuxing = WuXin,
            LiuPai = LiuPai,
            ChengHao = ChengHao,
            DaShiXiong = DaShiXiong,
            EventValue = EventValue,
            XingGe = XingGe,
            NPCTag = NpcTag,
            ZhangMeng = ZhangMeng,
            ZhangLao = ZhangLao,
            ZhuJiTime = ZhuJiTime,
            JinDanTime = JinDanTime,
            YuanYingTime = YuanYingTime,
            HuaShengTime = HuaShengTime,
            fuhao = Fuhao,
        });
    }
}