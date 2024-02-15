using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils;

/// <summary>
/// 角色死亡工具类扩展方法
/// </summary>
public static class DeathUtilsExtends
{
    /// <summary>
    /// 是否角色已经死亡
    /// </summary>
    /// <param name="npcId">角色ID</param>
    /// <returns>是否已死亡</returns>
    public static bool IsDeathInfo(this int npcId) => NPCEx.IsDeath(npcId.ToNpcNewId());

    /// <summary>
    /// 获得角色死亡信息
    /// </summary>
    /// <param name="npcId">角色ID</param>
    /// <returns>角色死亡信息</returns>
    public static NpcDeathInfo GetNpcDeathInfo(this int npcId) => DeathUtils.GetDeathData(npcId);

    /// <summary>
    /// 获得凶手ID
    /// </summary>
    /// <param name="npcId">角色ID</param>
    /// <returns>凶手ID</returns>
    public static int GetNpcDeathKillerId(this int npcId) => npcId.GetNpcDeathInfo().KillerId;

    /// <summary>
    /// 获得角色死亡类型名字
    /// </summary>
    /// <param name="npcId">角色ID</param>
    /// <returns>死亡类型名字</returns>
    public static string GetNpcDeathTypeName(this int npcId) => npcId.GetNpcDeathInfo().DeathTypeName;

    /// <summary>
    /// 获得角色死亡类型
    /// </summary>
    /// <param name="npcId">角色ID</param>
    /// <returns>死亡类型</returns>
    public static NpcDeathType GetNpcDeathType(this int npcId) => npcId.GetNpcDeathInfo().DeathType;

    /// <summary>
    /// 获得角色死亡的时间
    /// </summary>
    /// <param name="npcId">角色ID</param>
    /// <returns>死亡时间</returns>
    public static DateTime? GetNpcDeathDateTime(this int npcId) => npcId.GetNpcDeathInfo().GetDateTime();

    /// <summary>
    /// 获得角色死亡时间
    /// </summary>
    /// <param name="npcId">角色ID</param>
    /// <returns>死亡时间</returns>
    public static string GetNpcDeathTime(this int npcId) => npcId.GetNpcDeathInfo().DeathTime;

    /// <summary>
    /// 获得角色死亡类型
    /// </summary>
    /// <param name="num">死亡类型Id</param>
    /// <returns>死亡类型</returns>
    public static NpcDeathType GetDeathType(this int num)
    {
        return (NpcDeathType)num;
    }

    /// <summary>
    /// 获得角色死亡类型
    /// </summary>
    /// <param name="str">死亡类型名字</param>
    /// <returns>死亡类型</returns>
    public static NpcDeathType GetDeathType(this string str)
    {
        if (Enum.TryParse<NpcDeathType>(str, true, out var deathType))
        {
            return deathType;
        }

        return NpcDeathType.无;
    }

    /// <summary>
    /// 获得死亡角色类型
    /// </summary>
    /// <param name="deathType">死亡类型</param>
    /// <returns>死亡类型名字</returns>
    public static string GetTypeName(this NpcDeathType deathType)
    {
        return Enum.GetName(typeof(NpcDeathType), deathType);
    }
    /// <summary>
    /// 获得死亡角色类型
    /// </summary>
    /// <param name="deathType">死亡类型ID</param>
    /// <returns>死亡类型名字</returns>
    public static string GetDeathTypeName(this int deathType)
    {
        return Enum.GetName(typeof(NpcDeathType), deathType);
    }

    /// <summary>
    /// 获得角色类型
    /// </summary>
    /// <param name="num">角色类型Id</param>
    /// <returns>角色类型</returns>
    public static NpcType GetNpcType(this int num)
    {
        return (NpcType)num;
    }

    /// <summary>
    /// 获得角色类型
    /// </summary>
    /// <param name="str">角色类型名字</param>
    /// <returns>角色类型</returns>
    public static NpcType GetNpcType(this string str)
    {
        if (Enum.TryParse<NpcType>(str, true, out var type))
        {
            return type;
        }

        return NpcType.无;
    }

    /// <summary>
    /// 获得角色类型名字
    /// </summary>
    /// <param name="type">角色类型</param>
    /// <returns>角色类型名字</returns>
    public static string GetTypeName(this NpcType type)
    {
        return Enum.GetName(typeof(NpcType), type);
    }
    /// <summary>
    /// 获得角色类型名字
    /// </summary>
    /// <param name="type">角色类型</param>
    /// <returns>角色类型名字</returns>
    public static string GetTypeName(this int type)
    {
        return Enum.GetName(typeof(NpcType), (NpcType)type);
    }


    internal static Dictionary<int, NpcDeathInfo> GetNpcDeathInfos(this JSONObject jsonObject)
    {
        var jo = JObject.Parse(jsonObject.ToString());
        if (jo.ContainsKey("deathImportantList"))
        {
            jo.Remove("deathImportantList");
        }

        return jo.ToObject<Dictionary<int, NpcDeathInfo>>();
    }
}

/// <summary>
/// 角色死亡类型
/// </summary>
public enum NpcDeathType
{
    无,
    寿元已尽,
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

/// <summary>
/// 角色类型
/// </summary>
public enum NpcType
{
    无,
    竹山宗,
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

/// <summary>
/// 角色死亡信息
/// </summary>
public class NpcDeathInfo
{
    [JsonProperty("deathType")]     public NpcDeathType DeathType  = NpcDeathType.无;
    [JsonProperty("deathId")]       public int          DeathID    = -1;
    [JsonProperty("deathName")]     public string       DeathName  = "没有该角色";
    [JsonProperty("deathChengHao")] public string       DeathTitle = "没有该角色";
    [JsonProperty("deathTime")]     public string       DeathTime  = "5000-01-01";
    [JsonProperty("type")]          public NpcType      Type       = NpcType.无;

    /// <summary>
    /// 凶手ID 小于等于0代表 不存在凶手
    /// </summary>
    [JsonProperty("killNpcId", Required = Required.Default)]
    public int KillerId = 0;

    [JsonIgnore] public string DeathTypeName => DeathType.GetTypeName();
    [JsonIgnore] public string TypeName      => Type.GetTypeName();

    /// <summary>
    /// 获得角色死亡时间
    /// </summary>
    /// <returns></returns>
    public DateTime? GetDateTime()
    {
        if (DateTime.TryParse(DeathTime, out var dateTime))
        {
            return dateTime;
        }

        return null;
    }
}

/// <summary>
/// 角色死亡信息工具类
/// </summary>
public static class DeathUtils
{
    /// <summary>
    /// 固定角色死亡的列表ID
    /// </summary>
    public static List<int> DeathImportantList => NpcDeathJson != null && NpcDeathJson.HasField("deathImportantList")
        ? NpcDeathJson["deathImportantList"].ToList()
        : new List<int>();

    /// <summary>
    /// 角色死亡的Json
    /// </summary>
    public static JSONObject NpcDeathJson =>
        NpcDeathManager.npcDeathJson ?? new JSONObject();

    public static NPCDeath NpcDeathManager =>
        NpcJieSuanManager.inst.npcDeath;
    public static string NpcDeathJsonString =>
        NpcDeathManager.npcDeathJson.ToString(true);

    public static Dictionary<int, NpcDeathInfo> NpcDeathInfos =>
        NpcDeathJson.GetNpcDeathInfos();

    /// <summary>
    ///  获得角色死亡信息
    /// </summary>
    /// <param name="npcId"></param>
    /// <returns></returns>
    public static NpcDeathInfo GetDeathData(int npcId) =>
        npcId.IsDeathInfo() ? NpcDeathInfos[npcId.ToNpcNewId()] : new NpcDeathInfo();

    /// <summary>
    /// 获得角色死亡信息
    /// </summary>
    /// <param name="npcName"></param>
    /// <returns></returns>
    public static NpcDeathInfo GetDeathData(string npcName) =>
        GetNpcDeathList(npc => npc.DeathName == npcName).FirstOrDefault();


    /// <summary>
    ///  获得角色死亡类型的列表
    /// </summary>
    /// <param name="npcDeathType"></param>
    /// <returns></returns>
    public static List<NpcDeathInfo> GetNpcDeathTypeList(NpcDeathType npcDeathType) =>
        GetNpcDeathList(npc => npc.DeathType == npcDeathType);

    /// <summary>
    /// 获得角色死亡类型的列表
    /// </summary>
    /// <param name="npcDeathType"></param>
    /// <returns></returns>
    public static List<NpcDeathInfo> GetNpcDeathTypeList(int npcDeathType) =>
        GetNpcDeathTypeList(npcDeathType.GetDeathType());

    /// <summary>
    /// 获得角色死亡的列表
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static List<NpcDeathInfo> GetNpcDeathList(Func<NpcDeathInfo, bool> callback) =>
        NpcDeathInfos.Values.Where(callback).ToList();

    /// <summary>
    /// 获得角色死亡的列表
    /// </summary>
    /// <returns></returns>
    public static List<NpcDeathInfo> GetNpcDeathList() =>
        NpcDeathInfos.Values.ToList();

    /// <summary>
    /// 获得角色死亡类型的列表
    /// </summary>
    /// <param name="npcDeathType"></param>
    /// <returns></returns>
    public static List<NpcDeathInfo> GetNpcDeathTypeList(string npcDeathType) =>
        GetNpcDeathTypeList(npcDeathType.GetDeathType());

    /// <summary>
    /// 获得受害者的列表
    /// </summary>
    /// <param name="killerId"></param>
    /// <returns></returns>
    public static List<NpcDeathInfo> GetNpcKillerList(int killerId) =>
        GetNpcDeathList(npc => npc.KillerId == killerId.ToNpcNewId());

    /// <summary>
    /// 获得受害者数量
    /// </summary>
    /// <param name="killerId"></param>
    /// <returns></returns>
    public static int GetKillCount(int killerId) =>
        GetNpcKillerList(killerId).Count;
}