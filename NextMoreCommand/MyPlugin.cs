using System;
using System.Linq;
using System.Reflection;
using BepInEx;
using FairyGUI;
using HarmonyLib;
using JSONClass;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom;
using SkySwordKill.NextMoreCommand.CustomModDebug;
using SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using YSGame.TuJian;
using Input = UnityEngine.Input;

namespace SkySwordKill.NextMoreCommand
{
    [BepInDependency("skyswordkill.plugin.Next", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin("skyswordkill.plugin.NextMoreCommand", "NextMoreCommand", "1.0.2")]
    public class MyPlugin : BaseUnityPlugin
    {
        private KeyCode DramaDebugKey;
        private KeyCode AchivementDebugKey;

        private void Awake()
        {
            // 注册事件
            RegisterCommand();
            RegisterCustomModSetting();
            new Harmony("skyswordkill.plugin.NextMoreCommand").PatchAll();
            NextMoreCoreBinder.BindAll();
            ModManager.ModSettingChanged += () =>
            {
                ModManagerUtils.TryGetModSetting("Quick_DramaDebugKey", out DramaDebugKey);
                ModManagerUtils.TryGetModSetting("Quick_AchivementDebugKey", out AchivementDebugKey);
            };
            ModManager.ModLoadComplete += () =>
            {
                ModManagerUtils.TryGetModSetting("Quick_DramaDebugKey", out DramaDebugKey);
                ModManagerUtils.TryGetModSetting("Quick_AchivementDebugKey", out AchivementDebugKey);
            };
        }

        private void Start()
        {
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

            if (Input.GetKeyDown(AchivementDebugKey))
            {
                var list = CreateAvatarJsonData.DataList.Where(json => !string.IsNullOrWhiteSpace(json.UnlockKey));
                foreach (var json in list)
                {
                    if (!TuJianManager.Inst.IsUnlockedSpecialEvent(json.UnlockKey))
                    {
                        Logger.LogInfo(json.UnlockKey);
                        TuJianManager.Inst.UnlockSpecialEvent(json.UnlockKey);
                    }
                }
            }
        }

        private void RegisterCustomModSetting()
        {
            var registerCommandType = typeof(RegisterCustomModSettingAttribute);
            var types = Assembly.GetAssembly(registerCommandType).GetTypes();
            var init = "".PadLeft(25, '=');
            Main.LogInfo(init);
            Main.LogInfo($"注册自定义Mod设置开始.");
            Main.LogInfo(init);
            foreach (var type in types)
            {
                if (type.GetCustomAttributes(registerCommandType, true).Length > 0)
                {
                    var cEvent = Activator.CreateInstance(type) as ICustomSetting;
                    Main.LogInfo($"注册自定义Mod设置: {type.Name}");
                    Main.LogInfo($"注册自定义Mod设置: {cEvent}");
                    Main.LogInfo(init);
                    ModManager.RegisterCustomSetting(type.Name, cEvent);
                }
            }

            Main.LogInfo($"注册自定义Mod设置完毕.");
            Main.LogInfo(init);
        }

      

        private void RegisterCommand()
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