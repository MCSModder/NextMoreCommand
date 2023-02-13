using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.Utils;

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
    public DongFuInfo(string id)
    {
        RawID = id;
    }

    public string RawID;
    public JSONObject JsonRaw => DongFuUtils.DongFuData[RawID];

    public int ID => Convert.ToInt32(RawID.Replace("DongFu", ""));

    public string DongFuName
    {
        get => GetStr("DongFuName");
        set => SetField("DongFuName", value);
    }


    public int LingYanLevel
    {
        get => GetInt("LingYanLevel");
        set => SetField("LingYanLevel", value);
    }

    public int JuLingZhenLevel
    {
        get => GetInt("JuLingZhenLevel");
        set => SetField("JuLingZhenLevel", value);
    }

    public int LianQiFang
    {
        get => GetInt("Area0Unlock");
        set => SetField("Area0Unlock", value);
    }

    public int LianDanFang
    {
        get => GetInt("Area1Unlock");
        set => SetField("Area1Unlock", value);
    }

    public int LingTianArea
    {
        get => GetInt("Area2Unlock");
        set => SetField("Area2Unlock", value);
    }

    public int CangKu
    {
        get => GetInt("Area3Unlock");
        set => SetField("Area3Unlock", value);
    }

    public int LianGongFang
    {
        get => GetInt("Area4Unlock");
        set => SetField("Area4Unlock", value);
    }

    public JSONObject LingTian
    {
        get => GetField("LingTian");
    }

    public JSONObject GetField(string name)
    {
        return JsonRaw.GetField(name);
    }

    public int GetInt(string name)
    {
        return JsonRaw.GetField(name).I;
    }

    public string GetStr(string name)
    {
        return JsonRaw.GetField(name).str;
    }

    public void SetField(string name, string value)
    {
        JsonRaw.SetField(name, value);
    }

    public void SetField(string name, int value)
    {
        JsonRaw.SetField(name, value);
    }

    public void SetZhongZhi(int slot, int id)
    {
        DongFuManager.ZhongZhi(ID, slot, id);
    }

    public void SetShouHuo(int slot)
    {
        DongFuManager.ShouHuo(ID, slot);
    }

    public int CuiShengLingLi
    {
        get => LingTian["CuiShengLingLi"].I;
        set => LingTian.SetField("CuiShengLingLi", value);
    }
}

public static class DongFuUtils
{
    public static JSONObject DongFuData => PlayerEx.Player.DongFuData;
    public static string DongFuDataStr => DongFuData.ToString(true);

    public static Dictionary<int, DongFuInfo> DongFuInfo =>
        DongFuData.Count != _dongFuInfo.Count ? GetDongFuDataInfos() : _dongFuInfo;

    private static readonly Dictionary<int, DongFuInfo> _dongFuInfo = new Dictionary<int, DongFuInfo>();

    public static DongFuInfo GetDongFuData(int dongFuID)
    {
        if (DongFuManager.PlayerHasDongFu(dongFuID))
        {
            return new DongFuInfo($"DongFu{dongFuID}");
        }

        
        return null;
    }

    public static void CreateDongFu(int dongFuID, int level, string dongFuName)
    {
        DongFuManager.CreateDongFu(dongFuID, level);
        DongFuManager.SetDongFuName(dongFuID, dongFuName);
    }

    public static Dictionary<int, DongFuInfo> GetDongFuDataInfos()
    {
        _dongFuInfo.Clear();
        foreach (var key in DongFuData.keys)
        {
            var dongFuInfo = new DongFuInfo(key);
            var id = dongFuInfo.ID;
            _dongFuInfo[id] = dongFuInfo;
        }

        return _dongFuInfo;
    }
}