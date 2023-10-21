using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HarmonyLib;
//using Live2D.Cubism.Rendering.Masking;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand.Utils;
using Spine.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Patchs;

// enum EType
// {
//     Material,
//     RuntimeAnimatorController
// }

[HarmonyPatch(typeof(Resources), nameof(Resources.Load), typeof(string), typeof(Type))]
public static class ResourcesLoadPatch
{
    private static List<string> _original = new List<string>()
    {
        "MapPlayerYuJian",
        "MapPlayerHeDianZu",
        "MapPlayerWalk"
    };
    public static bool Prefix(string path, Type systemTypeInstance, ref Object __result)
    {


        if (!path.StartsWith("Spine/MapPlayer/") || systemTypeInstance != typeof(SkeletonDataAsset)) return true;
        var spine = path.Replace("Spine/MapPlayer/", "").Split('/')[0];
        if (_original.Contains(spine))
        {
            return true;
        }
        var nowDunShuSpineSeid = MapPlayerController.Inst.NormalShow.NowDunShuSpineSeid;
        var name = Tools.instance.getStaticSkillName(nowDunShuSpineSeid.skillid);
        if (!AssetsUtils.GetCustomMapPlayerSpine(name, out var customMapPlayerSpine)) return true;
        __result = customMapPlayerSpine.LoadSkeletonDataAsset(spine.ToLower());
        return false;

    }
}

[HarmonyPatch(typeof(ResManager), nameof(ResManager.LoadSpriteAtlas))]
public static class ResManagerLoadSpriteAtlasPatch
{
    public static void Postfix(string path, ref Dictionary<string, Sprite> __result)
    {
        var lower = path.ToLower();
        var files = Main.Res.FileResLoader.fileAssets.Where(item => item.Key.ToLower().StartsWith($"assets/{lower}/"))
            .Select(item => item.Key).ToArray();
        MyLog.Log("ResManager.LoadSpriteAtlas", $"开始加载资源 路径:{path}");
        MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源中 路径:{path} 图片:{files.Length}");

        foreach (var filepath in files)
        {
            MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源中 加载路径:{path}  文件路径:{filepath}");
            var texture = Main.Res.LoadAsset<Texture2D>(filepath);
            if (texture is null) continue;
            var sprite = Main.Res.GetSpriteCache(texture);
            sprite.name = Path.GetFileNameWithoutExtension(filepath);
            MyLog.Log("ResManager.LoadSpriteAtlas", $"加载资源完毕  加载路径:{path} 文件名:{Path.GetFileName(filepath)} 图片:{texture.name} 图片:{sprite.name}");

            __result[sprite.name] = sprite;

        }
    }
}