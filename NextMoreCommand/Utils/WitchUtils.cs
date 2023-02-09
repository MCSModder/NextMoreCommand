using HarmonyLib;
using KBEngine;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class WitchUtils
{
    public static bool GetGlobalValue(this int id, string source)
    {
        return GlobalValue.Get(id, source) >= 1;
    }

    public static bool HasTianFu(this int id)
    {
        return Tools.instance.CheckHasTianFu(id);
    }

    public static bool HasInt(this string key)
    {
        return DialogAnalysis.GetInt(key) == 1;
    }

    public static int GetInt(this string key)
    {
        return DialogAnalysis.GetInt(key);
    }

    public static void SetLife(this bool key)
    {
        var player = PlayerEx.Player;
        var value = (player.shouYuan - player.age) <= 10 ? 1 : 0;
        "shouMing".SetInt(value);
    }

    public static void SetCheat(this bool key)
    {
        var result = "chuGui".GetInt() + 1;
        "chuGui".SetInt(result);
        if (result > 3)
        {
            7200.AllDeath();
        }
    }

    public static void AllDeath(this int id)
    {
        DaolvUtils.SetAllDaolvDeath(id.ToNpcNewId());
    }

    public static void AddWife(this int id)
    {
        DaolvUtils.DaolvId.Add(id.ToNpcNewId());
    }

    public static bool CheckCheat(this int key)
    {
        return true.CheckCheat() && "chuGui".GetInt() > key;
    }

    public static void SetCheat(this int key,int id)
    {
        if (key.CheckCheat())
        {
            id.AllDeath();
            id.SetWife();
        }
    }

    public static bool HasWife => 7200.IsWife();

    public static void SetWife(this int id)
    {
        if (!id.IsWife())
        {
            id.AddWife();
        }
    }

    public static bool CheckCheat(this bool _)
    {
        return 7200.HasHarem() || 7200.HasCheat();
    }

    public static int Count => DaolvUtils.DaolvId.Count;

    public static bool HasCheat(this int id)
    {
        return "daHun".HasInt() && !id.IsWife() && Count > 1;
    }

    public static void SetHarem(this int id)
    {
        if (id.HasHarem())
        {
            id.AllDeath();
        }
    }

    public static bool HasHarem(this int id)
    {
        return id.IsWife() && Count > 1;
    }

    public static void SetInt(this string key, int value)
    {
        DialogAnalysis.SetInt(key, value);
    }

    public static void ReduceScore(this int value)
    {
        BiaoBaiManager.BiaoBaiScore.TotalScore -= value;
    }

    public static void AddScore(this int value)
    {
        BiaoBaiManager.BiaoBaiScore.TotalScore += value;
    }

    public static bool IsWife(this int id)
    {
        return PlayerEx.IsDaoLv(id.ToNpcNewId()) && "shengSi".HasInt();
    }
}