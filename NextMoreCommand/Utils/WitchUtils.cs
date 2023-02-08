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

    public static void Set(this string key)
    {
        switch (key)
        {
            case "shouMing":
                var player = PlayerEx.Player;
                var value = (player.shouYuan - player.age) <= 10 ? 1 : 0;
                key.SetInt(value);
                break;
            case "chuGui":
                var result = key.GetInt() + 1;
                key.SetInt(result);
                if (result > 3)
                {
                    7200.AllDeath();
                }

                break;
        }
    }

    public static void AllDeath(this int id)
    {
        DaolvUtils.SetAllDaolvDeath(id.ToNpcNewId());
    }

    public static void AddCheat(this int id)
    {
        DaolvUtils.DaolvId.Add(id.ToNpcNewId());
    }

    public static bool Check(this string key)
    {
        return true.CheckCheat() && key.GetInt() > 3;
    }

    public static bool CheckCheat(this bool _)
    {
        var count = DaolvUtils.DaolvId.Count;
        var isDaoLv = 7200.IsDaoLv();
        var isHarem = isDaoLv && count > 1;
        var isChuGui = !isDaoLv && count >= 1;
        return isHarem || ("daHun".HasInt() && isChuGui);
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

    public static bool IsDaoLv(this int id)
    {
        return PlayerEx.IsDaoLv(id.ToNpcNewId()) && "shengSi".HasInt();
    }
}