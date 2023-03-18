using System;
using System.Collections.Generic;
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
        public static AssetBundle GetAssetBundle(string path) => GetAssetBundle(path, out var assetBundle) ? assetBundle : null;
        public static SkeletonDataAsset GetSkeletonData(int avatar) => GetSkeletonData(avatar, out var skeletonData) ? skeletonData : null;
        public static bool GetSkeletonData(int avatar, out SkeletonDataAsset skeletonData)
        {
            var abPath = $"Assets/Avatar/Spine/{avatar.ToString()}/{avatar.ToString()}.ab";
            var skeletonPath = $"Assets/Skeleton/{avatar.ToString()}";

            if (CacheAssetBundle.ContainsKey(abPath))
            {
                var ab = CacheAssetBundle[abPath];
                skeletonData = ab.LoadAsset<SkeletonDataAsset>(skeletonPath);
            }
            else if (!Main.Res.TryGetAsset(abPath, out AssetBundle assetBundle))
            {
                skeletonData = null;

                return false;
            }
            else
            {
                CacheAssetBundle.Add(abPath, assetBundle);
                skeletonData = assetBundle.LoadAsset<SkeletonDataAsset>(skeletonPath);

            }


            return skeletonData != null;
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
        public static void Clear()
        {
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