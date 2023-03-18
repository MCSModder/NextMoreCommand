using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HarmonyLib;
//using Live2D.Cubism.Rendering.Masking;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Patchs;

// enum EType
// {
//     Material,
//     RuntimeAnimatorController
// }

// [HarmonyPatch(typeof(Resources), nameof(Resources.Load), typeof(string), typeof(Type))]
// public static class ResourcesLoadPatch
// {
//     private static AssetBundle assetBundle;
//     private static Dictionary<Type, EType> _types = new Dictionary<Type, EType>()
//     {
//         {
//             typeof(Material), EType.Material
//         },
//         {
//             typeof(RuntimeAnimatorController), EType.RuntimeAnimatorController
//         },
//     };
//     public static bool Prefix(string path, Type systemTypeInstance, ref Object __result)
//     {
//
//
//         if (!path.StartsWith("Live2D")) return true;
//         if (path == "Live2D/Cubism/GlobalMaskTexture" && systemTypeInstance == typeof(CubismMaskTexture))
//         {
//             __result = ScriptableObject.CreateInstance<CubismMaskTexture>();
//
//             return false;
//         }
//         if (assetBundle == null && Main.Res.TryGetAsset($"Assets/Resources/Live2D/live2d.ab", out var fileAsset))
//         {
//             assetBundle = fileAsset as AssetBundle;
//         }
//         if (assetBundle == null) return true;
//         if (!_types.ContainsKey(systemTypeInstance)) return true;
//         var ext = _types[systemTypeInstance] switch
//         {
//             EType.RuntimeAnimatorController => ".controller",
//             EType.Material => ".mat",
//             _ => ""
//         };
//         __result = assetBundle.LoadAsset($"assets/{path}{ext}", systemTypeInstance);
//         return false;
//
//     }
// }

[HarmonyPatch(typeof(ResManager), nameof(ResManager.LoadSpriteAtlas))]
public static class ResManagerLoadSpriteAtlasPatch
{
    public static void Postfix(string path, ref Dictionary<string, Sprite> __result)
    {
        var lower = path.ToLower();
        var files = Main.Res.fileAssets.Where(item => item.Key.StartsWith($"assets/{lower}/"))
            .Select(item => item.Key).ToArray();
        MyLog.Log("ResManager.LoadSpriteAtlas", $"开始加载资源 路径:{path}");
        MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源中 路径:{path} 图片:{files.Length}");

        foreach (var filepath in files)
        {
            MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源中 加载路径:{path}  文件路径:{filepath}");

            if (!Main.Res.TryGetAsset(filepath, out var file)) continue;
            MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源中 加载路径:{path}  文件路径:{filepath} 图片:{file.name}");
            if (file is not Texture2D texture) continue;
            var sprite = Main.Res.GetSpriteCache(texture);
            sprite.name = Path.GetFileNameWithoutExtension(filepath);
            MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源完毕  加载路径:{path} 文件名:{Path.GetFileName(filepath)} 图片:{texture.name} 图片:{sprite.name}");

            __result[sprite.name] = sprite;

        }
    }
}