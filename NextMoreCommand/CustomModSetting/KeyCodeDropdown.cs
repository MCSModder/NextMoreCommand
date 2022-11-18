using System;
using System.Collections.Generic;
using System.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.FGUI.Component;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Attribute;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.CustomModSetting
{
    [RegisterCustomModSetting]
    public class KeyCodeDropdown : ICustomSetting
    {
        public string[] Options => Enum.GetNames(typeof(KeyCode));
        public string DefaultValue { get; set; } = "None";

        public void OnInit(ModSettingDefinition_Custom customSetting)
        {
            var defaultValue = customSetting.GetConfig<string>("DefaultValue");
            DefaultValue = defaultValue == string.Empty ? "None" : defaultValue;
            customSetting.InitString(customSetting.Key, DefaultValue);
            var hasKey = ModManager.TryGetModSetting(customSetting.Key, out string key);
            var hasKeyCode = Enum.TryParse(key, true, out KeyCode keyCode);
            var value = Array.IndexOf(Options, Enum.GetName(typeof(KeyCode), (object)keyCode));
            if (hasKeyCode && (value < 0 || value >= Options.Length))
            {
                ModManager.SetModSetting(customSetting.Key, DefaultValue);
            }
        }

        public void OnDrawer(ModSettingDefinition_Custom customSetting, IInspector inspector)
        {
            var drawer = new CtlDropdownPropertyDrawer(
                customSetting.Name,
                () => Options,
                index => ModManager.SetModSetting(customSetting.Key, Options[index]),
                () =>
                {
                    ModManager.TryGetModSetting(customSetting.Key, out string key);
                    if (!Enum.TryParse(key, true, out KeyCode keyCode))
                    {
                        keyCode = KeyCode.None;
                    }


                    return Array.IndexOf(Options, Enum.GetName(typeof(KeyCode), (object)keyCode));
                });
            inspector.AddDrawer(drawer);
        }
    }
}