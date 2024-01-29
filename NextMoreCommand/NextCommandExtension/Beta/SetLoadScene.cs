// using System;
// using System.Collections.Generic;
// using SkySwordKill.Next;
// using SkySwordKill.Next.DialogEvent;
// using SkySwordKill.Next.DialogSystem;
// using SkySwordKill.Next.Res;
// using SkySwordKill.NextMoreCommand.Attribute;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Beta;
//
// public class CustomSceneAssetBundle
// {
//     public static readonly Dictionary<string, CustomSceneAssetBundle> CustomSceneAssetBundles =
//         new Dictionary<string, CustomSceneAssetBundle>();
//
//     private ResourcesManager Res => Main.Res;
//     public string AbName;
//     public AssetBundle SceneAb;
//     public AssetBundle SceneAssetAb;
//     public bool IsDone => SceneAssetAb != null && SceneAb != null;
//
//     public CustomSceneAssetBundle(string abName)
//     {
//         AbName = abName;
//     }
//
//     public void Load()
//     {
//         LoadSceneAssetAb();
//         LoadSceneAb();
//     }
//
//     private void LoadSceneAb()
//     {
//         if (Res.TryGetAsset($"Assets/Scene/{AbName}/scene.ab", out var fileAsset))
//         {
//             if (fileAsset is AssetBundle assetBundle)
//             {
//                 SceneAb = assetBundle;
//             }
//         }
//     }
//
//     private void LoadSceneAssetAb()
//
//     {
//         if (Res.TryGetAsset($"Assets/Scene/{AbName}/asset.ab", out var fileAsset))
//         {
//             if (fileAsset is AssetBundle assetBundle)
//             {
//                 SceneAssetAb = assetBundle;
//             }
//         }
//     }
//
//     public void Unload()
//     {
//         if (SceneAssetAb != null)
//         {
//             SceneAssetAb.Unload(true);
//         }
//
//         if (SceneAb != null)
//         {
//             SceneAb.Unload(true);
//         }
//     }
// }
//
// [RegisterCommand]
// [DialogEvent("SetLoadScene")]
// public class SetLoadScene : IDialogEvent
// {
//     public string AbName;
//     public string SceneName;
//
//     public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
//     {
//         AbName = command.GetStr(0).ToLower();
//         SceneName = command.GetStr(1);
//         if (!CustomSceneAssetBundle.CustomSceneAssetBundles.ContainsKey(AbName))
//         {
//             var customScene = new CustomSceneAssetBundle(AbName);
//             customScene.Load();
//             CustomSceneAssetBundle.CustomSceneAssetBundles[AbName] = customScene;
//         }
//
//
//         Tools.instance.loadMapScenes(SceneName, false);
//         callback?.Invoke();
//     }
// }