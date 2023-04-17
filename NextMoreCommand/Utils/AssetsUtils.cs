﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// using ProGif.GifManagers;
// using ProGif.Lib;
using SkySwordKill.Next;
using SkySwordKill.Next.Res;
using SkySwordKill.NextMoreCommand.Patchs;
using Spine.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public enum ESpineAssetType
    {
        Avatar,
        Cg,
    }


    [JsonObject]
    public class CustomMapPlayerSpine
    {
        [JsonIgnore]
        public AssetBundle AssetBundle { get; set; }
        [JsonIgnore]
        public FileAsset FileAsset { get; set; }
        [JsonIgnore]
        public Dictionary<string, SkeletonDataAsset> SkeletonDataAssetDictionary = new Dictionary<string, SkeletonDataAsset>();
        [JsonIgnore]
        public Dictionary<string, GameObject> AnimationPrefabDictionary = new Dictionary<string, GameObject>();
        [JsonIgnore]
        public Dictionary<string, List<string>> AnimationNameDictionary = new Dictionary<string, List<string>>();
        private bool _isInit;
        public T LoadAsset<T>(string path) where T : Object
        {
            return AssetBundle == null ? null : AssetBundle.LoadAsset<T>(path);
        }
        public Object LoadAsset(string path, Type type)
        {
            return AssetBundle == null ? null : AssetBundle.LoadAsset(path, type);
        }
        public SkeletonDataAsset LoadSkeletonDataAsset(int key) => LoadSkeletonDataAsset(key.ToString());
        public SkeletonDataAsset LoadSkeletonDataAsset(string key)
        {

            if (SkeletonDataAssetDictionary.Count == 0)
            {
                return null;
            }
            return SkeletonDataAssetDictionary.TryGetValue(key, out var value) ? value : null;
        }
        public GameObject LoadSkeletonAnimation(int key) => LoadSkeletonAnimation(key.ToString());
        public GameObject LoadSkeletonAnimation(string key)
        {

            if (AnimationPrefabDictionary.Count == 0)
            {
                return null;
            }
            return AnimationPrefabDictionary.TryGetValue(key, out var value) ? value : null;
        }

        public bool CheckAnimation(int key, string animationName, out bool isIdle) => CheckAnimation(key.ToString(), animationName, out isIdle);
        public bool CheckAnimation(string key, string animationName, out bool isIdle)
        {
            isIdle = false;
            if (AnimationNameDictionary.Count == 0)
            {
                return false;
            }
            var result = AnimationNameDictionary.ContainsKey(key) && AnimationNameDictionary[key].Contains(animationName);
            if (result && animationName.ToLower().Contains("idle"))
            {
                isIdle = true;
            }
            return result;
        }
        public void Init()
        {
            if (_isInit)
            {
                return;
            }
            if (AssetBundle == null)
            {
                _isInit = true;
                return;
            }


            foreach (var assetName in AssetBundle.GetAllAssetNames())
            {
                if (assetName.EndsWith("_skeletondata.asset"))
                {
                    var skeletonDataAsset = LoadAsset<SkeletonDataAsset>(assetName);
                    var filename = Path.GetFileName(assetName).Replace("_skeletondata.asset", "");
                    AnimationNameDictionary.Add(filename, skeletonDataAsset.GetSkeletonData(true).Animations.Select(animation => animation.Name).ToList());
                    SkeletonDataAssetDictionary.Add(filename, skeletonDataAsset);
                }
                else if (assetName.EndsWith("_animation.prefab"))
                {
                    var animationPrefab = LoadAsset<GameObject>(assetName);
                    var filename = Path.GetFileName(assetName).Replace("_animation.prefab", "");
                    AnimationPrefabDictionary.Add(filename, animationPrefab);
                }
            }
            _isInit = true;
        }
        public override string ToString()
        {
            return JObject.FromObject(this).ToString(Formatting.Indented);
        }
        public byte[] ToByte()
        {
            return Encoding.UTF8.GetBytes(ToString());
        }
    }

    [JsonObject]
    public class CustomSpineConfig
    {


        [JsonProperty("剧情对话位置", Required = Required.Default)]
        public CustomSpineOption SayDialogPos { get; set; }
        [JsonProperty("NPC列表位置", Required = Required.Default)]
        public CustomSpineOption UINpcSvItemPos { get; set; }
        [JsonProperty("交互窗口位置", Required = Required.Default)]
        public CustomSpineOption UINpcJiaoHuPopPos { get; set; }
        [JsonProperty("信息窗口位置", Required = Required.Default)]
        public CustomSpineOption UINpcInfoPanelPos { get; set; }
        [JsonProperty("对战立绘位置", Required = Required.Default)]
        public CustomSpineOption FightAvatarPos { get; set; }
        [JsonProperty("战斗窗口位置", Required = Required.Default)]
        public CustomSpineOption FpUIMagPos { get; set; }
        [JsonProperty("论道界面位置", Required = Required.Default)]
        public CustomSpineOption LunDaoManagerPos { get; set; }
        [JsonProperty("CG骨骼位置", Required = Required.Default)]
        public CustomSpineOption CgSpineManagerPos { get; set; }

        [JsonIgnore]
        public AssetBundle AssetBundle { get; set; }
        [JsonIgnore]
        public FileAsset FileAsset { get; set; }
        [JsonIgnore]
        public Dictionary<string, SkeletonDataAsset> SkeletonDataAssetDictionary = new Dictionary<string, SkeletonDataAsset>();
        [JsonIgnore]
        public Dictionary<string, GameObject> AnimationPrefabDictionary = new Dictionary<string, GameObject>();
        [JsonIgnore]
        public Dictionary<string, List<string>> AnimationNameDictionary = new Dictionary<string, List<string>>();
        [JsonIgnore]
        public Dictionary<string, List<string>> SkinNameDictionary = new Dictionary<string, List<string>>();
        private bool _isInit;
        public T LoadAsset<T>(string path) where T : Object
        {
            return AssetBundle == null ? null : AssetBundle.LoadAsset<T>(path);
        }
        public Object LoadAsset(string path, Type type)
        {
            return AssetBundle == null ? null : AssetBundle.LoadAsset(path, type);
        }
        public SkeletonDataAsset LoadSkeletonDataAsset(int key) => LoadSkeletonDataAsset(key.ToString());
        public SkeletonDataAsset LoadSkeletonDataAsset(string key)
        {

            if (SkeletonDataAssetDictionary.Count == 0)
            {
                return null;
            }
            return SkeletonDataAssetDictionary.TryGetValue(key, out var value) ? value : null;
        }
        public GameObject LoadSkeletonAnimation(int key) => LoadSkeletonAnimation(key.ToString());
        public GameObject LoadSkeletonAnimation(string key)
        {

            if (AnimationPrefabDictionary.Count == 0)
            {
                return null;
            }
            return AnimationPrefabDictionary.TryGetValue(key, out var value) ? value : null;
        }

        public bool CheckAnimation(int key, string animationName, out bool isIdle) => CheckAnimation(key.ToString(), animationName, out isIdle);
        public bool CheckAnimation(string key, string animationName, out bool isIdle)
        {
            isIdle = false;
            if (AnimationNameDictionary.Count == 0)
            {
                return false;
            }
            var result = AnimationNameDictionary.ContainsKey(key) && AnimationNameDictionary[key].Contains(animationName);
            if (result && animationName.ToLower().Contains("idle"))
            {
                isIdle = true;
            }
            return result;
        }
        public bool CheckSkin(int key, string skinName) => CheckSkin(key.ToString(), skinName);
        public bool CheckSkin(string key, string skinName)
        {

            if (AnimationNameDictionary.Count == 0)
            {
                return false;
            }


            return SkinNameDictionary.ContainsKey(key) && SkinNameDictionary[key].Contains(skinName);
        }
        public void Init()
        {
            if (_isInit)
            {
                return;
            }
            if (AssetBundle == null)
            {
                _isInit = true;
                return;
            }


            foreach (var assetName in AssetBundle.GetAllAssetNames())
            {
                if (assetName.EndsWith("_skeletondata.asset"))
                {
                    var skeletonDataAsset = LoadAsset<SkeletonDataAsset>(assetName);

                    var filename = Path.GetFileName(assetName).Replace("_skeletondata.asset", "");
                    AnimationNameDictionary.Add(filename, skeletonDataAsset.GetSkeletonData(true).Animations.Select(animation => animation.Name).ToList());
                    SkinNameDictionary.Add(filename, skeletonDataAsset.GetSkeletonData(true).Skins.Select(skin => skin.Name).ToList());
                    SkeletonDataAssetDictionary.Add(filename, skeletonDataAsset);
                }
                else if (assetName.EndsWith("_animation.prefab"))
                {
                    var animationPrefab = LoadAsset<GameObject>(assetName);
                    var filename = Path.GetFileName(assetName).Replace("_animation.prefab", "");
                    AnimationPrefabDictionary.Add(filename, animationPrefab);
                }
            }
            _isInit = true;
        }
        public override string ToString()
        {
            return JObject.FromObject(this).ToString(Formatting.Indented);
        }
        public byte[] ToByte()
        {
            return Encoding.UTF8.GetBytes(ToString());
        }
    }

    public static class AssetsUtils
    {
        public readonly static Dictionary<string, FileAsset> CacheFileAssets = new Dictionary<string, FileAsset>();
        public readonly static Dictionary<string, AssetBundle> CacheAssetBundle = new Dictionary<string, AssetBundle>();
        public readonly static Dictionary<string, CustomSpineConfig> CacheCustomSpineConfig = new Dictionary<string, CustomSpineConfig>();
        public readonly static Dictionary<string, CustomMapPlayerSpine> CacheCustomMapPlayerSpine = new Dictionary<string, CustomMapPlayerSpine>();
        public static bool GetFileAsset(string path, out FileAsset fileAsset)
        {
            if (CacheFileAssets.TryGetValue(path, out var asset))
            {
                fileAsset = asset;
            }
            else if (!Main.Res.TryGetFileAsset(path, out fileAsset))
            {

                return false;
            }
            else
            {
                CacheFileAssets.Add(path, fileAsset);
            }


            return true;
        }
        public static bool GetAssetBundle(string path, out AssetBundle assetBundle) => GetAssetBundle(path, out assetBundle, out var _);
        public static bool GetAssetBundle(string path, out AssetBundle assetBundle, out FileAsset fileAsset)
        {
            if (CacheAssetBundle.TryGetValue(path, out var value))
            {
                assetBundle = value;
                fileAsset = CacheFileAssets[path];
                return true;
            }
            if (GetFileAsset(path, out fileAsset))
            {
                var asset = fileAsset.LoadAsset();
                if (asset is AssetBundle bundle)
                {
                    assetBundle = bundle;
                    CacheAssetBundle.Add(path, assetBundle);
                    return true;
                }
            }
            assetBundle = null;
            return false;
        }
        public static bool GetCustomSpineConfigAvatar(int avatar, out CustomSpineConfig customSpineConfig) => GetCustomSpineConfigAvatar(avatar.ToString(), out customSpineConfig);
        public static bool GetCustomSpineConfigAvatar(string key, out CustomSpineConfig customSpineConfig) => GetCustomSpineConfig($"Assets/Avatar/Spine/{key}/{key}.ab", $"Avatar-{key}", out customSpineConfig);

        public static bool GetCustomSpineConfigCG(string key, out CustomSpineConfig customSpineConfig) => GetCustomSpineConfig($"Assets/CG/Spine/{key}/{key}.ab", $"CG-{key}", out customSpineConfig);

        public static bool GetCustomSpineConfig(int key, out CustomSpineConfig customSpineConfig, ESpineAssetType spineAssetType = ESpineAssetType.Avatar) => GetCustomSpineConfig(key.ToString(), out customSpineConfig, spineAssetType);
        public static bool GetCustomSpineConfig(string key, out CustomSpineConfig customSpineConfig, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
        {
            customSpineConfig = null;
            return spineAssetType switch
            {
                ESpineAssetType.Avatar => GetCustomSpineConfigAvatar(key, out customSpineConfig),
                ESpineAssetType.Cg => GetCustomSpineConfigCG(key, out customSpineConfig),
                _ => throw new ArgumentOutOfRangeException(nameof(spineAssetType), spineAssetType, null)
            };
        }
        public static bool GetCustomSpineConfig(string path, string key, out CustomSpineConfig customSpineConfig)
        {

            if (CacheCustomSpineConfig.TryGetValue(key, out var value))
            {
                customSpineConfig = value;
                return true;
            }
            if (GetAssetBundle(path, out var assetBundle, out var fileAsset))
            {
                var directoryName = Path.GetDirectoryName(fileAsset.FileRawPath);
                var configJsonPath = BepInEx.Utility.CombinePaths(directoryName, "config.json");
                MyPluginMain.LogInfo($"configJsonPath:{configJsonPath} directoryName:{directoryName} ");
                var config = new CustomSpineConfig();
                var isExists = File.Exists(configJsonPath);
                if (isExists)
                {
                    var json = File.ReadAllText(configJsonPath);
                    MyPluginMain.LogInfo(json);
                    config = JObject.Parse(json).ToObject<CustomSpineConfig>() ?? new CustomSpineConfig();

                }

                config.AssetBundle = assetBundle;
                config.FileAsset = fileAsset;
                config.Init();
                if (!isExists && configJsonPath.Contains("本地Mod测试"))
                {
                    using (var openWrite = File.OpenWrite(configJsonPath))
                    {
                        var info = config.ToByte();
                        openWrite.Write(info, 0, info.Length);
                    }

                }
                CacheCustomSpineConfig.Add(key, config);
                customSpineConfig = config;
                return true;
            }
            customSpineConfig = null;
            return false;
        }
        public static bool GetCustomMapPlayerSpine(string key, out CustomMapPlayerSpine customMapPlayerSpine)
        {

            if (CacheCustomMapPlayerSpine.TryGetValue(key, out var value))
            {
                customMapPlayerSpine = value;
                return true;
            }
            if (GetAssetBundle($"Assets/Avatar/MapPlayer/{key}/{key}.ab", out var assetBundle, out var fileAsset))
            {

                var config = new CustomMapPlayerSpine();
                config.AssetBundle = assetBundle;
                config.FileAsset = fileAsset;
                config.Init();
                CacheCustomMapPlayerSpine.Add(key, config);
                customMapPlayerSpine = config;
                return true;
            }
            customMapPlayerSpine = null;
            return false;
        }

        public static AssetBundle GetAssetBundle(string path) => GetAssetBundle(path, out var assetBundle) ? assetBundle : null;
        public static AssetBundle GetAssetBundle(int avatar) => GetAssetBundle(avatar.ToString(), out var assetBundle) ? assetBundle : null;
        public static SkeletonDataAsset GetSkeletonData(int avatar) => GetSkeletonData(avatar, out var skeletonData) ? skeletonData : null;
        public static bool GetSkeletonData(int avatar, out SkeletonDataAsset skeletonData, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
        {
            var hasCustomSpineConfig = GetCustomSpineConfig(avatar, out var customSpineConfig, spineAssetType);
            if (!hasCustomSpineConfig)
            {
                skeletonData = null;
                return false;
            }
            skeletonData = customSpineConfig.LoadSkeletonDataAsset(avatar);
            return skeletonData != null;
        }
        public static bool GetCustomSpineOption(int avatar, ESpineType spineType, out CustomSpineOption customSpineOption, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
        {
            customSpineOption = null;
            return avatar > 0 && GetCustomSpineOption(avatar.ToString(), spineType, out customSpineOption, spineAssetType);
        }
        public static bool GetCustomSpineOption(string key, ESpineType spineType, out CustomSpineOption customSpineOption, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
        {
            customSpineOption = null;
            if (spineType == ESpineType.None)
            {

                return false;
            }

            var hasCustomSpineConfig = GetCustomSpineConfig(key, out var customSpineConfig, spineAssetType);
            if (!hasCustomSpineConfig)
            {
                return false;
            }

            customSpineOption = spineType switch
            {
                ESpineType.FpUIMag => customSpineConfig.FpUIMagPos,
                ESpineType.FightAvatar => customSpineConfig.FightAvatarPos,
                ESpineType.SayDialog => customSpineConfig.SayDialogPos,
                ESpineType.UINpcInfoPanel => customSpineConfig.UINpcInfoPanelPos,
                ESpineType.UINpcSvItem => customSpineConfig.UINpcSvItemPos,
                ESpineType.UINpcJiaoHuPop => customSpineConfig.UINpcJiaoHuPopPos,
                ESpineType.LunDaoManager => customSpineConfig.LunDaoManagerPos,
                ESpineType.CGManager => customSpineConfig.CgSpineManagerPos,
                _ => customSpineOption
            };

            return customSpineOption != null;
        }
        public static bool GetSkeletonAnimation(string key, out GameObject skeletonAnimation, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
        {
            var hasCustomSpineConfig = GetCustomSpineConfig(key, out var customSpineConfig, spineAssetType);
            if (!hasCustomSpineConfig)
            {
                skeletonAnimation = null;
                return false;
            }
            skeletonAnimation = customSpineConfig.LoadSkeletonAnimation(key);
            return skeletonAnimation != null;
        }
        public static bool GetSkeletonAnimation(int avatar, out GameObject skeletonAnimation)
        {
            var hasCustomSpineConfig = GetCustomSpineConfigAvatar(avatar, out var customSpineConfig);
            if (!hasCustomSpineConfig)
            {
                skeletonAnimation = null;
                return false;
            }
            skeletonAnimation = customSpineConfig.LoadSkeletonAnimation(avatar);
            return skeletonAnimation != null;
        }
        public static string GetName<T>(this T instance) where T : Enum
        {
            return Enum.GetName(typeof(T), instance);
        }

        public static bool CheckAnimation(int avatar, string animationName, out bool isIdle, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
        {
            isIdle = false;
            var hasCustomSpineConfig = GetCustomSpineConfig(avatar, out var customSpineConfig, spineAssetType);
            return hasCustomSpineConfig && customSpineConfig.CheckAnimation(avatar, animationName, out isIdle);
        }
        public static bool CheckAnimation(string key, string animationName, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
        {

            var hasCustomSpineConfig = GetCustomSpineConfig(key, out var customSpineConfig, spineAssetType);
            return hasCustomSpineConfig && customSpineConfig.CheckAnimation(key, animationName, out var _);
        }
        public static bool CheckSkin(int avatar, string skinName)
        {


            return CheckSkin(avatar.ToString(), skinName);
        }
        public static bool CheckSkin(string key, string skinName, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
        {

            var hasCustomSpineConfig = GetCustomSpineConfig(key, out var customSpineConfig, spineAssetType);
            return hasCustomSpineConfig && customSpineConfig.CheckSkin(key, skinName);
        }
        public static void Clear()
        {
            CacheCustomSpineConfig.Clear();
            CacheCustomMapPlayerSpine.Clear();
            CacheFileAssets.Clear();
            foreach (var assetBundle in CacheAssetBundle.Values)
            {
                assetBundle.Unload(true);
            }
            CacheAssetBundle.Clear();
            if (NextMoreCommand.Instance == null)
            {
                return;

            }
            // var dict = PGif.Instance.m_GifPlayerDict;
            // var transform = NextMoreCommand.Instance.transform;
            // var count = transform.childCount;
            // var list = new List<GameObject>();
            // for (var i = 0; i < count; i++)
            // {
            //     var child = transform.GetChild(i);
            //     var go = child.gameObject;
            //     var component = go.GetComponent<ProGifPlayerComponent>();
            //     if (component == null) continue;
            //     go.SetActive(true);
            //     component.Clear();
            //     dict.Remove(go.name);
            //     list.Add(go);
            // }
            // list.ForEach(Object.DestroyImmediate);
        }
    }
}