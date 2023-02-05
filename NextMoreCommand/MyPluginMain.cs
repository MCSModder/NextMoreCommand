using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using JSONClass;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.Mod;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.NPC;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.CustomModDebug;
using SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore;
using SkySwordKill.NextMoreCommand.Utils;
using Steamworks;
using YSGame.TuJian;
using Input = UnityEngine.Input;
namespace SkySwordKill.NextMoreCommand
{
    [BepInDependency("skyswordkill.plugin.Next", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin("skyswordkill.plugin.NextMoreCommands", "NextMoreCommands", "1.0.3")]
    public class MyPluginMain : BaseUnityPlugin
    {
        private KeyCode DramaDebugKey;

        public static MyPluginMain Instance;
        public static MyPluginMain I => Instance;
        public static void LogInfo(object obj) => I.Logger.LogInfo(obj);
        public static void LogError(object obj) => I.Logger.LogError(obj);
        private Harmony _harmony;

        private void Awake()
        {
            Instance = this;
   
            // 注册事件
            RegisterCommand();
            RegisterCustomModSetting();
            _harmony = new Harmony("skyswordkill.plugin.NextMoreCommands");
            _harmony.PatchAll();
            NextMoreCoreBinder.BindAll();
            ModManager.ModReload += () =>
            {
                CustomNpc.CustomNpcs.Clear();
                SkillComboManager.SkillCombos.Clear();
                SkillComboManager.CacheSkillCombos.Clear();
            };
            ModManager.ModSettingChanged += () =>
            {
                ModManagerUtils.TryGetModSetting("Quick_DramaDebugKey", out DramaDebugKey);
            };
            ModManager.ModLoadComplete += () =>
            {
                ModManagerUtils.TryGetModSetting("Quick_DramaDebugKey", out DramaDebugKey);
            };
        }


        private void Update()
        {
            if (Input.GetKeyDown(DramaDebugKey))
            {
                if (ModDialogDebugWindow.Instance == null)
                {
                    new ModDialogDebugWindow().Show();
                }
                else
                {
                    ModDialogDebugWindow.Instance.Hide();
                }
            }
        }

        private void RegisterCustomModSetting()
        {
            var registerCommandType = typeof(RegisterCustomModSettingAttribute);
            var types = Assembly.GetAssembly(registerCommandType).GetTypes();
            var init = "".PadLeft(25, '=');
            MyPluginMain.LogInfo(init);
            MyPluginMain.LogInfo($"注册自定义Mod设置开始.");
            MyPluginMain.LogInfo(init);
            foreach (var type in types)
            {
                if (type.GetCustomAttributes(registerCommandType, true).Length > 0)
                {
                    var cEvent = Activator.CreateInstance(type) as ICustomSetting;
                    MyPluginMain.LogInfo($"注册自定义Mod设置: {type.Name}");
                    MyPluginMain.LogInfo($"注册自定义Mod设置: {cEvent}");
                    MyPluginMain.LogInfo(init);
                    ModManager.RegisterCustomSetting(type.Name, cEvent);
                }
            }

            MyPluginMain.LogInfo($"注册自定义Mod设置完毕.");
            MyPluginMain.LogInfo(init);
        }


        private void RegisterCommand()
        {
            var registerCommandType = typeof(RegisterCommandAttribute);
            var types = Assembly.GetAssembly(registerCommandType).GetTypes();
            var init = "".PadLeft(25, '=');
            MyPluginMain.LogInfo(init);
            MyPluginMain.LogInfo($"注册指令开始.");
            MyPluginMain.LogInfo(init);
            foreach (var type in types)
            {
                if (type.GetCustomAttributes(registerCommandType, true).Length > 0)
                {
                    var cEvent = Activator.CreateInstance(type) as IDialogEvent;
                    MyPluginMain.LogInfo($"注册指令名: {type.Name}");
                    MyPluginMain.LogInfo($"注册指令类: {cEvent}");
                    MyPluginMain.LogInfo(init);
                    DialogAnalysis.RegisterCommand(type.Name, cEvent);
                }
            }

            MyPluginMain.LogInfo($"注册指令完毕.");
            MyPluginMain.LogInfo(init);
        }

        private void OnDestroy()
        {
            _harmony.UnpatchSelf();
        }
    }
}