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




        private void Awake()
        {
            
            // 注册事件
            RegisterCommand();
            RegisterCustomModSetting();
            new Harmony("skyswordkill.plugin.NextMoreCommands").PatchAll();
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