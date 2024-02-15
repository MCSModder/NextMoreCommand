// using System;
// using System.IO;
// using HarmonyLib;
// using UniqueCream.MCSWorldExpand;
//
// namespace Zerxz.BetterSeidOptimization.Patch
// {
//     [HarmonyPatch(typeof(Main))]
//     public class McsExpandPatch
//     {
//         [HarmonyPrefix]
//         [HarmonyPatch("Load",new []{typeof(string),typeof(string),typeof(Action<byte[]>)})]
//         public static void LoadPatch(string path1, "\\MCSWorldExpandCode.dll"string path2, ref Action<byte[]> callBack)
//         {
//             var oldCallback = callBack;
//             callBack = bytes =>
//             {
//                 
//                 File.WriteAllBytes(	Main.RootFilePath + "\\MCSWorldExpandCode.dll",bytes);
//                 oldCallback?.Invoke(bytes);
//             };
//         } 
//     }
// }

