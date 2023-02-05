// using System;
// using System.Collections;
// using Cysharp.Threading.Tasks;
// using HarmonyLib;
// using SkySwordKill.Next;
// using SkySwordKill.NextMoreCommand.Utils;
// using UnityEngine;
//
// namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;
//
// [HarmonyPatch(typeof(MainUIMag), nameof(MainUIMag.OpenMain))]
// public static class MainUIMagOpenMainPatch
// {
//     public static void Postfix()
//     {
//         if (PlayTimeGames.Instance == null)
//         {
//             var go = new GameObject(nameof(PlayTimeGames), typeof(PlayTimeGames));
//         }
//     }
// }
//
// public class PlayTimeGames : MonoBehaviour
// {
//     public static PlayTimeGames Instance;
//     public bool isPlay = false;
//     public int Minute =  10;
//     private void Awake()
//     {
//         if (Instance != null)
//         {
//             Destroy(gameObject);
//         }
//
//         Instance = this;
//         DontDestroyOnLoad(this);
//     }
//
//     private void Update()
//     {
//         if (!isPlay)
//         {
//             isPlay = true;
//             StartCoroutine(PlayTime());
//         }
//   
//     }
//
//     IEnumerator PlayTime()
//     {
//        MyPluginMain.LogInfo(Time.time);
//        MyPluginMain.LogInfo(PlayTimeUtils.GetSeconds());
//         PlayTimeUtils.HasPlayHour();
//         yield return new WaitForSecondsRealtime(Minute*60);
//         isPlay = false;
//        MyPluginMain.LogInfo(Time.time);
//     }
//
//     private void OnDestroy()
//     {
//         Instance = null;
//     }
// }