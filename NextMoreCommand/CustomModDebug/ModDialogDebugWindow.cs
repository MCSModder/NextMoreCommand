using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using HarmonyLib;
using KBEngine;
using script.NewLianDan;
using script.Submit;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.FGUI;
using SkySwordKill.Next.FGUI.Component;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore;
using SkySwordKill.NextMoreCommand.Utils;
using Tab;
using UnityEngine;
using YSGame;
using GameObject = UnityEngine.GameObject;

namespace SkySwordKill.NextMoreCommand.CustomModDebug;

static class CtlToolsSearchBoxExtends
{
    public static void OnChanged(this CtlToolsSearchBox instance, Action<string> onSizeChanged)
    {
        instance.SearchBox.m_inContent.onSizeChanged.Add((() => { onSizeChanged?.Invoke(instance.SearchContent); }));
    }
}

public enum DramaState
{
    None,
    Drama,
    Command
}

public struct NextEventCharacter
{
    public string Name;
    public int Id;

    public NextEventCharacter(string name, int id)
    {
        Name = name;
        Id = id;
    }
}

public struct NextEventOption
{
    public string Text;
    public string EventName;

    public NextEventOption(string text, string eventName = "")
    {
        Text = text;
        EventName = eventName;
    }
}

public enum ReloadState
{
    None,
    Next,
    NextMainMenu,
    MainMenu
}

public class NextReloadManager : MonoBehaviour
{
    private static NextReloadManager instance;

    public static NextReloadManager Instance => instance
        ? instance
        : new GameObject(nameof(NextReloadManager), typeof(NextReloadManager)).GetComponent<NextReloadManager>();

    public ReloadState ReloadState = ReloadState.None;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(instance);
            return;
        }

        instance = this;
    }

    private void Update()
    {
        switch (ReloadState)
        {
            case ReloadState.Next:
                ReloadState = ReloadState.None;
                StartCoroutine(ReloadMod());
                return;
            case ReloadState.NextMainMenu:
                ReloadState = ReloadState.None;
                StartCoroutine(ReloadModCheckBox());
                return;
            case ReloadState.MainMenu:
                ReloadState = ReloadState.None;
                StartCoroutine(ReloadMainMenu());
                return;
        }
    }

    public IEnumerator ReloadMainMenu()
    {
        TabUIMag.OpenTab(6);
        yield return new WaitForSeconds(2);
        TySelect.inst.Show("是否要返回主界面？", delegate
        {
            if (FpUIMag.inst != null)
            {
                UnityEngine.Object.Destroy(FpUIMag.inst.gameObject);
            }

            if (TpUIMag.inst != null)
            {
                UnityEngine.Object.Destroy(TpUIMag.inst.gameObject);
            }

            if (SubmitUIMag.Inst != null)
            {
                SubmitUIMag.Inst.Close();
            }

            if (LianDanUIMag.Instance != null)
            {
                UnityEngine.Object.Destroy(LianDanUIMag.Instance.gameObject);
            }

            if (LianQiTotalManager.inst != null)
            {
                UnityEngine.Object.Destroy(LianQiTotalManager.inst.gameObject);
            }


            TabUIMag.Instance.TryEscClose();
            YSSaveGame.Reset();
            KBEngineApp.app.entities[10] = null;
            KBEngineApp.app.entities.Remove(10);
            Tools.instance.loadOtherScenes("MainMenu");
            DestroyImmediate(this);
        }, null, true);
    }

    private IEnumerator ReloadMod()
    {
        TabUIMag.OpenTab(6);
        yield return new WaitForSeconds(2);
        TySelect.inst.Show("是否要返回主界面并重载NextMod？", () =>
        {
            if (FpUIMag.inst != null)
            {
                UnityEngine.Object.Destroy(FpUIMag.inst.gameObject);
            }

            if (TpUIMag.inst != null)
            {
                UnityEngine.Object.Destroy(TpUIMag.inst.gameObject);
            }

            if (SubmitUIMag.Inst != null)
            {
                SubmitUIMag.Inst.Close();
            }

            if (LianDanUIMag.Instance != null)
            {
                UnityEngine.Object.Destroy(LianDanUIMag.Instance.gameObject);
            }

            if (LianQiTotalManager.inst != null)
            {
                UnityEngine.Object.Destroy(LianQiTotalManager.inst.gameObject);
            }


            TabUIMag.Instance.TryEscClose();

            YSSaveGame.Reset();
            KBEngineApp.app.entities[10] = null;
            KBEngineApp.app.entities.Remove(10);
            Tools.instance.loadOtherScenes("MainMenu");
            DestroyImmediate(this);
            ModManager.ReloadAllMod();
        }, null, true);
    }
    

    private IEnumerator ReloadModCheckBox()
    {
        TabUIMag.OpenTab(6);
        yield return new WaitForSeconds(2);
        if (FpUIMag.inst != null)
        {
            UnityEngine.Object.Destroy(FpUIMag.inst.gameObject);
        }

        if (TpUIMag.inst != null)
        {
            UnityEngine.Object.Destroy(TpUIMag.inst.gameObject);
        }

        if (SubmitUIMag.Inst != null)
        {
            SubmitUIMag.Inst.Close();
        }

        if (LianDanUIMag.Instance != null)
        {
            UnityEngine.Object.Destroy(LianDanUIMag.Instance.gameObject);
        }

        if (LianQiTotalManager.inst != null)
        {
            UnityEngine.Object.Destroy(LianQiTotalManager.inst.gameObject);
        }


        TabUIMag.Instance.TryEscClose();

        YSSaveGame.Reset();
        KBEngineApp.app.entities[10] = null;
        KBEngineApp.app.entities.Remove(10);
        Tools.instance.loadOtherScenes("MainMenu");
        DestroyImmediate(this);
        ModManager.ReloadAllMod();
    }
}

public struct NextEventCommand
{
    public string Name;
    public string Text;


    public NextEventCommand(string name, string text = "")
    {
        Text = text;
        Name = name;
    }
}

public class ModDialogDebugWindow : FGUIWindowBase
{
    public static ModDialogDebugWindow Instance;
    public UI_ModDialogDebug MainView => (UI_ModDialogDebug)contentPane;

    public ModDialogDebugWindow() : base("NextMoreCore", "ModDialogDebug")
    {
    }

    private ConfigTarget<bool> DebugMode => Main.I.DebugMode;

    private bool _isGame = false;
    private Dictionary<string, string> _tempVar = new Dictionary<string, string>();

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (_refreshSeachBox > 0f)
        {
            _refreshSeachBox -= Time.deltaTime;
        }
        else
        {
            _refreshSeachBox = 0f;
        }

        _isGame = Tools.instance != null && Tools.instance.getPlayer() != null;

        _tempVar["gametext"] = _isGame ? string.Empty : "未开始游戏";
        _tempVar["IsRunningEvent"] = DialogAnalysis.IsRunningEvent ? "运行中" : "未运行";
        _tempVar["EventQueue"] = DialogAnalysis.EventQueue.Count.ToString();
        MainView.m_drama.m_resetState.m_resetState.templateVars = _tempVar;
        MainView.m_command.m_resetState.m_resetState.templateVars = _tempVar;

        MainView.m_debugText.templateVars = _tempVar;
        MainView.m_debugBar.m_commandButton.visible = _isGame;
        MainView.m_debugBar.m_dramaIdButton.visible = _isGame;
        MainView.m_debugBar.m_mainMenuButton.visible = _isGame;

        SetState(DebugMode.Value && _isGame ? CurrentState : DramaState.None);
    }


    protected override void OnInit()
    {
        base.OnInit();
        _isGame = Tools.instance != null && Tools.instance.getPlayer() != null;
        BindDebugBar();

        _currentState = DramaState.Drama;
        SetState(DebugMode.Value && _isGame ? CurrentState : DramaState.None);
        BindDrama();
        //BindCommand();
        Instance = this;
    }

    private void BindDebugBar()
    {
        MainView.m_frame.m_closeButton.onClick.Add(OnClickClose);
        MainView.m_debugBar.m_debugButton.selected = DebugMode.Value;

        MainView.m_debugBar.m_debugButton.onClick.Add((() =>
        {
            DebugMode.Value = MainView.m_debugBar.m_debugButton.selected;
            _tempVar["gametext"] = _isGame ? string.Empty : "未开始游戏";

            SetState(DebugMode.Value && _isGame ? _currentState : DramaState.None);
        }));
        MainView.m_debugBar.m_mainMenuButton.onClick.Add((() =>
            NextReloadManager.Instance.ReloadState = ReloadState.MainMenu));
        MainView.m_debugBar.m_dramaIdButton.onClick.Add(() =>
        {
            NextReloadManager.Instance.ReloadState = ReloadState.NextMainMenu;
        });

        MainView.m_debugBar.m_commandButton.onClick.Add(
            () => { NextReloadManager.Instance.ReloadState = ReloadState.Next; });
    }


    private void BindDrama()
    {
        var drama = MainView.m_drama;
        drama.SearchBox.OnSearch = OnSearch;
        drama.SearchBox.OnChanged(OnChangedSearch);
        drama.m_resetState.m_resetButton.onClick.Add((() => DialogAnalysis.CancelEvent()));
    }

    private IEnumerable<string> _commandKeys;

    private void BindCommand()
    {
        _commandKeys = Traverse.Create(typeof(DialogAnalysis))
            .Field<Dictionary<string, IDialogEvent>>("_registerEvents").Value
            .Keys;
        var command = MainView.m_command;
        command.m_resetState.m_resetButton.onClick.Add((() => DialogAnalysis.CancelEvent()));
        command.m_addCommand.SearchBox.SetItems(_commandKeys);
        command.m_addCommand.SearchBox.OnChanged = () =>
        {
            var index = command.m_addCommand.SearchBox.SelectedIndex;
            MyPluginMain.LogInfo($"index:{index.ToString()} Name:{_commandKeys.ToList()[index]}");
        };
        command.m_addCommand.m_addButton.onClick.Add((() =>
        {
            var index = command.m_addCommand.SearchBox.SelectedIndex;
            var s = command.m_addCommand.m_commandInput.m_inContent.text;
            var _com = new NextEventCommand(_commandKeys.ToList()[index], s);
            _nextEventCommands.Add(_com);
            var item = (UI_ComboCommandItem)command.m_commandList.AddItemFromPool().asCom;

            item.SearchBox.SetItems(_commandKeys);
            item.SearchBox.SelectedIndex = index;
            item.m_commandInput.m_inContent.text = s;
            item.m_deleteButton.onClick.Add((() => _nextEventCommands.Remove(_com)));
        }));
        RefreshCommandList();
    }

    private void SetState(DramaState state)
    {
        switch (state)
        {
            case DramaState.Drama:
                MainView.m_drama.visible = _isGame;
                MainView.m_command.visible = false;
                MainView.m_debugText.visible = !_isGame;
                if (_isGame)
                {
                    RefreshIntList();
                    RefreshStringList();
                    RefreshDrama();
                }

                break;
            case DramaState.Command:
                MainView.m_drama.visible = false;
                MainView.m_command.visible = _isGame;
                MainView.m_debugText.visible = !_isGame;
                break;
            case DramaState.None:
                MainView.m_drama.visible = false;
                MainView.m_command.visible = false;
                MainView.m_debugText.visible = true;
                break;
        }
    }

    #region 剧情调试相关

    private DramaState _currentState;

    public DramaState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    private float _refreshSeachBox = 0f;
    private List<string> _recentDramaId = new List<string>(20);
    private string _seachText;

    private void OnChangedSearch(string obj)
    {
        if (_refreshSeachBox == 0f)
        {
            _refreshSeachBox = 5f;
            _seachText = obj;
        }
    }

    private void OnSearch(string obj)
    {
        _seachText = obj;
        _refreshSeachBox = 0f;
    }

    private void RefreshDramaId()
    {
        var dramaIdList = MainView.m_drama.m_listDramaId;
        if (_recentDramaId.Count == dramaIdList.numItems)
        {
            return;
        }

        dramaIdList.RemoveChildrenToPool();

        for (int i = _recentDramaId.Count - 1; i >= 0; i--)
        {
            var item = dramaIdList.AddItemFromPool().asButton;
            var key = _recentDramaId[i];
            item.text = key;
            item.onClick.Add((() => DialogAnalysis.StartDialogEvent(key)));
            item.cursor = FGUIManager.MOUSE_HAND;
        }
    }

    private void RefreshIntList()
    {
        var listInt = MainView.m_drama.m_ListInt;
        listInt.RemoveChildrenToPool();
        foreach (var info in DialogAnalysis.GetAllInt())
        {
            listInt.AddItemFromPool().asButton.text = $"{info.Key}:{info.Value.ToString()}";
        }
    }

    private void RefreshStringList()
    {
        var listString = MainView.m_drama.m_listString;
        listString.RemoveChildrenToPool();
        foreach (var info in DialogAnalysis.GetAllStr())
        {
            listString.AddItemFromPool().asButton.text = $"{info.Key}:{info.Value}";
        }
    }

    private void RefreshDrama()
    {
        var list = MainView.m_drama.m_list;


        if (_refreshSeachBox == 0f)
        {
            _seachText = MainView.m_drama.SearchBox.SearchContent;
        }

        IEnumerable<string> keys = DialogAnalysis.DialogDataDic.Keys;
        keys = string.IsNullOrWhiteSpace(_seachText) ? keys : keys.Where(key => key.Contains(_seachText));
        var enumerable = keys.ToList();
        if (enumerable.Count == list.numItems)
        {
            _refreshSeachBox = 5f;
            return;
        }

        list.RemoveChildrenToPool();


        foreach (var key in enumerable)
        {
            var item = list.AddItemFromPool().asButton;
            item.text = $"{key}";
            item.onClick.Add(() =>
            {
                DialogAnalysis.StartDialogEvent(key);
                if (_recentDramaId.Count == _recentDramaId.Capacity)
                {
                    _recentDramaId.RemoveAt(0);
                }

                _recentDramaId.Add(key);
                RefreshDramaId();
            });
            item.cursor = FGUIManager.MOUSE_HAND;
        }
    }

    #endregion

    #region 指令调试相关

    private List<NextEventCharacter> _nextEventCharacters = new List<NextEventCharacter>();
    private List<NextEventCommand> _nextEventCommands = new List<NextEventCommand>();
    private List<NextEventOption> _nextEventOptions = new List<NextEventOption>();

    private void RefreshCommandList()
    {
        var commandList = MainView.m_command.m_commandList;
        foreach (var command in _nextEventCommands)
        {
            var item = (UI_ComboCommandItem)commandList.AddItemFromPool().asCom;
            item.SearchBox.SetItems(_commandKeys);
            item.SearchBox.SelectedIndex = Array.IndexOf(_commandKeys.ToArray(), command.Name);
            item.m_commandInput.m_inContent.text = command.Text;
            item.m_deleteButton.onClick.Add((() => _nextEventCommands.Remove(command)));
        }
    }

    #endregion

    protected override void OnHide()
    {
        base.OnHide();
        Instance = null;
    }

    private void OnClickClose(EventContext context)
    {
        Hide();
    }
}