using System.Collections.Generic;
using System.IO;
using System.Text;
using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Patchs;

[HarmonyPatch(typeof(UISiXuItem), nameof(UISiXuItem.SetData))]
public static class UISiXuItemPatch
{
    public static bool Prefix(UISiXuItem __instance, SiXuData sixudata)
    {
        if (sixudata.WuDaoType <= 22)
        {
            return true;
        }

        var instance = Traverse.Create(__instance);
        instance.Field<SiXuData>("data").Value = sixudata;
        __instance.PinJieText.text = sixudata.PinJieStr;
        __instance.TypeText.text = sixudata.WuDaoTypeStr;
        __instance.TimeText.text = sixudata.ShengYuTime;
        __instance.SiXuImage.sprite = UIBiGuanGanWuPanel.Inst.WuDaoTypeSpriteList[8];
        var path = $"Assets/Wudao/Sixu/{sixudata.WuDaoTypeStr}.png".ToLower();
        MyLog.Log("UISiXuItem", $"开始加载资源  加载路径:{path}");
        if (Main.Res.TryGetAsset(path, out var fileAsset))
        {
            if (fileAsset is Texture2D texture2D)
            {
                MyLog.Log("UISiXuItem", $"加载资源完毕  加载路径:{path} 图片:{texture2D.name}");
                __instance.SiXuImage.sprite = Main.Res.GetSpriteCache(texture2D);
            }
        }

        return false;
    }
}

[HarmonyPatch(typeof(UINPCWuDaoSVItem), nameof(UINPCWuDaoSVItem.SetWuDao))]
public static class UINPCWuDaoSVItemPatch
{
    private static List<string> LevelName = new List<string>()
    {
        "一窍不通",
        "初窥门径",
        "略有小成",
        "融会贯通",
        "道之真境",
        "大道已成"
    };

    public static bool Prefix(UINPCWuDaoSVItem __instance, UINPCWuDaoData data)
    {
        if (data.ID <= 22)
        {
            return true;
        }

        var instance = Traverse.Create(__instance);
        __instance.LevelText.text = LevelName[data.Level];
        __instance.TypeImage.sprite = __instance.WuDaoTypeSprites[8];
        var path = $"Assets/Wudao/NPCWuDao/{data.ID}.png".ToLower();
        MyLog.Log("UINPCWuDaoSVItemPatch", $"开始加载资源  加载路径:{path}");
        if (Main.Res.TryGetAsset(path, out var fileAsset))
        {
            if (fileAsset is Texture2D texture2D)
            {
                MyLog.Log("UINPCWuDaoSVItemPatch", $"加载资源完毕  加载路径:{path} 图片:{texture2D.name}");

                __instance.TypeImage.sprite = Main.Res.GetSpriteCache(texture2D);
            }
        }

        var stringBuilder = new StringBuilder();
        foreach (var skillId in data.SkillIDList)
        {
            var uiWuDaoSkillData = UINPCData._WuDaoSkillDict[skillId];
            stringBuilder.AppendLine("#s34#cb47a39" + uiWuDaoSkillData.Name + " #n" + uiWuDaoSkillData.Desc);
            instance.Field<bool>("hasSkill").Value = true;
        }

        if (!instance.Field<bool>("hasSkill").Value)
        {
            __instance.HengTiaoImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        __instance.SkillText.text = stringBuilder.ToString();


        return false;
    }
}