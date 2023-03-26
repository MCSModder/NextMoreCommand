using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Puerts;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Patchs;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using WXB;
using Object = UnityEngine.Object;

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
        public static JsEnvManager Inst;

        public JsEnv JsEnv;
        public object RunJavaScript(string src, string funcName, object[] args)
        {
            return GetJavaScript<object>(src, funcName, args);
        }

        public T GetJavaScript<T>(string src, string funcName, object[] args)
        {
            JsEnv.UsingFunc<Func<string, string, object[], T>>();
            var func = JsEnv?.ExecuteModule<Func<string, string, object[], T>>("runJs.mjs", "default");
            if (func == null) return default(T);
            var result = func.Invoke(src, funcName, args);
            return result is JSObject ? default(T) : result;
        }
        private void Awake()
        {
            var files = MyPluginMain.I.files;
            if (files == null)
            {
                Destroy(this);
            }
            if (Inst != null)
            {
                Destroy(this);
                return;
            }
            Inst = this;

        }
        public void InjectSupportForCjs()
        {
            JsEnv?.ExecuteModule("load.mjs");
            JsEnv?.ExecuteModule("modular.mjs");
        }
        private void InitJavaScriptEnv()
        {

            _loader = new NextLoader();
            JsEnv = new JsEnv(_loader);
            InjectSupportForCjs();
           // Test1();
        }
        public object Test1()
        {

            return RunJavaScript("dialog.js", "dialog", new object[]
            {
                new DialogEnvironment()
            });
        }
        public static object Test()
        {
            return Inst.Test1();
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
            JsFileCaches.Clear();
            // GC.Collect();
            DestroyImmediate(this);
        }
        public static readonly Dictionary<string, JsFileCache> JsFileCaches = new Dictionary<string, JsFileCache>();
        private NextLoader _loader;
        public static void AddJsFileCache(string filePath, JsFileCache jsFileCache)
        {
            if (!ModManagerLoadModData.JsExt.Contains(Path.GetExtension(filePath)) && !filePath.EndsWith(".js"))
            {
                filePath += ".js";
            }
            if (JsFileCaches.TryGetValue(filePath, out var old))
            {
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
            if (!ModManagerLoadModData.JsExt.Contains(Path.GetExtension(filePath)) && !filePath.EndsWith(".js"))
            {
                filePath += ".js";
            }
            return JsFileCaches.TryGetValue(filePath, out var jsFileCache) ? jsFileCache.FromMod.Path : (string)null;
        }
        private void OnDestroy()
        {
            Inst = null;
            JsEnv?.ClearModuleCache();
            JsEnv?.Dispose();
        }
    }
}