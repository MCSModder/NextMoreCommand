using HarmonyLib;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(MainUIMag), nameof(MainUIMag.RefreshSave))]
public static class MainUIMagRefreshSavePatch
{
    public static void Prefix(MainUIMag __instance)
    {
        if (ModManager.TryGetModSetting("Quick_SaveSlotDebug", out long saveSlotDebug))
        {
            if (saveSlotDebug > 9)
            {
                var save = (int)saveSlotDebug;
                __instance.maxNum = save;
                __instance.selectAvatarPanel.maxNum = save;
            }
            else
            {
                ModManager.SetModSetting("Quick_SaveSlotDebug", 9);
            }
        }
    }
}

[HarmonyPatch(typeof(MainUIMag), nameof(MainUIMag.OpenMain))]
public static class MainUIMagOpenMainPatch
{
    public static void Prefix()
    {
         AssetsUtils.Clear();
    }
}

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