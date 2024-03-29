﻿using System;
using System.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class PlayTimeUtils
{
    public static  float    Now => Time.realtimeSinceStartup;
    private static int      _playHourTime = 0;
    private static string[] ignoreScene   = new[] { "MainMenu", "Loading" };
    private static string[] _triggerType  = { "游玩每小时", "HasPlayHour" };
    public static bool HasPlayHour()
    {
        var hour = Convert.ToInt32(GetHours());
        MyPluginMain.LogInfo(hour);
        if (hour > _playHourTime && !ignoreScene.Contains(SceneManager.GetActiveScene().name))
        {
            MyPluginMain.LogInfo($"触发每小时游玩触发器");

            DialogAnalysis.TryTrigger(_triggerType, null, true);

            _playHourTime++;
            return true;
        }

        return false;
    }

    public static TimeSpan GetTimeSpan()
    {
        return TimeSpan.FromSeconds(Now);
    }

    public static double GetSeconds()
    {
        return GetTimeSpan().TotalSeconds;
    }

    public static double GetMinutes()
    {
        return GetTimeSpan().TotalMinutes;
    }

    public static double GetHours()
    {
        return GetTimeSpan().TotalHours;
    }

    public static double GetDays()
    {
        return GetTimeSpan().TotalDays;
    }
}