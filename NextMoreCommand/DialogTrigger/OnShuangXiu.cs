using HarmonyLib;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.DialogTrigger;
[HarmonyPatch(typeof(PlayerEx), nameof(PlayerEx.DoShuangXiu))]
public static class OnShuangXiu
{
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
        if (DialogAnalysis.TryTrigger(new[] { "双修秘术前", "BeforeShuangXiu" }, env, true))
        {
            MyPluginMain.LogInfo("触发双修前触发器");
        }
    }

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
        if (DialogAnalysis.TryTrigger(new[] { "双修秘术后", "AfterShuangXiu" }, env, true))
        {
            MyPluginMain.LogInfo("触发双修后触发器");
        }

   
    }
}
