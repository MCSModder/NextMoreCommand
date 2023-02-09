using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.Res;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

[HarmonyPatch(typeof(ResManager), nameof(ResManager.LoadSpriteAtlas))]
public static class ResManagerLoadSpriteAtlasPatch
{
    public static void Postfix(string path, ref Dictionary<string, Sprite> __result)
    {
        string lower = path.ToLower();
        var files = Main.Res.fileAssets.Where(item => item.Key.StartsWith($"assets/{lower}/"))
            .Select(item => item.Key).ToArray();
        MyLog.Log("ResManager.LoadSpriteAtlas", $"开始加载资源 路径:{path}");
        MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源中 路径:{path} 图片:{files.Length}");
        
        foreach (var filepath in files)
        {
            MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源中 加载路径:{path}  文件路径:{filepath}");

            if (Main.Res.TryGetAsset(filepath,out var file))
            {
                MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源中 加载路径:{path}  文件路径:{filepath} 图片:{file.name}");
                if (file is Texture2D texture)
                {
                    var sprite = Main.Res.GetSpriteCache(texture);
                    sprite.name = Path.GetFileNameWithoutExtension(filepath);
                    MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源完毕  加载路径:{path} 文件名:{Path.GetFileName(filepath)} 图片:{texture.name} 图片:{sprite.name}");
                   
                    __result[sprite.name] = sprite;
                }
            }
            
        }
    }
}