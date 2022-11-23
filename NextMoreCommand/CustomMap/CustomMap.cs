using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.CustomMap
{

    public class CustomMapAward
    {
        [JsonProperty("ID")] public int ID;
        [JsonProperty("index")] public int Index;
    }
    public class CustomMapEvent
    {
        [JsonProperty("ID")] public int ID;
        [JsonProperty("index")] public int Index;
    }
    public class CustomMap
    {
        // 初始化副本时间
        [JsonProperty("startTime")] public string StartTime;
        // 可不可重置副本
        [JsonProperty("ShouldReset")] public bool ShouldReset;
        // 副本类型
        [JsonProperty("type")] public int Type;
        // 副本属性
        [JsonProperty("ShuXin")] public int ShuXing;
        // 副本难度
        [JsonProperty("NamDu")] public int NanDu;
        // 副本Y轴长度
        [JsonProperty("high")] public int High;
        // 副本X轴长度
        [JsonProperty("wide")] public int Wide;
        // 副本名字
        [JsonProperty("Name")] public string Name;
        // 副本UUID
        [JsonProperty("UUID")] public string Uuid;
        // 副本奖励
        [JsonProperty("Award")] public CustomMapAward[] Award;
        // 副本事件
        [JsonProperty("Event")] public CustomMapEvent[] Event;
        // 副本地图
        [JsonProperty("Map")] public FuBenMap.NodeType[,]  Map;
        public JObject ToJObject()=>JObject.FromObject(this);
    }

 
   
    
}