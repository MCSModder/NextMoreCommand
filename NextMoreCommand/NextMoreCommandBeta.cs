using System;
using System.Collections;
using System.Reflection;
using System.Text;
using HarmonyLib;
using ProGif.GifManagers;
using SkySwordKill.Next;
using SkySwordKill.Next.FCanvas;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Custom.RealizeSeid.Skills;
using SkySwordKill.NextMoreCommand.Patchs;
using SkySwordKill.NextMoreCommand.Puerts;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using UnityEngine.Serialization;
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
        gameObject.AddMissingComponent<PGif>();
        DontDestroyOnLoad(this);
        ModManager.TryGetModSetting("Quick_AchivementDebug", out AchivementDebug);
        ModManager.TryGetModSetting("Quick_SaveSlotDebug", out _saveSlotDebug);
        ModManager.ModReload += () =>
        {
            if (jsEnvManager != null)
            {
                jsEnvManager.Reset();
            }

        };
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
    public static void SetParent(Transform transform) => instance.transform.SetParent(transform);
    public static Transform SearchTransform(string path) => instance.FindTransform(path);
    public static GameObject SearchGameObject(string path) => instance.FindGameObject(path);
    public Transform FindTransform(string path) => transform.Find(path);
    public GameObject FindGameObject(string path)
    {
        var tranform1 = FindTransform(path);
        return tranform1 == null ? null : tranform1.gameObject;
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
    public static JsEnvManager JsEnv => instance.jsEnvManager;
    [FormerlySerializedAs("javaScript")] public JsEnvManager jsEnvManager;
    private void Start()
    {
        jsEnvManager = gameObject.AddMissingComponent<JsEnvManager>();
    }
}