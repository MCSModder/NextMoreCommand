using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class DeathExtends
{
    public static string GetTypeName(this NpcDeathType deathType)
    {
        return Enum.GetName(typeof(NpcDeathType), deathType);
    }
    public static string GetTypeName(this NpcType deathType)
    {
        return Enum.GetName(typeof(NpcType), deathType);
    }
    public static Dictionary<int, NpcDeathInfo> GetNpcDeathInfos(this JSONObject jsonObject)
    {
        var jo = JObject.Parse(jsonObject.ToString());
        if (jo.ContainsKey("deathImportantList"))
        {
            DeathUtils.DeathImportantList = jo["deathImportantList"]?.ToObject<List<int>>();
            jo.Remove("deathImportantList");
        }
        
        return jo.ToObject<Dictionary<int, NpcDeathInfo>>();
    }
}

public enum NpcDeathType
{
    寿元已尽 = 1,
    被玩家打死,
    游历时意外身亡,
    被妖兽反杀,
    被其它Npc截杀,
    做宗门任务死了,
    做主城任务死了,
    炼丹被炸死,
    炼器被炸死,
    不明原因死亡,
    截杀时被反杀,
    飞升失败
}

public enum NpcType
{
    竹山宗 = 1,
    金虹剑派,
    星河剑派,
    离火门,
    化尘教,
    倪府,
    林府,
    百里府,
    公孙府,
    散修,
    白帝楼,
    天机阁,
    沂山派,
    禾山道,
    蓬莎岛,
    碎星岛,
    千流岛,
    古神教,
    天魔道,
    血剑宫,
    风雨楼,
    普通杀手,
    星宫,
    废弃,
    万魂殿
}

public class NpcDeathInfo
{
    [JsonProperty("deathType")] public NpcDeathType DeathType;
    [JsonProperty("deathId")] public int DeathID;
    [JsonProperty("deathName")] public string DeathName;
    [JsonProperty("deathChengHao")] public string DeathTitle;
    [JsonProperty("deathTime")] public string DeathTime;
    [JsonProperty("type")] public NpcType Type;

    [JsonProperty("killNpcId", Required = Required.Default)]
    public int KillerId = 0;

    [JsonIgnore] public string DeathTypeName => DeathType.GetTypeName();
    [JsonIgnore] public string TypeName => Type.GetTypeName();

    public DateTime? GetDateTime()
    {
        if (DateTime.TryParse(DeathTime, out var dateTime))
        {
            return dateTime;
        }

        return null;
    }
}

public static class DeathUtils
{
    public static List<int> DeathImportantList;
    public static JSONObject npcDeathJson =>
        NpcJieSuanManager.inst.npcDeath.npcDeathJson;
    public static string npcDeathJsonString =>
        NpcJieSuanManager.inst.npcDeath.npcDeathJson.ToString(true);
    public static Dictionary<int, NpcDeathInfo> NpcDeathInfos =>
        npcDeathJson.GetNpcDeathInfos();

    public static NpcDeathInfo GetDeathData(int npcId) => NpcDeathInfos[npcId.ToNpcNewId()];

    public static int GetKillCount(int killerId) =>
        NpcDeathInfos.Values.Where(item => item.KillerId == killerId.ToNpcNewId()).ToList().Count;
}