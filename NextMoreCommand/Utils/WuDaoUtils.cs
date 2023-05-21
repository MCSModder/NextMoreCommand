using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.Utils;

public class CustomWuDaoInfo
{
    public int Id;
    public int Value;
}

public class WudaoJsonInfo
{
    [JsonProperty("id")] public int WudaoId = 0;
    [JsonProperty("level")] public int Level = 0;
    [JsonProperty("exp")] public int Exp = 0;
    [JsonIgnore] public string Id => WudaoId.ToString();

    public void SetExpByLevel()
    {
        Exp = jsonData.instance.WuDaoJinJieJson[Level - 1]["Max"].I;
    }

    public JSONObject ToJsonObject()
    {
        return new JSONObject(JObject.FromObject(this).ToString());
    }
}

public static class WuDaoExtends
{
    private static NpcJieSuanManager NpcJieSuanManager => NpcJieSuanManager.inst;
    public static JSONObject GetNpcData(this int id) => NpcJieSuanManager.GetNpcData(id.ToNpcNewId());

    public static void AddNpcWuDaoExp(this int id, int wudaoId, int exp) =>
        NpcJieSuanManager.npcSetField.AddNpcWuDaoExp(id.ToNpcNewId(), wudaoId, exp);

    public static void AddNpcWuDaoZhi(this int id, int value) =>
        NpcJieSuanManager.npcSetField.AddNpcWuDaoZhi(id.ToNpcNewId(), value);

    public static void AddNpcWuDaoDian(this int id, int num) =>
        NpcJieSuanManager.npcSetField.AddNpcWuDaoDian(id.ToNpcNewId(), num);
}

public static class WuDaoUtils
{
    private static NpcJieSuanManager NpcJieSuanManager => NpcJieSuanManager.inst;

    public static void UpdateNpcWuDao(int npcId)
    {
        NpcJieSuanManager.UpdateNpcWuDao(npcId.ToNpcNewId());
    }

    public static void AddNpcWuDao(int npcId, int wudaoId, int level, int skill)
    {
        var wudao = wudaoId * 100 + level * 10 + skill;
        NpcJieSuanManager.AddNpcWuDao(npcId.ToNpcNewId(), wudao);
    }

    public static void AddNpcWuDao(int npcId, int wudaoIdSkill)
    {
        NpcJieSuanManager.AddNpcWuDao(npcId.ToNpcNewId(), wudaoIdSkill);
    }

    public static void SetWudaoExp(int npcId, int wudaoId, int exp = 0)
    {
        var npc = npcId.GetNpcData();
        var wudaoJson = npc["wuDaoJson"];
        var json = new WudaoJsonInfo()
        {
            WudaoId = wudaoId,
            Level = 0,
            Exp = 0,
        };
        wudaoJson.SetField(wudaoId.ToString(), json.ToJsonObject());
        npcId.AddNpcWuDaoExp(wudaoId, exp);
    }

    public static void SetWudao(JSONObject npcId, int wudaoId)
    {
        var wudaoJson = npcId["wuDaoJson"];
        var json = new WudaoJsonInfo()
        {
            WudaoId = wudaoId,
            Level = 0,
            Exp = 0,
        };
        wudaoJson.SetField(wudaoId.ToString(), json.ToJsonObject());
    }

    public static void AddWudaoExp(int npcId, int wudaoId, int exp = 0)
    {
        var npc = npcId.GetNpcData();
        var wudaoJson = npc["wuDaoJson"];
        var wudaoIdStr = wudaoId.ToString();
        if (wudaoJson.HasField(wudaoIdStr))
        {
            npcId.AddNpcWuDaoExp(wudaoId, exp);
        }
        else
        {
            var json = new WudaoJsonInfo()
            {
                Exp = 0,
                Level = 0,
                WudaoId = wudaoId
            };
            wudaoJson.SetField(wudaoIdStr, json.ToJsonObject());
            npcId.AddNpcWuDaoExp(wudaoId, exp);
        }
    }

    public static WudaoJsonInfo GetWudao(int npcId, int wudaoId)
    {
     
         var npc = npcId.GetNpcData();
        var wudaoJson = npc["wuDaoJson"];
        var wudaoIdStr = wudaoId.ToString();
        if (wudaoJson.HasField(wudaoIdStr))
        {
 
            return wudaoJson[wudaoIdStr].ToJObject().ToObject<WudaoJsonInfo>();
        }

        return null;
    }
}