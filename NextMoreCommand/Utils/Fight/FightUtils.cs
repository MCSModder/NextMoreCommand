using System.Collections.Generic;
using System.Linq;
using Fungus;
using MiniExcelLibs.Attributes;
using Newtonsoft.Json;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils.Fight;

[JsonObject]
public class BuffInfo
{

    public static List<BuffInfo> ParseBuffInfo(string raw)
    {
        return raw.Split('|').Where(text => !string.IsNullOrWhiteSpace(text)).Select(buff => new BuffInfo(buff)).ToList();
    }
    [JsonProperty("ID")] public int    ID    = 0;
    [JsonProperty("数量")] public int    Count = 0;
    [JsonIgnore]         public string Raw   = string.Empty;

    private Tools Tools   => Tools.instance;
    public  bool  IsEmpty => string.IsNullOrWhiteSpace(Raw);
    public BuffInfo(string str)
    {
        Raw = str;
        var split = str.Split(new[] { ',' }, 2);
        if (split.Length == 2)
        {
            ID = int.Parse(split[0]);
            Count = int.Parse(split[1]);
        }
        else
        {
            ID = int.Parse(Raw);
            Count = 1;
        }
    }
    public void SetPlayer()
    {
        var player = Tools.monstarMag.HeroAddBuff;
        if (player.ContainsKey(ID))
        {
            player[ID] += Count;
        }
        else
        {
            player.Add(ID, Count);
        }
    }
    public void SetEnemy()
    {
        var enemy = Tools.monstarMag.monstarAddBuff;
        if (enemy.ContainsKey(ID))
        {
            enemy[ID] += Count;
        }
        else
        {
            enemy.Add(ID, Count);
        }
    }
}

public interface IFightInfo
{
    public int                      Id           { get; set; }
    public int                      NpcId        { get; set; }
    public int                      CanRun       { get; set; }
    public StartFight.FightEnumType FightType    { get; set; }
    public int                      Background   { get; set; }
    public string                   Music        { get; set; }
    public List<BuffInfo>           PlayerBuff   { get; set; }
    public List<BuffInfo>           EnemyBuff    { get; set; }
    public string                   VictoryEvent { get; set; }
    public string                   DefeatEvent  { get; set; }
    public List<string>             Tags         { get; set; }
}

[JsonObject]
public class FightInfoJson : IFightInfo
{
    [JsonProperty("战斗事件ID", Required = Required.Always, Order = 0)]
    public int Id { get; set; }

    [JsonProperty("角色ID", Required = Required.Always, Order = 1)]
    public int NpcId { get; set; } = 0;

    [JsonProperty("能否逃跑")] public int CanRun { get; set; } = 0;

    [JsonProperty("战斗类型", Required = Required.Always, Order = 2)]
    public StartFight.FightEnumType FightType { get; set; } = StartFight.FightEnumType.Normal;

    [JsonProperty("战斗背景")]   public int            Background   { get; set; } = 0;
    [JsonProperty("战斗背景")]   public string         Music        { get; set; } = string.Empty;
    [JsonProperty("战斗标签")]   public List<string>   Tags         { get; set; } = new List<string>();
    [JsonProperty("玩家BUFF")] public List<BuffInfo> PlayerBuff   { get; set; } = new List<BuffInfo>();
    [JsonProperty("敌方BUFF")] public List<BuffInfo> EnemyBuff    { get; set; } = new List<BuffInfo>();
    [JsonProperty("胜利事件")]   public string         VictoryEvent { get; set; } = string.Empty;
    [JsonProperty("战败事件")]   public string         DefeatEvent  { get; set; } = string.Empty;
}

public class FightInfoExcel : IFightInfo
{
    [ExcelColumnName("战斗事件ID")] public int Id { get; set; }

    [ExcelColumnName("角色ID")] public int NpcId { get; set; } = 0;

    [ExcelColumnName("能否逃跑")] public int CanRun { get; set; } = 0;


    [ExcelIgnore] public StartFight.FightEnumType FightType { get; set; } = StartFight.FightEnumType.Normal;

    [ExcelColumnName("战斗类型")]
    private int FightTypes
    {
        get => (int)FightType;
        set => FightType = (StartFight.FightEnumType)value;
    }

    [ExcelColumnName("战斗背景")] public int            Background { get; set; } = 0;
    [ExcelColumnName("战斗背景")] public string         Music      { get; set; } = string.Empty;
    [ExcelIgnore]             public List<string>   Tags       { get; set; } = new List<string>();
    [ExcelIgnore]             public List<BuffInfo> PlayerBuff { get; set; } = new List<BuffInfo>();
    [ExcelIgnore]             public List<BuffInfo> EnemyBuff  { get; set; } = new List<BuffInfo>();

    [ExcelColumnName("战斗标签")]
    public string RawTags
    {
        get => GetTagsToString();
        set => SetStringToTags(value);
    }

    [ExcelColumnName("玩家BUFF")] public string RawPlayerBuff { get; set; }
    [ExcelColumnName("敌方BUFF")] public string RawEnemyBuff  { get; set; }
    [ExcelColumnName("胜利事件")]   public string VictoryEvent  { get; set; } = string.Empty;
    [ExcelColumnName("战败事件")]   public string DefeatEvent   { get; set; } = string.Empty;

    private string GetTagsToString()
    {
        var count = Tags.Count - 1;
        var msg   = "";
        for (var i = 0; i <= count; i++)
        {
            if (i == count)
            {
                msg += $"{Tags[i]}";
            }
            else
            {
                msg += $"{Tags[i]},";
            }
        }

        return msg;
    }

    private void SetStringToTags(string str)
    {
        Tags = str.Split(',').ToList();
    }

}

public static class FightUtils
{
    public static List<BuffInfo> ParseBuffInfo(this DialogCommand command, int index = 0, string defaultValue = "")
    {
        return BuffInfo.ParseBuffInfo(command.GetStr(index, defaultValue));
    }
}