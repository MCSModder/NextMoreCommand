using System.Collections.Generic;
using System.Linq;
using KBEngine;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils;

/// <summary>
/// 道侣工具类
/// </summary>
public static class DaolvUtils
{
    public static Avatar Player => PlayerEx.Player;
    public static JSONObject DaolvId => Player.DaoLvId;
    public static NpcJieSuanManager NpcJieSuanManager => NpcJieSuanManager.inst;

    /// <summary>
    /// 获得角色名字
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>角色名字</returns>
    public static string GetName(int id) => DialogAnalysis.GetNpcName(id.ToNpcNewId());

    /// <summary>
    /// 设置所有道侣死亡
    /// </summary>
    /// <param name="filterNpc">过滤角色ID列表</param>
    /// <returns>获得道侣死亡ID列表</returns>
    public static List<int> SetAllDaolvDeath(int filterNpc) => SetAllDaolvDeath(new[] { filterNpc });

    /// <summary>
    /// 设置所有道侣死亡
    /// </summary>
    /// <param name="filterNpc">过滤角色ID列表</param>
    /// <returns>获得道侣死亡ID列表</returns>
    public static List<int> SetAllDaolvDeath(params int[] filterNpc)
    {
        var list = new List<int>();
        var daolvList = DaolvId.ToList();
        PlayerEx.Player.DaoLvId = JSONObject.Create(JSONObject.Type.ARRAY);
        var filter = filterNpc.Select(NPCEx.NPCIDToNew).ToList();
        foreach (var id in daolvList)
        {
            if (filter.Contains(id))
            {
                DaolvId.Add(id);
                continue;
            }

            MyPluginMain.LogInfo($"道侣死亡 ID:{id} 名字:{GetName(id)}");
            UIPopTip.Inst.Pop($"道侣死亡 ID:{id} 名字:{GetName(id)}");
            NpcJieSuanManager.npcDeath.SetNpcDeath((int)NpcDeathType.被其它Npc截杀, id);
            list.Add(id);
        }

        return list;
    }
}