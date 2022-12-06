using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.FGUI;
using SkySwordKill.Next.ModGUI;
using SkySwordKill.NextFGUI.NextCore;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

[HarmonyPatch(typeof(ModMainWindow), "InitLink")]
public static class ModMainWindowInitLink
{
    public static void Postfix(ModMainWindow __instance)
    {
        var list = __instance.MainView.m_listLink;
        var item = list.AddItemFromPool();
        item.text = "自定义副本";
        item.onClick.Add(() => Main.LogInfo("打开副本"));
        item.cursor = FGUIManager.MOUSE_HAND;
    }
}

