// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using BepInEx;
// using Puerts;
// using SkySwordKill.Next;
// using SkySwordKill.NextMoreCommand.Patchs;
//
// namespace SkySwordKill.NextMoreCommand.Puerts
// {
//     public class NextLoader : ILoader, IModuleChecker
//     {
//
//         public bool FileExists(string filePath)
//         {
//             if (!ModManagerLoadModData.JsExt.Contains(Path.GetExtension(filePath)) && !filePath.EndsWith(".js"))
//             {
//                 filePath += ".js";
//             }
//             // MyPluginMain.LogInfo($"判断文件路径:{filePath}");
//             if (mockFileContent.ContainsKey(filePath))
//             {
//                 return true;
//             }
//             if (JsEnvManager.JsFileCaches.ContainsKey(filePath))
//             {
//                 return true;
//             }
//
//             var lib = MyPluginMain.I.PathLibDir.Value;
//             return File.Exists(Utility.CombinePaths(lib, filePath)) || File.Exists(Utility.CombinePaths(lib, "puerts", filePath));
//         }
//  
//         public string ReadFile(string filePath, out string debugPath)
//         {
//             if (!ModManagerLoadModData.JsExt.Contains(Path.GetExtension(filePath)) && !filePath.EndsWith(".js"))
//             {
//                 filePath += ".js";
//             }
//             // MyPluginMain.LogInfo($"读取文件路径:{filePath}");
//             string filepath;
//             if (JsEnvManager.JsFileCaches.TryGetValue(filePath, out var jsFileCache))
//             {
//                 filepath = jsFileCache.FilePath;
//                 debugPath = filePath;
//                 return File.ReadAllText(filepath);
//             }
//             if (mockFileContent.TryGetValue(filePath, out var file))
//             {
//                 debugPath = filePath;
//                 return file;
//             }
//             var lib = MyPluginMain.I.PathLibDir.Value;
//             filepath = Utility.CombinePaths(lib, "puerts", filePath);
//             if (File.Exists(filepath))
//             {
//                 debugPath = filepath;
//                 return File.ReadAllText(filepath);
//             }
//             filepath = Utility.CombinePaths(lib, filePath);
//             if (File.Exists(filepath))
//             {
//                 debugPath = filepath;
//                 return File.ReadAllText(filepath);
//             }
//             debugPath = string.Empty;
//             return debugPath;
//         }
//         public bool IsESM(string filepath)
//         {
//             //MyPluginMain.LogInfo(filepath);
//             return filepath.Length >= 4 && filepath.EndsWith(".mjs");
//         }
//         public readonly Dictionary<string, string> mockFileContent = new Dictionary<string, string>();
//         public void AddMockFileContent(string filePath, string context)
//         {
//             if (!ModManagerLoadModData.JsExt.Contains(Path.GetExtension(filePath)) && !filePath.EndsWith(".js"))
//             {
//                 filePath += ".js";
//             }
//             mockFileContent[filePath] = context;
//         }
//     }
// }