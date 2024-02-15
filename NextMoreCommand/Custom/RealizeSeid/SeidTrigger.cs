using System;
using System.Collections.Generic;
using System.Linq;
using MiniExcelLibs;
using MiniExcelLibs.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.Lua;
using SkySwordKill.NextMoreCommand.Custom.RealizeSeid.Skills;
using UnityEngine;
using XLua;

namespace SkySwordKill.NextMoreCommand.Custom.RealizeSeid;

public class SeidTrigger
{
    [ExcelColumnIndex(0)]
    [JsonProperty(Order = 0, Required = Required.Always)]

    public string Id { get; set; } = "";

    [ExcelColumnIndex(1)]
    [JsonProperty(Order = 1, Required = Required.Always)]
    public string Condition { get; set; }

    [ExcelColumnIndex(2)]
    [JsonProperty(Order = 2, Required = Required.Always)]
    public string LuaFilename { get; set; }


    public SeidTrigger()
    {
    }

    public SeidTrigger(string luaFilename = "", string condition = "")
    {
        LuaFilename = luaFilename;
        Condition = condition;
    }

    [ExcelIgnore] [JsonIgnore] private LuaManager        LuaManager   => Main.Lua;
    [ExcelIgnore] [JsonIgnore] public  LuaFileCache      LuaFileCache => LuaManager.LuaCaches[LuaFilename];
    [ExcelIgnore] [JsonIgnore] public  DialogEnvironment DialogEnvironment = new DialogEnvironment();

    [ExcelIgnore]
    [JsonIgnore]
    public bool IsCheck => string.IsNullOrWhiteSpace(Condition) ||
                           DialogAnalysis.CheckCondition(Condition, DialogEnvironment);

    public static void TestSaveExcel()
    {
        var trigger = new SeidTrigger("TestReailzed", "")
        {
            Id = "TEST"
        };
        MyPluginMain.LogInfo(BepInEx.Paths.GameRootPath);
        MiniExcel.SaveAs(BepInEx.Paths.GameRootPath + "/test.xlsx", new[] { trigger, trigger, trigger });
        MiniExcel.SaveAs(BepInEx.Paths.GameRootPath + "/test.csv", new[] { trigger, trigger, trigger },
            excelType: ExcelType.CSV);
    }

    public static Dictionary<string, SeidTrigger> SeidTriggers = new Dictionary<string, SeidTrigger>();

    public static void TestLoadExcel()
    {
        MyPluginMain.LogInfo(BepInEx.Paths.GameRootPath);
        var list = MiniExcel.Query<SeidTrigger>(BepInEx.Paths.GameRootPath + "/test.xlsx").ToList();
        foreach (var iTrigger in list)
        {
            SeidTriggers[iTrigger.Id] = iTrigger;
        }

        list = MiniExcel.Query<SeidTrigger>(BepInEx.Paths.GameRootPath + "/test.csv", excelType: ExcelType.CSV)
            .ToList();
        foreach (var iTrigger in list)
        {
            SeidTriggers[iTrigger.Id] = iTrigger;
        }
    }

    public static void TestJson()
    {
        var trigger = new SeidTrigger("TestReailzed", "")
        {
            Id = "TEST"
        };
        MyPluginMain.LogInfo(JObject.FromObject(trigger));
    }
}