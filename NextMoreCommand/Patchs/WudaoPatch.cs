using HarmonyLib;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Patchs;
[HarmonyPatch(typeof(NPCFactory),nameof(NPCFactory.SetNpcWuDao))]
public static class WudaoPatch
{
    public static void Postfix(int level, int wudaoType, JSONObject npcDate)
    {
        MyLog.Log($"level:{level} wudaoType:{wudaoType}");
        var wudaoJson = npcDate.GetField("wuDaoJson");
        
        for (int i = 0; i < jsonData.instance.NPCWuDaoJson.Count; i++)
        {
            var npcWuDaoJson = jsonData.instance.NPCWuDaoJson[i];
            var type = npcWuDaoJson["Type"].I;
            if (npcWuDaoJson["lv"].I == level && type > 22  &&  type == wudaoType)
            {
         
            }
        }
    }
}