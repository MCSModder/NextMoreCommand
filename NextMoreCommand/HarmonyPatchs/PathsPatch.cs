using HarmonyLib;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;
[HarmonyPatch(typeof(Paths),nameof(Paths.GetNewSavePath))]
public static class PathsPatch
{
    public static void Prefix()
    {
        if (clientApp.IsTestVersion && NextMoreCommandBeta.instance == null)
        {
            var go  = new GameObject(nameof(NextMoreCommandBeta));
            go.AddComponent<NextMoreCommandBeta>();
            Object.DontDestroyOnLoad(go);
        }
    }
}