using System;
using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using YSGame.TuJian;

namespace SkySwordKill.NextMoreCommand;

public class NextMoreCommandBeta : MonoBehaviour
{
    public static NextMoreCommandBeta instance;
    private KeyCode AchivementDebugKey;
    private Traverse _tuJianManager;
    private void Awake()
    {
        instance = this;
        ModManagerUtils.TryGetModSetting("Quick_AchivementDebugKey", out AchivementDebugKey);
        ModManager.ModSettingChanged += () =>
        {
            ModManagerUtils.TryGetModSetting("Quick_AchivementDebugKey", out AchivementDebugKey);
        };
        ModManager.ModLoadComplete += () =>
        {
            ModManagerUtils.TryGetModSetting("Quick_AchivementDebugKey", out AchivementDebugKey);
        };
        _tuJianManager = Traverse.Create(TuJianManager.Inst);
    }
    private void Update()
    {


        if (Input.GetKeyDown(AchivementDebugKey))
        {
        
            foreach (var jsonObject in jsonData.instance.CreateAvatarJsonData.list)
            {
                    
                if (!jsonObject.HasField("UnlockKey")|| string.IsNullOrWhiteSpace(jsonObject["UnlockKey"].str)) continue;
                var key = jsonObject["UnlockKey"].str;
                Main.LogInfo(key);
                var method = _tuJianManager.Method("UnlockSpecialEvent",  key );
                if (method.MethodExists())
                {
                    method.GetValue();
                }
                TuJianManager.Inst.Save();
            }
        }
    }
}