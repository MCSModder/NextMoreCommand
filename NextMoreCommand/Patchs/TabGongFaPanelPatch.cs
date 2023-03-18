using HarmonyLib;
using Tab;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(TabGongFaPanel), "Init")]
    public static class TabGongFaPanelPatch
    {
        private static void Postfix()
        {

        }
    }
}