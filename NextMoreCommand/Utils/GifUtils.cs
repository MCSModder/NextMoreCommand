using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;
using Fungus;
using JetBrains.Annotations;
using ProGif.GifManagers;
using ProGif.Lib;
using SkySwordKill.Next;
using SkySwordKill.Next.Res;
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

    public static class GifUtils
    {
        public readonly static Dictionary<string, FileAsset> CacheFileAssets = new Dictionary<string, FileAsset>();
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