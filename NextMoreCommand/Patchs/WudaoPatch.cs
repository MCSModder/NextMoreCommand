using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HarmonyLib;
using JSONClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(NPCFactory), nameof(NPCFactory.SetNpcWuDao))]
public static class NpcFactorySetNpcWudaoPatch
{
    private static JSONObject _wudaoJson;
    private static int _level;
    private static int _npcId;
    private static int _wudaoType;
    public static Dictionary<int, WuDaoAllTypeJson> WuDaoAllType => WuDaoAllTypeJson.DataDict;
    private static JSONObject _npcDate;

    public static List<int> CustomWuDaoTypeList =>
        WuDaoAllType.Keys.Where(index => index > 22).ToList();

    public static JSONObject NpcWuDaoJson => jsonData.instance.NPCWuDaoJson;

    private static int NpcId => _npcDate.HasField("BindingNpcID") && _npcDate["BindingNpcID"].I > 0
        ? _npcDate["BindingNpcID"].I
        : _npcDate["id"].I;

    public static void Postfix(int level, int wudaoType, JSONObject npcDate)
    {
        if (CustomWuDaoTypeList.Count == 0) return;
        _level = level;
        _wudaoType = wudaoType;
        _wudaoJson = npcDate.GetField("wuDaoJson");
        _npcDate = npcDate;
        _npcId = NpcId;

        foreach (var customWudao in CustomWuDaoTypeList.Where(customWudao => !_wudaoJson.HasField(customWudao.ToString())))
        {
            WuDaoUtils.SetWudao(npcDate, customWudao);
        }

        var dict = NPCWuDaoJson.DataDict.Where(item => item.Value.Type == _wudaoType && item.Value.lv == _level);
        foreach (var wuDao in dict)
        {
            SetWudao(wuDao);
        }

        Reset();
    }

    private static void Reset()
    {
        _level = 0;
        _wudaoType = 0;
        _wudaoJson = null;
        _npcDate = null;
        _npcId = 0;
    }

    public static void SetWudao(KeyValuePair<int, NPCWuDaoJson> wudao)
    {
        var npcWuDao = wudao.Value;
        var index = wudao.Key;
        var level = npcWuDao.lv;
        var wudaoType = npcWuDao.Type;
        var json = NpcWuDaoJson[index.ToString()];
        var customWuDaoInfos = new List<CustomWuDaoInfo>();
        foreach (var wudaoId in CustomWuDaoTypeList)
        {
            var key = $"value{wudaoId.ToString()}";
            if (!json.HasField(key)) continue;
            var value = json[key].I;
            customWuDaoInfos.Add(new CustomWuDaoInfo()
            {
                Id = wudaoId,
                Value = value
            });
        }

        if (customWuDaoInfos.Count == 0)
        {
            return;
        }

        if (level != _level || wudaoType != _wudaoType) return;
        foreach (var customWuDao in customWuDaoInfos)
        {
            var wudaoJson = new WudaoJsonInfo()
            {
                WudaoId = customWuDao.Id,
                Level = customWuDao.Value
            };
            var id = customWuDao.Id;
            MyLog.Log("设置NPC结算自定义悟道", $"NPCFactory.SetNpcWuDao");
            MyLog.Log("设置NPC自定义悟道", $"角色ID:{_npcId.ToString()} 角色名字:{_npcDate.GetField("Name").str}");
            MyLog.Log("设置NPC自定义悟道", $"悟道ID:{id.ToString()} 悟道名字:{WuDaoAllType[id].name1} 悟道境界:{customWuDao.Value.ToString()}");
            wudaoJson.SetExpByLevel();
            _wudaoJson.SetField(wudaoJson.Id, wudaoJson.ToJsonObject());
        }
    }
}

[HarmonyPatch(typeof(NpcJieSuanManager), nameof(NpcJieSuanManager.UpdateNpcWuDao))]
public static class NpcJieSuanManagerUpdateNpcWuDaoPatch
{
    private static JSONObject _wudaoJson;
    private static int _npcId;
    private static int _level;
    private static int _wudaoType;
    public static Dictionary<int, WuDaoAllTypeJson> WuDaoAllType => WuDaoAllTypeJson.DataDict;
    private static JSONObject _npcDate;

    public static List<int> CustomWuDaoTypeList => WuDaoAllType.Keys.Where(index => index > 22).ToList();

    public static JSONObject NpcWuDaoJson => jsonData.instance.NPCWuDaoJson;


    public static void Postfix(int npcId)
    {
        if (CustomWuDaoTypeList.Count == 0) return;
        _npcId = npcId;
        _npcDate = jsonData.instance.AvatarJsonData[npcId.ToString()];
        _wudaoJson = _npcDate.GetField("wuDaoJson");
        _level = _npcDate["Level"].I;
        _wudaoType = _npcDate["wudaoType"].I;


        foreach (var customWudao in CustomWuDaoTypeList)
        {
            if (_wudaoJson.HasField(customWudao.ToString()))
            {
                continue;
            }

            WuDaoUtils.SetWudao(_npcDate, customWudao);
        }

        var dict = NPCWuDaoJson.DataDict.Where(item => item.Value.Type == _wudaoType && item.Value.lv == _level);
        var keyValuePairs = dict.ToList();
        foreach (var wuDao in keyValuePairs)
        {
            SetWudao(wuDao);
        }

        Reset();
    }

    private static void Reset()
    {
        _level = 0;
        _wudaoType = 0;
        _wudaoJson = null;
        _npcDate = null;
        _npcId = 0;
    }

    public static void SetWudao(KeyValuePair<int, NPCWuDaoJson> wudao)
    {
        var npcWuDao = wudao.Value;
        var index = wudao.Key;
        var level = npcWuDao.lv;
        var wudaoType = npcWuDao.Type;
        var json = NpcWuDaoJson[index.ToString()];
        var customWuDaoInfos = (from wudaoId in CustomWuDaoTypeList
            let key = $"value{wudaoId.ToString()}"
            where json.HasField(key)
            let value = json[key].I
            select new CustomWuDaoInfo()
            {
                Id = wudaoId,
                Value = value
            }).ToList();

        //MyLog.Log("设置NPC结算自定义悟道", $"level:{level} wudaoType:{wudaoType} customWuDaoInfos:{customWuDaoInfos.Count}");
        if (customWuDaoInfos.Count == 0)
        {
            return;
        }


        foreach (var customWuDao in customWuDaoInfos)
        {
            var wudaoJson = new WudaoJsonInfo()
            {
                WudaoId = customWuDao.Id,
                Level = customWuDao.Value
            };
            var id = customWuDao.Id;
            MyLog.Log("设置NPC结算自定义悟道", $"触发NpcJieSuanManager.UpdateNpcWuDao");
            MyLog.Log("设置NPC结算自定义悟道", $"角色ID:{_npcId.ToString()} 角色名字:{_npcId.GetNpcName()}");
            MyLog.Log("设置NPC结算自定义悟道", $"悟道ID:{id.ToString()} 悟道名字:{WuDaoAllType[id].name1} 悟道境界:{customWuDao.Value.ToString()}");
            wudaoJson.SetExpByLevel();
            _wudaoJson.SetField(wudaoJson.Id, wudaoJson.ToJsonObject());
        }
    }
}