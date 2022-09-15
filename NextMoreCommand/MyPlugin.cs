using System;
using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using MarkerMetro.Unity.WinLegacy.Reflection;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand
{
    [BepInPlugin("skyswordkill.plugin.NextMoreCommand", "NextMoreCommand", "1.0.1")]
    public class MyPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony("skyswordkill.plugin.NextMoreCommand").PatchAll();
            // 注册事件
            RegisterCommand();
        }

        private static void RegisterCommand()
        {
            var registerCommandType = typeof(RegisterCommandAttribute);
            var types = Assembly.GetAssembly(registerCommandType).GetTypes();
            var init = "".PadLeft(25, '=');
            Main.LogInfo(init);
            Main.LogInfo($"注册指令开始.");
            Main.LogInfo(init);
            foreach (var type in types)
            {
                if (type.GetCustomAttributes(registerCommandType, true).Length > 0)
                {
                    var cEvent = Activator.CreateInstance(type) as IDialogEvent;
                    Main.LogInfo($"注册指令名: {type.Name}");
                    Main.LogInfo($"注册指令类: {cEvent}");
                    Main.LogInfo(init);
                    DialogAnalysis.RegisterCommand(type.Name, cEvent);
                }
            }

            Main.LogInfo($"注册指令完毕.");
            Main.LogInfo(init);
        }
    }
}