using System;
using System.Linq;
using System.Text;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Steamworks;
using UnityEngine;

namespace MCSSubscribeDependencies;

[BepInPlugin("SubscribeDependencies", "SubscribeDependencies", "1.0.0")]
public class SubscribeDependencies : BaseUnityPlugin
{
    private Harmony _harmony;
    public static ManualLogSource m_manualLogSource;
    private void Awake()
    {
        _harmony = new Harmony("SubscribeDependencies");
        _harmony.PatchAll();
        m_manualLogSource = Logger;
        
    }
}
[HarmonyPatch(typeof(MainUIMag),(nameof(MainUIMag.OpenMain)))]
public static class MainUIMagOpenMainPatch
{
    public static void Postfix(MainUIMag __instance)
    {
        if (SteamAPI.IsSteamRunning())
        {
            WorkshopUtils.Subscribe();
        }
    }
 
}