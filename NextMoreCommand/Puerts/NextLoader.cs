using System.IO;
using BepInEx;
using Puerts;
using SkySwordKill.Next;

namespace SkySwordKill.NextMoreCommand.Puerts
{
    public class NextLoader : ILoader, IModuleChecker
    {

        public bool FileExists(string filepath)
        {
           // MyPluginMain.LogInfo($"判断文件路径:{filepath}");
            if (filepath.StartsWith("puerts/") && Main.Res.fileAssets.ContainsKey($"Assets/Resources/{filepath}"))
            {
                return true;
            }
            if (JsEnvManager.JsFileCaches.ContainsKey(filepath))
            {
                return true;
            }
            var lib = MyPluginMain.I.PathLibDir.Value;
            return File.Exists(Utility.CombinePaths(lib,"puerts", filepath));
        }
        private string PathToUse(string filepath)
        {
            return
                filepath.EndsWith(".cjs") || filepath.EndsWith(".mjs") ? filepath.Substring(0, filepath.Length - 4) : filepath;
        }
        public string ReadFile(string filepath, out string debugpath)
        {
          //  MyPluginMain.LogInfo($"读取文件路径:{filepath}");
            string filePath;
            if (filepath.StartsWith("puerts/") && Main.Res.TryGetFileAsset($"Assets/Resources/{filepath}", out var fileAsset))
            {
                filePath = fileAsset.FileRawPath;
                debugpath = Directory.GetDirectoryRoot(filePath);
                return File.ReadAllText(filePath);
            }
            if (JsEnvManager.JsFileCaches.TryGetValue(filepath, out var jsFileCache))
            {
                filePath = jsFileCache.FilePath;
                debugpath = filePath;
                return File.ReadAllText(filepath);
            }
            var lib = MyPluginMain.I.PathLibDir.Value;
            filePath = Utility.CombinePaths(lib,"puerts", filepath);
            if (File.Exists(filePath))
            {
                debugpath = filepath;
                return File.ReadAllText(filePath);
            }
            debugpath = string.Empty;
            return debugpath;
        }
        public bool IsESM(string filepath)
        {
            return filepath.Length >= 4 && !filepath.EndsWith(".cjs");
        }
    }
}