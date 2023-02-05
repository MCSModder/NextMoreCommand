using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using YSGame;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

[HarmonyPatch(typeof(YSNewSaveSystem), nameof(YSNewSaveSystem.SaveAvatar))]
public static class SaveGamePatch
{
    private static string PathPre => YSNewSaveSystem.NowAvatarPathPre;
    private static string SavePath => Paths.GetNewSavePath();
    private const string FileName = $"AvatarNextData.json";
    private static string SaveNextPath => $"{SavePath}/{PathPre}/{FileName}";

    public static void Postfix()
    {
       MyPluginMain.LogInfo("开始触发保存角色:保存Next存档数据");
       MyPluginMain.LogInfo($"保存Next存档路径:{SaveNextPath}");
        DialogAnalysis.SaveAvatarNextData(SaveNextPath);
    }
}