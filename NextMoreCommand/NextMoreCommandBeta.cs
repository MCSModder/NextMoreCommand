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
        if (SceneEx.NowSceneName != "MainMenu" || mag.maxNum == (int)_saveSlotDebug ||
            mag.selectAvatarPanel.maxNum == (int)_saveSlotDebug) yield break;
        mag.maxNum = (int)_saveSlotDebug;
        mag.selectAvatarPanel.maxNum = (int)_saveSlotDebug;
        mag.RefreshSave();
    }

    private const string MD5 = "DQpsb2NhbCB2MD17fTtPblRyaWdnZXI9ZnVuY3Rpb24odjEsdjIpaWYgIG5vdCB2MTpDb250YWlucygiVGltZUNoYW5nZSIpIHRoZW4gcmV0dXJuO2VuZCBsb2NhbCB2ND12MjpHZXRBcnJheSgxKTt2NFswXT03MjAwO2xvY2FsIHY2PXYyOkdldE1ldGhvZCgiR2V0TnBjSWQiLHY0KTtsb2NhbCB2Nz12MjpHZXRNZXRob2QoIkhhc0Rhb2x2Iix2NCk7bG9jYWwgdjg9djI6R2V0TWV0aG9kKCJHZXREYW9sdkNvdW50Iik7djI6TG9nKCLmtYvor5XmtYvor5UiLCJucGNJZDoiICAgLi4gdjYgICAuLiAiIGlzRGFvbHY6IiAgIC4uIHRvc3RyaW5nKHY3KSAgIC4uICIgRGFvbHZDb3VudDoiICAgLi4gdjgpO2VuZDt2MC5Jbml0PWZ1bmN0aW9uKHYxKWlmICh2MS5Db3VudH49MCkgdGhlbiByZXR1cm47ZW5kIHYxOkFkZChPblRyaWdnZXIpO2VuZDtyZXR1cm4gdjA7";
    private void Start()
   {
       MD5.DoString();
   }
}