using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;
using Fungus;
using JetBrains.Annotations;
using ProGif.GifManagers;
using ProGif.Lib;
using SkySwordKill.Next;
using SkySwordKill.Next.Res;
using Spine;
using Spine.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public enum EPose
    {
        Idle,
        Hit,
        Custom
    }

    public class GifOptions
    {

    }

    public static class AssetsUtils
    {
        public readonly static Dictionary<string, FileAsset> CacheFileAssets = new Dictionary<string, FileAsset>();
        public readonly static Dictionary<string, AssetBundle> CacheAssetBundle = new Dictionary<string, AssetBundle>();
        public readonly static Dictionary<int, List<string>> CacheAnimation = new Dictionary<int, List<string>>();
        public static bool GetFileAsset(string path, out FileAsset fileAsset)
        {
            if (CacheFileAssets.ContainsKey(path))
            {
                fileAsset = CacheFileAssets[path];
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
        public static bool GetAssetBundle(string path, out AssetBundle assetBundle)
        {
            if (CacheAssetBundle.ContainsKey(path))
            {
                assetBundle = CacheAssetBundle[path];
                return true;
            }
            if (Main.Res.TryGetAsset(path, out Object asset))
            {
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
        public static bool GetAssetBundle(int avatar, out AssetBundle assetBundle) => GetAssetBundle($"Assets/Avatar/Spine/{avatar.ToString()}/{avatar.ToString()}.ab", out assetBundle);
        public static AssetBundle GetAssetBundle(string path) => GetAssetBundle(path, out var assetBundle) ? assetBundle : null;
        public static AssetBundle GetAssetBundle(int avatar) => GetAssetBundle(avatar, out var assetBundle) ? assetBundle : null;
        public static SkeletonDataAsset GetSkeletonData(int avatar) => GetSkeletonData(avatar, out var skeletonData) ? skeletonData : null;
        public static bool GetSkeletonData(int avatar, out SkeletonDataAsset skeletonData)
        {
            var skeletonPath = $"assets/skeleton/{avatar.ToString()}_skeletondata.asset";
            var hasAssetBundle = GetAssetBundle(avatar, out var assetBundle);
            if (!hasAssetBundle)
            {
                skeletonData = null;
                return false;
            }
            skeletonData = assetBundle.LoadAsset<SkeletonDataAsset>(skeletonPath);
            return skeletonData != null;
        }
        public static bool GetSkeletonAnimation(int avatar, out GameObject skeletonAnimation)
        {
            var skeletonPath = $"assets/skeleton/skeletonanimation.prefab";
            var hasAssetBundle = GetAssetBundle(avatar, out var assetBundle);
            if (!hasAssetBundle)
            {
                skeletonAnimation = null;
                return false;
            }
            skeletonAnimation = assetBundle.LoadAsset<GameObject>(skeletonPath);
            return skeletonAnimation != null;
        }
        public static string GetName<T>(this T instance) where T : Enum
        {
            return Enum.GetName(typeof(T), (object)instance);
        }
        public static bool GetGifPath(int avatar, out string path, EPose pose = EPose.Idle)
        {
            var id = NPCEx.NPCIDToOld(avatar);

            if (GetFileAsset($"Assets/Gif/{id.ToString()}/{pose.GetName()}.gif", out var fileAsset))
            {
                path = fileAsset.FileRawPath;
                return true;
            }
            path = string.Empty;
            return false;
        }
        public static bool CheckAnimation(int avatar, Skeleton skeleton, string animationName)
        {
            var list = new List<string>();
            if (CacheAnimation.ContainsKey(avatar))
            {
                list = CacheAnimation[avatar];
                return list.Contains(animationName);
            }
            CacheAnimation.Add(avatar, list);
            var animations = skeleton.Data.Animations;
            list.AddRange(animations.Select(animation => animation.Name));
            return list.Contains(animationName);
        }
        public static void Clear()
        {
            CacheFileAssets.Clear();
            CacheAnimation.Clear();
            foreach (var assetBundle in CacheAssetBundle.Values)
            {
                assetBundle.Unload(true);
            }
            CacheAssetBundle.Clear();
            if (NextMoreCommand.Instance == null)
            {
                return;

            }
            var dict = PGif.Instance.m_GifPlayerDict;
            var transform = NextMoreCommand.Instance.transform;
            var count = transform.childCount;
            var list = new List<GameObject>();
            for (var i = 0; i < count; i++)
            {
                var child = transform.GetChild(i);
                var go = child.gameObject;
                var component = go.GetComponent<ProGifPlayerComponent>();
                if (component == null) continue;
                go.SetActive(true);
                component.Clear();
                dict.Remove(go.name);
                list.Add(go);
            }
            list.ForEach(Object.DestroyImmediate);
        }
    }
}