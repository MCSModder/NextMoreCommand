using HarmonyLib;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.DialogTrigger;

[HarmonyPatch(typeof(PlayerEx), nameof(PlayerEx.DoShuangXiu))]
public static class OnShuangXiu
{
    private static string[] _triggerTypeBeforeShuangXiu = { "双修秘术前", "BeforeShuangXiu" };
    public static void Prefix(int skillID, UINPCData npc)
    {

        var env = new DialogEnvironment()
        {
            bindNpc = npc,
            roleBindID = npc.ZhongYaoNPCID,
            roleID = npc.ID,
            roleName = npc.Name,
            mapScene = Tools.getScreenName(),
        };
        if (DialogAnalysis.TryTrigger(_triggerTypeBeforeShuangXiu, env, true))
        {
            MyPluginMain.LogInfo("触发双修前触发器");
        }
    }
    private static string[]  _triggerTypeAfterShuangXiu = { "双修秘术后", "AfterShuangXiu" };
    public static void Postfix(int skillID, UINPCData npc)
    {
        var env = new DialogEnvironment()
        {

            bindNpc = npc,
            roleBindID = npc.ZhongYaoNPCID,
            roleID = npc.ID,
            roleName = npc.Name,
            mapScene = Tools.getScreenName()
        };
        if (DialogAnalysis.TryTrigger(_triggerTypeAfterShuangXiu, env, true))
        {
            MyPluginMain.LogInfo("触发双修后触发器");
        }


    }
}