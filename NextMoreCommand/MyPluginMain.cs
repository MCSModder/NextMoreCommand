using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using BepInEx;
using HarmonyLib;
using JSONClass;
using MCSSubscribeDependencies;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.Mod;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProGif.GifManagers;
using ProGif.Lib;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.NPC;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;
using SkySwordKill.NextMoreCommand.CustomModDebug;
using SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore;
// using SkySwordKill.NextMoreCommand.Puerts;
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
        private static KeyCode DramaDebugKey;
        public static bool IsDebugMode = false;
        // public static long JSDebugPort = -1;
        // public static bool JSDebugMode = false;

        public static MyPluginMain Instance;
        public static MyPluginMain I => Instance;
        public static void LogInfo(object obj) => I.Logger.LogInfo(obj);
        public static void LogWarning(object obj) => I.Logger.LogWarning(obj);
        public static void LogError(object obj) => I.Logger.LogError(obj);
        public static void LogDebug(object obj) => I.Logger.LogDebug(obj);
        public static void LogFatal(object obj) => I.Logger.LogFatal(obj);
        public static void LogMessage(object obj) => I.Logger.LogMessage(obj);
        private Harmony _harmony;
        public List<string>  files;

        private void Awake()
        {
            Instance = this;

            // 注册事件
            RegisterCommand();
            RegisterCustomModSetting();
            _harmony = new Harmony("skyswordkill.plugin.NextMoreCommands");
            _harmony.PatchAll();
            InitDir();
          
            NextMoreCoreBinder.BindAll();
       
            ModManager.ModReload += () =>
            {
                CustomNpc.CustomNpcs.Clear();
                SkillComboManager.SkillCombos.Clear();
                SkillComboManager.CacheSkillCombos.Clear();
                // NextMoreCommand.InitJsEnvManager();
                
            };
            ModManager.ModSettingChanged += () =>
            {
                ModManagerUtils.TryGetModSetting("Quick_DramaDebugKey", out DramaDebugKey);
                ModManager.TryGetModSetting("Quick_IsDebugMode", out IsDebugMode);
                // ModManager.TryGetModSetting("Quick_JSDebugPort", out JSDebugPort);
                // ModManager.TryGetModSetting("Quick_JSDebugMode", out JSDebugMode);
            };
            ModManager.ModLoadComplete += () =>
            {
                WorkshopUtils.WorkShopItems = WorkshopUtils.GetAllModWorkshopItem();
                ModManagerUtils.TryGetModSetting("Quick_DramaDebugKey", out DramaDebugKey);
                ModManager.TryGetModSetting("Quick_IsDebugMode", out IsDebugMode);
                // ModManager.TryGetModSetting("Quick_JSDebugPort", out JSDebugPort);
                // ModManager.TryGetModSetting("Quick_JSDebugMode", out JSDebugMode);
                AssetsUtils.Clear();
            };
        }


        private void Update()
        {
            if (!Input.GetKeyDown(DramaDebugKey)) return;
            if (ModDialogDebugWindow.Instance == null)
            {
                new ModDialogDebugWindow().Show();
            }
            else
            {
                ModDialogDebugWindow.Instance.Hide();
            }
        }

        private void RegisterCustomModSetting()
        {
            var registerCommandType = typeof(RegisterCustomModSettingAttribute);
            var types = Assembly.GetAssembly(registerCommandType).GetTypes();
            var init = "".PadLeft(25, '=');
            LogInfo(init);
            LogInfo($"注册自定义Mod设置开始.");
            LogInfo(init);
            foreach (var type in types)
            {
                if (type.GetCustomAttributes(registerCommandType, true).Length > 0)
                {
                    var cEvent = Activator.CreateInstance(type) as ICustomSetting;
                    LogInfo($"注册自定义Mod设置: {type.Name}");
                    LogInfo($"注册自定义Mod设置: {cEvent}");
                    LogInfo(init);
                    ModManager.RegisterCustomSetting(type.Name, cEvent);
                }
            }

            LogInfo($"注册自定义Mod设置完毕.");
            LogInfo(init);
        }


        private void RegisterCommand()
        {
            var registerCommandType = typeof(RegisterCommandAttribute);
            var types = Assembly.GetAssembly(registerCommandType).GetTypes();
            var init = "".PadLeft(25, '=');
            LogInfo(init);
            LogInfo($"注册指令开始.");
            LogInfo(init);
            foreach (var type in types)
            {
                if (type.GetCustomAttributes(registerCommandType, true).Length <= 0) continue;
                var cEvent = Activator.CreateInstance(type) as IDialogEvent;
                LogInfo($"注册指令名: {type.Name}");
                LogInfo($"注册指令类: {cEvent}");
                LogInfo(init);
                DialogAnalysis.RegisterCommand(type.Name, cEvent);
            }

            LogInfo($"注册指令完毕.");
            LogInfo(init);
        }

        private void InitDir()
        {
            var dllPath = Directory.GetParent(typeof(MyPluginMain).Assembly.Location)?.FullName;

            PathLocalModsDir =
                new Lazy<string>(() => BepInEx.Paths.GameRootPath + @"\本地Mod测试");
            PathLibDir =
                new Lazy<string>(() => Utility.CombinePaths(
                    dllPath,
                    @"Lib"));
            // var dir = PathLibDir.Value;
            // if (string.IsNullOrWhiteSpace(dir)) return;
            // files = Directory.GetFiles(dir).Select(Path.GetFileName).ToList();
            // files.ForEach(filePathName =>
            // {
            //     LogInfo($"开始加载:{filePathName}");
            //     DllTools.LoadDllFile(dir, filePathName);
            // });
            //


        }

        public Lazy<string> PathLibDir { get; set; }

        public Lazy<string> PathLocalModsDir { get; set; }

        private void OnDestroy()
        {
            _harmony.UnpatchSelf();
        }
    }
}