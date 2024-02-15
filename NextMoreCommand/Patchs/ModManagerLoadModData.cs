using HarmonyLib;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.CustomModData;


namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(ModManager), "LoadModData")]
public static class ModManagerLoadModData
{

    public static void Prefix(ModConfig modConfig)
    {
        CustomModDataManager.Read(modConfig);
    }

}