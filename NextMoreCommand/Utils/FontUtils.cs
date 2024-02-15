// using System;
// using System.Collections.Generic;
// using System.IO;
// using FairyGUI;
// using SkySwordKill.Next;
// using SkySwordKill.Next.DialogEvent;
// using SkySwordKill.Next.DialogSystem;
// using SkySwordKill.Next.Res;
// using SkySwordKill.NextMoreCommand.Utils;
// using UnityEngine;
//
//
//     public class FontAb
//     {
//         private bool _isInit;
//         public T LoadAsset<T>(string path) where T : UnityEngine.Object
//         {
//             return AssetBundle == null ? null : AssetBundle.LoadAsset<T>(path);
//         }
//         public UnityEngine.Object LoadAsset(string path, Type type)
//         {
//             return AssetBundle == null ? null : AssetBundle.LoadAsset(path, type);
//         }
//
//         public AssetBundle AssetBundle { get; set; }
//         public FileAsset FileAsset { get; set; }
//         public Dictionary<string, Font> Fonts { get; set; } = new Dictionary<string, Font>();
//         public List<DynamicFont> DynamicFonts { get; set; } = new List<DynamicFont>();
//         public void Init()
//         {
//             if (_isInit)
//             {
//                 return;
//             }
//             if (AssetBundle == null)
//             {
//                 _isInit = true;
//                 return;
//             }
//
//
//             foreach (var asset in AssetBundle.GetAllAssetNames())
//             {
//                 var font = LoadAsset<Font>(asset);
//                 if (font is not null)
//                 {
//                     RegisterFont(font, asset);
//                 }
//             }
//             
//             _isInit = true;
//         }
//         public void RegisterFont(Font font, string alias)
//         {
//             var fontName = Path.GetFileNameWithoutExtension(alias);
//             SkySwordKill.Next.Main.LogInfo($"注册字体名字 {fontName} AB字体路径:{alias} AB包路径:{FileAsset.FileRawPath}");
//             Fonts[fontName] = font;
//             var dynamicFont = new DynamicFont(fontName, font);
//             DynamicFonts.Add(dynamicFont);
//             FontManager.RegisterFont(dynamicFont);
//         }
//         public void UnRegisterFont()
//         {
//             
//             DynamicFonts.ForEach(FontManager.UnregisterFont);
//             DynamicFonts.Clear();
//             Fonts.Clear();
//         }
//     }
//
//     public static class FontUtils
//     {
//         public readonly static Dictionary<string, FileAsset> CacheFileAssets = new Dictionary<string, FileAsset>();
//         public readonly static Dictionary<string, AssetBundle> CacheAssetBundle = new Dictionary<string, AssetBundle>();
//         public readonly static Dictionary<string, FontAb> CacheFontsAB = new Dictionary<string, FontAb>();
//         public static bool GetAssetBundle(string path, out AssetBundle assetBundle) => GetAssetBundle(path, out assetBundle, out var _);
//         public static bool GetAssetBundle(string path, out AssetBundle assetBundle, out FileAsset fileAsset)
//         {
//             if (CacheAssetBundle.TryGetValue(path, out var value))
//             {
//                 assetBundle = value;
//                 fileAsset = CacheFileAssets[path];
//                 return true;
//             }
//             if (GetFileAsset(path, out fileAsset))
//             {
//                 var asset = fileAsset.LoadAsset();
//                 if (asset is AssetBundle bundle)
//                 {
//                     assetBundle = bundle;
//                     CacheAssetBundle.Add(path, assetBundle);
//                     return true;
//                 }
//             }
//             assetBundle = null;
//             return false;
//         }
//         public static bool GetFileAsset(string path, out FileAsset fileAsset)
//         {
//             if (CacheFileAssets.TryGetValue(path, out var asset))
//             {
//                 fileAsset = asset;
//             }
//             else if (!Main.Res.TryGetFileAsset(path, out fileAsset))
//             {
//
//                 return false;
//             }
//             else
//             {
//                 CacheFileAssets.Add(path, fileAsset);
//             }
//
//
//             return true;
//         }
//         public static bool TryRegisterFont(string key, out FontAb fontAb)
//         {
//
//             if (CacheFontsAB.TryGetValue(key, out var value))
//             {
//                 fontAb = value;
//                 return true;
//             }
//             if (GetAssetBundle($"Assets/FGUI/Font/{key}.ab", out var assetBundle, out var fileAsset))
//             {
//
//                 var config = new FontAb();
//                 config.AssetBundle = assetBundle;
//                 config.FileAsset = fileAsset;
//                 config.Init();
//                 fontAb = config;
//                 return true;
//             }
//             fontAb = null;
//             return false;
//         }
//         public static void Clear()
//         {
//             foreach (var fontAb in CacheFontsAB.Values)
//             {
//                 fontAb.UnRegisterFont();
//             }
//             CacheFontsAB.Clear();
//             CacheFileAssets.Clear();
//
//             foreach (var assetBundle in CacheAssetBundle.Values)
//             {
//                 assetBundle.Unload(true);
//             }
//             CacheAssetBundle.Clear();
//         }
//     }
//
// [DialogEvent("RegisterFGUIFont")]
// [DialogEvent("注册FGUI字体")]
// public class RegisterFGUIFont : IDialogEvent
// {
//     public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
//     {
//         // NPCEx.GetJsonString()
//         var fontAb = command.GetStr(0);
//         FontUtils.TryRegisterFont(fontAb,out _);
//         callback?.Invoke();
//     }
// }

