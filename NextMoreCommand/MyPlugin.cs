using System;
using BepInEx;
using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand
{
    [BepInPlugin("skyswordkill.plugin.NextMoreCommand", "NextMoreCommand", "1.0.0")]
    public class MyPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony("skyswordkill.plugin.NextMoreCommand").PatchAll();
            // 注册事件
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            DialogAnalysis.RegisterCommand("SetTempFlowchart",new SetTempFlowchart());
            DialogAnalysis.RegisterCommand("RunTempFlowchart",new RunTempFlowchart());
        }
    }
}