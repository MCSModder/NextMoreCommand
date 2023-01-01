using System.Text.RegularExpressions;
using Fungus;
using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;
[HarmonyPatch(typeof(Say),nameof(Say.OnEnter))]
public static class FungusSayPatch
{
    private static string ReplaceSelfName(this string text, string name)
    {
        return text.Replace("我们", "{{WE}}").Replace("我",name).Replace("{{WE}}","我们");
    }
    private static string originText;
    private static Regex _regex = new Regex("我[^们]?");
    private static Traverse _say;
    public static void Prefix(Say __instance)
    {
         _say = Traverse.Create(__instance);
        var npcId = _say.Field<IntegerVariable>("AvatarID").Value.Value;
        if (NpcUtils.SelfNameDict.TryGetValue(npcId,out var value))
        {
            var storyText = _say.Field<string>("storyText");
            originText = storyText.Value;
            storyText.Value = originText.ReplaceSelfName( value);
        }
    }

    public static void Postfix()
    {
        if (string.IsNullOrWhiteSpace(originText) || _say == null)
        {
            _say = null;
            return;
        }
        var storyText = _say.Field<string>("storyText");
        storyText.Value = originText;
        originText = string.Empty;
        _say = null;
    }
}