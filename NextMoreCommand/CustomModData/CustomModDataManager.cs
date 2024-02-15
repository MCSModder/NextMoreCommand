using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.NextSeachNpcExtension;

namespace SkySwordKill.NextMoreCommand.CustomModData
{
    public static class CustomModDataManager
    {
        public static bool   HasPath(this     string path)                => Directory.Exists(path);
        public static string CombinePath(this string path, string folder) => Path.Combine(path, folder);

        public static bool HasDirectory(params string[] paths)
        {
            var result = false;
            foreach (var path in paths)
            {
                if (path.HasPath())
                {
                    result = true;
                }
            }
            return result;
        }
        public static readonly Dictionary<string, IModData> ModDataDict = new Dictionary<string, IModData>();
        public static void Init()
        {
            foreach (var types in AppDomain.CurrentDomain.GetAssemblies()
                         .Select(assembly => assembly.GetTypes()))
            {
                foreach (var type in types)
                {
                    if (!typeof(IModData).IsAssignableFrom(type)) continue;
                    foreach (var attribute in type.GetCustomAttributes<ModDataAttribute>())
                    {
                        var key = attribute.Name;
                        RegisterModData(key, Activator.CreateInstance(type) as IModData);
                    }
                }
            }
        }
        public static void LoadData(string modDir, string folder, ModConfig modConfig, Action<string, ModConfig> onComplete)
        {
            var path = modDir.CombinePath(folder);
            if (path.HasPath())
            {
                onComplete.Invoke(path, modConfig);
            }
        }
        private static void RegisterModData(string key, IModData modData)
        {
            ModDataDict[key] = modData;
        }
        public static void Read(ModConfig modConfig)
        {
            MyPluginMain.LogInfo($"=================== NextMore开始生成 =====================");
            foreach (var data in ModDataDict.Select(modData => modData.Value).Where(data => data.Check(modConfig)))
            {
                try
                {
                    data.Read(modConfig);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            MyPluginMain.LogInfo($"=================== NextMore结束加载 =====================");

        }
    }
}