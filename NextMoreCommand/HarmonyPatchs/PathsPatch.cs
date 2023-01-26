using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

[HarmonyPatch(typeof(Paths),nameof(Paths.GetNewSavePath))]
public static class PathsPatchs
{
    public static void Prefix()
    {
        if (NextMoreCommand.instance == null)
        {
            
            var go  = new GameObject(nameof(NextMoreCommand));
            go.AddComponent<NextMoreCommand>();
            Object.DontDestroyOnLoad(go);
        }
    }
    
}
