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
    private static string _originText;
    private static Traverse _say;
    public static void Prefix(Say __instance)
    {
         _say = Traverse.Create(__instance);
        var npcId = _say.Field<IntegerVariable>("AvatarID").Value.Value;
        if (NpcUtils.SelfNameDict.TryGetValue(npcId,out var value))
        {
            var storyText = _say.Field<string>("storyText");
            _originText = storyText.Value;
            storyText.Value = _originText.ReplaceSelfName( value);
        }
    }

    public static void Postfix()
    {
        if ( _say != null)
        {
            var storyText = _say.Field<string>("storyText");
            if (!string.IsNullOrWhiteSpace(_originText))
            {
                storyText.Value = _originText;
            }
            
          
        }
        _originText = string.Empty;
        _say = null;
    }
}