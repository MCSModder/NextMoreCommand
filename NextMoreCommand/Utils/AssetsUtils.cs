using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// using ProGif.GifManagers;
// using ProGif.Lib;
using SkySwordKill.Next;
using SkySwordKill.Next.Mod;
using SkySwordKill.Next.Res;
using SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;
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
        public AssetBundle AssetBundle => AbRefAsset?.RefAssetBundle;
        [JsonIgnore]
        // public FileAsset FileAsset { get; set; }
        public ABRefAsset AbRefAsset { get; set; }
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
        [JsonProperty("玩家头像位置", Required = Required.Default)]
        public CustomSpineOption UiHeadPanelPos { get; set; }
        [JsonProperty("物品背包位置", Required = Required.Default)]
        public CustomSpineOption TabUiMagPos { get; set; }
        [JsonProperty("自定义位置", Required = Required.Default)]
        public Dictionary<string,CustomSpineOption> CustomSpineOptions { get; set; }
        public void Start()
        {
            SayDialogPos ??= CustomSpineOption.SayDialogPos;
            UINpcSvItemPos ??= CustomSpineOption.UINpcSvItemPos;
            UINpcJiaoHuPopPos ??= CustomSpineOption.UINpcJiaoHuPopPos;
            UINpcInfoPanelPos ??= CustomSpineOption.UINpcInfoPanelPos;
            UINpcInfoPanelPos ??= CustomSpineOption.UINpcInfoPanelPos;
            FightAvatarPos ??= CustomSpineOption.FightAvatarPos;
            FpUIMagPos ??= CustomSpineOption.FpUIMagPos;
            LunDaoManagerPos ??= CustomSpineOption.LunDaoManagerPos;
            CgSpineManagerPos ??= new CustomSpineOption();
            UiHeadPanelPos ??= CustomSpineOption.UiHeadPanelPos;
            TabUiMagPos ??= CustomSpineOption.TabUiMagPos;
            CustomSpineOptions ??= new Dictionary<string, CustomSpineOption>();
        }

        [JsonIgnore]
        public AssetBundle AssetBundle => AbRefAsset?.RefAssetBundle;
        [JsonIgnore]
        // public FileAsset FileAsset { get; set; }
        public ABRefAsset AbRefAsset { get; set; }
        [JsonIgnore]
        public Dictionary<string, SkeletonDataAsset> SkeletonDataAssetDictionary = new Dictionary<string, SkeletonDataAsset>();
        [JsonIgnore]
        public Dictionary<string, GameObject> AnimationPrefabDictionary = new Dictionary<string, GameObject>();
        [JsonIgnore]
        public Dictionary<string, List<string>> AnimationNameDictionary = new Dictionary<string, List<string>>();
        [JsonIgnore]
        public Dictionary<string, GameObject> SkeletonGraphicPrefabDictionary = new Dictionary<string, GameObject>();
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
        public GameObject LoadSkeletonGraphic(int key) => LoadSkeletonGraphic(key.ToString());
        public GameObject LoadSkeletonGraphic(string key)
        {

            if (SkeletonGraphicPrefabDictionary.Count == 0)
            {
                return null;
            }
            return SkeletonGraphicPrefabDictionary.TryGetValue(key, out var value) ? value : null;
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
            if (SkinNameDictionary.Count == 0)
            {
                return false;
            }
            var dictResult = SkinNameDictionary.ContainsKey(key);
            var skinResult = dictResult && SkinNameDictionary[key].Contains(skinName);
            return skinResult;
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
                    continue;
                }
                if (assetName.EndsWith("_animation.prefab"))
                {
                    var animationPrefab = LoadAsset<GameObject>(assetName);
                    var filename = Path.GetFileName(assetName).Replace("_animation.prefab", "");
                    AnimationPrefabDictionary.Add(filename, animationPrefab);
                    continue;
                }
                if (assetName.EndsWith("_skeletongraphic.prefab"))
                {
                    var animationPrefab = LoadAsset<GameObject>(assetName);
                    var filename = Path.GetFileName(assetName).Replace("_skeletongraphic.prefab", "");
                    SkeletonGraphicPrefabDictionary.Add(filename, animationPrefab);
                    continue;
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
        // public readonly static Dictionary<string, FileAsset> CacheFileAssets = new Dictionary<string, FileAsset>();
        // public readonly static Dictionary<string, AssetBundle> CacheAssetBundle = new Dictionary<string, AssetBundle>();
        public readonly static Dictionary<string, CustomSpineConfig> CacheCustomSpineConfig = new Dictionary<string, CustomSpineConfig>();
        public readonly static Dictionary<string, CustomSpineOption> CacheCustomImageConfig = new Dictionary<string, CustomSpineOption>();
        public readonly static Dictionary<string, CustomMapPlayerSpine> CacheCustomMapPlayerSpine = new Dictionary<string, CustomMapPlayerSpine>();
        public readonly static Dictionary<string, ABRefAsset> CacheABRefAsset= new Dictionary<string, ABRefAsset>();
        // public static bool GetFileAsset(string path, out FileAsset fileAsset)
        // {
        //     if (CacheFileAssets.TryGetValue(path, out var asset))
        //     {
        //         fileAsset = asset;
        //     }
        //     else if (!Main.Res.FileResLoader.fileAssets.TryGetValue(path, out fileAsset))
        //     {
        //
        //         return false;
        //     }
        //     else
        //     {
        //         CacheFileAssets.Add(path, fileAsset);
        //     }
        //
        //
        //     return true;
        // }
        // public static bool GetAssetBundle(string path, out AssetBundle assetBundle) => GetAssetBundle(path, out assetBundle, out var _);
        // public static bool GetAssetBundle(string path, out AssetBundle assetBundle, out FileAsset fileAsset)
        // {
        // if (CacheAssetBundle.TryGetValue(path, out var value))
        // {
        //     assetBundle = value;
        //     fileAsset = CacheFileAssets[path];
        //     return true;
        // }
        // if (GetFileAsset(path, out fileAsset))
        // {
        //     var asset = fileAsset.Load();
        //     if (asset is AssetBundle bundle)
        //     {
        //         assetBundle = bundle;
        //         CacheAssetBundle.Add(path, assetBundle);
        //         return true;
        //     }
        // }
        //     assetBundle = null;
        //     return false;
        // }
        public static bool TryGetABRefAsset(string path, out  ABRefAsset abRefAsset)
        {
            if (CacheABRefAsset.TryGetValue(path,out abRefAsset))
            {
                return true;
            }
            var abRefAssets = Main.Res.ABResLoader.abRefAssets;
            var _path = path.ToLower();
            foreach (var asset in abRefAssets)
            {
                var abpath = asset.ABPath.ToLower().Replace("\\", "/");
                var isMatch = abpath.EndsWith(_path);
                if (!isMatch) continue;
                abRefAsset = asset;
                CacheABRefAsset[path] = abRefAsset;
                return true;
            }

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
        
        public static bool GetCustomImageConfig(string path, out CustomSpineOption customSpineOption)
        {
            customSpineOption = null;
            if (CacheCustomImageConfig.TryGetValue(path, out customSpineOption))
            {
                return true;
            }

            // if (!GetFileAsset(path, out var fileAsset)) return false;
            if (TryGetABRefAsset(path,out var abRefAsset))
            {
                return false;
            }
            
            var directoryName = Path.GetDirectoryName(abRefAsset.ABPath);
            var configJsonPath = BepInEx.Utility.CombinePaths(directoryName, "config.json");
            MyPluginMain.LogInfo($"configJsonPath:{configJsonPath} directoryName:{directoryName} ");
            customSpineOption = new CustomSpineOption(new CustomSpinePos(0, 3), new CustomSpinePos(0.5f, 0.5f));
            var isExists = File.Exists(configJsonPath);
            switch (isExists)
            {
                case true:
                {
                    var json = File.ReadAllText(configJsonPath);
                    MyPluginMain.LogInfo(json);
                    customSpineOption = JObject.Parse(json).ToObject<CustomSpineOption>() ?? customSpineOption;

                    CacheCustomImageConfig.Add(path, customSpineOption);
                    return true;
                }
                case false when configJsonPath.Contains("本地Mod测试"):
                {
                    CacheCustomImageConfig.Add(path, customSpineOption);
                    using var openWrite = File.OpenWrite(configJsonPath);
                    var info = Encoding.UTF8.GetBytes(customSpineOption.ToString());
                    openWrite.Write(info, 0, info.Length);
                    break;
                }
            }
            return false;
        }
        public static bool GetCustomSpineConfig(string path, string key, out CustomSpineConfig customSpineConfig)
        {

            if (CacheCustomSpineConfig.TryGetValue(key, out var value))
            {
                customSpineConfig = value;
                return true;
            }
            if (TryGetABRefAsset(path,out var abRefAsset))
            {
             
                var directoryName = Path.GetDirectoryName(abRefAsset.ABPath);
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
                config.Start();
                config.AbRefAsset = abRefAsset;
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
            if (TryGetABRefAsset($"Assets/Avatar/MapPlayer/{key}/{key}.ab",out var abRefAsset))
            {

                var config = new CustomMapPlayerSpine();
                config.AbRefAsset = abRefAsset;
                config.Init();
                CacheCustomMapPlayerSpine.Add(key, config);
                customMapPlayerSpine = config;
                return true;
            }
            customMapPlayerSpine = null;
            return false;
        }

        // public static AssetBundle GetAssetBundle(string path) => GetAssetBundle(path, out var assetBundle) ? assetBundle : null;
        // public static AssetBundle GetAssetBundle(int avatar) => GetAssetBundle(avatar.ToString(), out var assetBundle) ? assetBundle : null;
        public static SkeletonDataAsset GetSkeletonData(int avatar) => GetSkeletonData(avatar, out var skeletonData) ? skeletonData : null;
        public static bool GetSkeletonData(int avatar, out SkeletonDataAsset skeletonData, ESpineAssetType spineAssetType = ESpineAssetType.Avatar) => GetSkeletonData(avatar.ToString(), out skeletonData, spineAssetType);
        public static bool GetSkeletonData(string avatar, out SkeletonDataAsset skeletonData, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
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
            Main.LogInfo($"AvatarID:{key}");
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
                ESpineType.UiHeadPanel => customSpineConfig.UiHeadPanelPos,
                ESpineType.TabUiMag => customSpineConfig.TabUiMagPos,
                _ => customSpineOption
            };

            return customSpineOption != null;
        }
        public static bool GetSkeletonAnimation(string key, out GameObject skeletonAnimation, ESpineAssetType spineAssetType)
        {
            Main.LogInfo($"AvatarID:{key}");
            var hasCustomSpineConfig = GetCustomSpineConfig(key, out var customSpineConfig, spineAssetType);
            if (!hasCustomSpineConfig)
            {
                skeletonAnimation = null;
                return false;
            }
            skeletonAnimation = customSpineConfig.LoadSkeletonAnimation(key);
            return skeletonAnimation != null;
        }
        public static bool GetSkeletonAnimation(int avatar, out GameObject skeletonAnimation) => GetSkeletonAnimation(avatar.ToString(), out skeletonAnimation);
        public static bool GetSkeletonAnimation(string avatar, out GameObject skeletonAnimation)
        {
            Main.LogInfo($"AvatarID:{avatar}");
            var hasCustomSpineConfig = GetCustomSpineConfigAvatar(avatar, out var customSpineConfig);
            if (!hasCustomSpineConfig)
            {
                skeletonAnimation = null;
                return false;
            }
            skeletonAnimation = customSpineConfig.LoadSkeletonAnimation(avatar);
            return skeletonAnimation != null;
        }
        public static bool GetSkeletonGraphic(string key, out GameObject skeletonGraphic, ESpineAssetType spineAssetType)
        {
            var hasCustomSpineConfig = GetCustomSpineConfig(key, out var customSpineConfig, spineAssetType);
            if (!hasCustomSpineConfig)
            {
                skeletonGraphic = null;
                return false;
            }
            skeletonGraphic = customSpineConfig.LoadSkeletonGraphic(key);
            return skeletonGraphic != null;
        }
        public static bool GetSkeletonGraphic(int avatar, out GameObject skeletonGraphic) => GetSkeletonGraphic(avatar.ToString(), out skeletonGraphic);
        public static bool GetSkeletonGraphic(string avatar, out GameObject skeletonGraphic)
        {
            var hasCustomSpineConfig = GetCustomSpineConfigAvatar(avatar, out var customSpineConfig);
            if (!hasCustomSpineConfig)
            {
                skeletonGraphic = null;
                return false;
            }
            skeletonGraphic = customSpineConfig.LoadSkeletonGraphic(avatar);
            return skeletonGraphic != null;
        }
        public static string GetName<T>(this T instance) where T : Enum
        {
            return Enum.GetName(typeof(T), instance);
        }
        public static bool CheckAnimation(int avatar, string animationName, out bool isIdle, ESpineAssetType spineAssetType = ESpineAssetType.Avatar) => CheckAnimation(avatar.ToString(), animationName, out isIdle, spineAssetType);
        public static bool CheckAnimation(string avatar, string animationName, out bool isIdle, ESpineAssetType spineAssetType = ESpineAssetType.Avatar)
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
            var uiHeadPanel = UIHeadPanel.Inst;

            if (uiHeadPanel != null)
            {
                var face = uiHeadPanel.Face;
                var skeleton = face != null ? face.GetComponent<SkeletonGraphic>() : null;
                if (skeleton != null && !face.SkeletonDataAsset.Contains(skeleton.SkeletonDataAsset))
                {

                    Object.DestroyImmediate(UIHeadPanel.Inst.gameObject);
                    Object.Instantiate(Resources.Load<GameObject>($"NewUI/Head/UIHeadPanel"), NewUICanvas.Inst.transform);
                }

            }
            SpineObject.Clear();
            CGSpineManager.Instance.Reset();
            
            CacheCustomImageConfig.Clear();
            CacheCustomSpineConfig.Clear();
            CacheCustomMapPlayerSpine.Clear();
            CacheABRefAsset.Clear();
            // CacheFileAssets.Clear();
            //
            // foreach (var assetBundle in CacheAssetBundle.Values)
            // {
            //     assetBundle.Unload(true);
            // }
            // CacheAssetBundle.Clear();
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