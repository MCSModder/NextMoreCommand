using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.CustomMap
{
    public class CustomMapAward
    {
        public CustomMapAward(int id, int index)
        {
            ID = id;
            Index = index;
        }

        [JsonProperty("ID")] public int ID;
        [JsonProperty("index")] public int Index;
    }

    public class CustomMapEvent
    {
        public CustomMapEvent(int id, int index)
        {
            ID = id;
            Index = index;
        }

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
        [JsonProperty("Map")] public MapNodeType[,] Map;

        [JsonProperty] public List<Position> Exit;

        [JsonProperty] public List<Position> Entrance;
        [JsonProperty] public List<NextEvent> NextTrigger;
        [JsonProperty] public List<NextEvent> NextDialog;
        [JsonProperty] public List<MyBlock> Blocks;
        public Position? GetExit(int index = 0)
        {
            if (Exit.Count == 0) return null;
            if (index < 0) return Exit[0];
            return index >= Exit.Count ? Exit[Exit.Count - 1] : Exit[index];
        }

        public NextEvent GetNextTrigger(int index = 0)
        {
            if (NextTrigger.Count == 0) return null;
            if (index < 0) return NextTrigger[0];
            return index >= NextTrigger.Count ? NextTrigger[NextTrigger.Count - 1] : NextTrigger[index];
        }

        public NextEvent GetNextDialog(int index = 0)
        {
            if (NextDialog.Count == 0) return null;
            if (index < 0) return NextDialog[0];
            return index >= NextDialog.Count ? NextDialog[NextDialog.Count - 1] : NextDialog[index];
        }

        public Position? GetEntrance(int index = 0)
        {
            if (Entrance.Count == 0) return null;
            if (index < 0) return Entrance[0];
            return index >= Entrance.Count ? Entrance[Entrance.Count - 1] : Entrance[index];
        }

        public JObject ToJObject() => JObject.FromObject(this);
    }
}