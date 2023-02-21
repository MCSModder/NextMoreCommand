using System;
using System.Collections;
using System.Reflection;
using System.Text;
using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.FCanvas;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Custom.RealizeSeid.Skills;
using SkySwordKill.NextMoreCommand.Patchs;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using XLua;
using YSGame.TuJian;

namespace SkySwordKill.NextMoreCommand;

public class NextMoreCommand : MonoBehaviour
{
    private static NextMoreCommand instance;
    public static NextMoreCommand Instance => instance;
    public bool? AchivementDebug;
    private long _saveSlotDebug = 9;
    public long SaveSlotDebug => _saveSlotDebug <= 9 ? 9 : _saveSlotDebug;
    public bool IsRefresh = false;

    public static NextMoreCommand Create()
    {
        var go = new GameObject(nameof(NextMoreCommand), typeof(NextMoreCommand));
        var component = go.GetComponent<NextMoreCommand>();
        if (component == null)
        {
            go.AddComponent<NextMoreCommand>();
        }

        return component;
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        ModManager.TryGetModSetting("Quick_AchivementDebug", out AchivementDebug);
        ModManager.TryGetModSetting("Quick_SaveSlotDebug", out _saveSlotDebug);

        ModManager.ModSettingChanged += () =>
        {
            ModManager.TryGetModSetting("Quick_AchivementDebug", out AchivementDebug);
            ModManager.TryGetModSetting("Quick_SaveSlotDebug", out _saveSlotDebug);
            if (_saveSlotDebug < 9)
            {
                _saveSlotDebug = 9;
                ModManager.SetModSetting("Quick_SaveSlotDebug", _saveSlotDebug);
            }

            IsRefresh = true;
            MyLog.Log("ModSettingChanged", "修改完");
        };
        ModManager.ModLoadComplete += () =>
        {
            ModManager.TryGetModSetting("Quick_AchivementDebug", out AchivementDebug);
            ModManager.TryGetModSetting("Quick_SaveSlotDebug", out _saveSlotDebug);
            if (_saveSlotDebug < 9)
            {
                _saveSlotDebug = 9;
                ModManager.SetModSetting("Quick_SaveSlotDebug", _saveSlotDebug);
            }

            IsRefresh = true;

            MyLog.Log("ModLoadComplete", "加载完");
        };
    }

    private void Update()
    {
        if (IsRefresh)
        {
            IsRefresh = false;
            StartCoroutine(Refresh());
        }
    }

    private IEnumerator Refresh()
    {
        yield return new WaitForSeconds(1);
        var mag = MainUIMag.inst;
        if (SceneEx.NowSceneName == "MainMenu" && mag.maxNum != (int)_saveSlotDebug &&
            mag.selectAvatarPanel.maxNum != (int)_saveSlotDebug)
        {
            mag.maxNum = (int)_saveSlotDebug;
            mag.selectAvatarPanel.maxNum = (int)_saveSlotDebug;
            mag.RefreshSave();
        }
    }

}