using System;
using System.Collections.Generic;
using System.IO;
using Puerts;
using SkySwordKill.Next;
using SkySwordKill.Next.Mod;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Puerts
{
    public class JsFileCache
    {
        public ModConfig FromMod { get; set; }
        public string FilePath { get; set; }
        public JsFileCache()
        {
        }
        public JsFileCache(ModConfig fromMod, string filePath)
        {
            FromMod = fromMod;
            FilePath = filePath;
        }
    }

    public class JsEnvManager : MonoBehaviour
    {

        public JsEnv JsEnv;
        public void RunJavaScript(string scr, string funcName, object[] args)
        {

            JsEnv.ExecuteModule<Func<string>>("", "default");
            //  JsEnv.ExecuteModule<Func<>>("", "default");
        }
        private void Awake()
        {
            var files = MyPluginMain.I.files;
            if (files == null)
            {
                Destroy(this);
            }


        }

        private void InitJavaScriptEnv()
        {

            if (JsEnv != null)
            {
                JsEnv.ClearModuleCache();
                JsEnv.Dispose();
            }
            JsEnv = new JsEnv(new NextLoader());
//             JsEnv.Eval(@"
// console.log('初始化成功')
// try {
//   aaaa.aaa;
// } catch (error) {
//   console.error(error);
//   console.trace();
// }
// ");
//             JsEnv.UsingAction<string>();
//             var action1 = JsEnv.ExecuteModule<Action<string>>("dialog.mjs", "default");
//             action1?.Invoke(" JsEnv.ExecuteModule<Action<string>>(\"dialog.mjs\",\"default\")");

        }
        public void ClearCache() => JsEnv?.ClearModuleCache();
        private void Start()
        {

            InitJavaScriptEnv();
        }
        private void Update()
        {
            JsEnv?.Tick();
        }
        public void Reset()
        {
            InitJavaScriptEnv();
            JsFileCaches.Clear();
        }
        public static readonly Dictionary<string, JsFileCache> JsFileCaches = new Dictionary<string, JsFileCache>();
        public static void AddJsFileCache(string filePath, JsFileCache jsFileCache)
        {

            if (JsFileCaches.ContainsKey(filePath))
            {
                var old = JsFileCaches[filePath];
                MyPluginMain.LogWarning("JavaScript\"" + filePath + "\"发生覆盖 [" + old.FromMod.Name + "]" + old.FilePath + " --> [" + jsFileCache.FromMod.Name + "]" + jsFileCache.FilePath);
            }
            else
            {
                var ext = Path.GetExtension(jsFileCache.FilePath);
                MyPluginMain.LogInfo(("添加JavaScript指向：" + filePath + ext));
            }
            JsFileCaches[filePath] = jsFileCache;
        }
        public static string GetJavaScriptModPath(string filePath)
        {
            return JsFileCaches.TryGetValue(filePath, out var jsFileCache) ? jsFileCache.FromMod.Path : (string)null;
        }
        private void OnDestroy()
        {
            JsEnv?.Dispose();
        }
    }
}