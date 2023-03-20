using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Puerts;
using SkySwordKill.Next;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Patchs;
using SkySwordKill.NextMoreCommand.Utils;
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
            GetJavaScript<object>(scr, funcName, args);
        }
        public int GetInt(string scr, string funcName, object[] args) => GetJavaScript<int>(scr, funcName, args);
        public string GetStr(string scr, string funcName, object[] args) => GetJavaScript<string>(scr, funcName, args);
        public bool GetBoolean(string scr, string funcName, object[] args) => GetJavaScript<bool>(scr, funcName, args);
        public object GetJavaScript(string scr, string funcName, Type type, object[] args) => GetType().GetMethod("GetJavaScript")
            ?.MakeGenericMethod(type).Invoke(this, new object[]
            {
                scr, funcName, args
            });
        public T GetJavaScript<T>(string scr, string funcName, object[] args)
        {
            _loader?.AddMockFileContent("JsEnvManager/RunJavaScript.mjs", $"import * as all from '{scr}';export default function(funcName,args){{var func = all[funcName];return func ? func(args,all) : func}}");
            JsEnv.UsingFunc<Func<string, object[], T>>();
            var result = JsEnv?.ExecuteModule<Func<string, List<object>, T>>("JsEnvManager/RunJavaScript.mjs", "default");
            return result != null ? result.Invoke(funcName, args.ToList()) : default(T);
        }
        public List<object> GetJavaScript(string scr, string funcName, object[] args) => GetJavaScript<List<object>>(scr, funcName, args);
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

            _loader = new NextLoader();
            JsEnv = new JsEnv(_loader);

            // RunJavaScript("dialog.mjs", "default", new object[]
            // {
            //     "初始化", 1, this, new DialogEnvironmentContext(),
            // });
            // _result = GetJavaScript<object>("dialog.mjs", "default", new object[]
            // {
            //     "初始化", 1, this, new DialogEnvironmentContext(),
            // });

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
            if (!ModManagerLoadModData.JsExt.Contains(Path.GetExtension(filePath)) && !filePath.EndsWith(".js"))
            {
                filePath += ".js";
            }
            return JsFileCaches.TryGetValue(filePath, out var jsFileCache) ? jsFileCache.FromMod.Path : (string)null;
        }
        private void OnDestroy()
        {
            JsEnv?.ClearModuleCache();
            JsEnv?.Dispose();
        }
    }
}