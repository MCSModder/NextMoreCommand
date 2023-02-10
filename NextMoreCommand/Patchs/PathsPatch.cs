using HarmonyLib;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(Paths),nameof(Paths.GetNewSavePath))]
public static class PathsPatchs
{
    public static void Prefix()
    {
        if (NextMoreCommand.Instance == null)
        {
            NextMoreCommand.Create();
        }
    }
    
}
