using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.Utils;

enum AreaUnlockType
{
    炼器,
    炼丹,
    灵田,
    仓库,
    练功
}

[JsonObject]
public class LingTianInfo
{
    public int ID;
    public int LingLi;
}

public class DongFuInfo
{
    [JsonProperty("DongFuName")] public string DongFuName;
    [JsonProperty("LingYanLevel")] public int LingYanLevel;
    [JsonProperty("JuLingZhenLevel")] public int JuLingZhenLevel;
    [JsonProperty("Area0Unlock")] public int Area0Unlock;
    [JsonProperty("Area1Unlock")] public int Area1Unlock;
    [JsonProperty("Area2Unlock")] public int Area2Unlock;
    [JsonProperty("Area3Unlock")] public int Area3Unlock;
    [JsonProperty("Area4Unlock")] public int Area4Unlock;
    [JsonProperty("LingTian")] public Dictionary<string, object> LingTian;
    [JsonIgnore] public string ID;

    [JsonIgnore] public Dictionary<string, LingTianInfo> LingTianDict => GetLingTianDict();

    [JsonIgnore] public int CuiShengLingLi => (int)LingTian["CuiShengLingLi"];

    public Dictionary<string, LingTianInfo> GetLingTianDict()
    {
        var dict = new Dictionary<string, LingTianInfo>();
        foreach (var item in LingTian)
        {
            if (item.Key == "CuiShengLingLi")
            {
                continue;
            }

            if (item.Value is JObject value)
            {
                dict[item.Key] = value.ToObject<LingTianInfo>();
            }
        }

        return dict;
    }
}

public static class DongFuUtils
    {
        public static JSONObject DongFuData => PlayerEx.Player.DongFuData;
        public static string DongFuDataStr => DongFuData.ToString(true);

        public static Dictionary<string, DongFuInfo> DongFuDataInfos =>
            DongFuData.ToJObject().ToObject<Dictionary<string, DongFuInfo>>();

        public static readonly Dictionary<int, string> DongFuName = new Dictionary<int, string>();

        public static DongFuInfo GetDongFuData(int dongFuID)
        {
            if (DongFuManager.PlayerHasDongFu(dongFuID))
            {
                return DongFuData[$"DongFu{dongFuID}"].ToJObject().ToObject<DongFuInfo>();
            }

            return null;
        }

        public static void CreateDongFu(int dongFuID, int level, string dongFuName)
        {
            DongFuManager.CreateDongFu(dongFuID, level);
            DongFuManager.SetDongFuName(dongFuID, dongFuName);
        }

        public static Dictionary<string, DongFuInfo> GetDongFuDataInfos()
        {
            DongFuName.Clear();
            var data = DongFuData.ToJObject().ToObject<Dictionary<string, DongFuInfo>>() ??
                       new Dictionary<string, DongFuInfo>();
            foreach (var item in data)
            {
                item.Value.ID = item.Key;
                var dongFuIndex = Convert.ToInt32(item.Key.Replace("DongFu", ""));
                DongFuName[dongFuIndex] = item.Value.DongFuName;
            }

            return data;
        }
    }