﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fungus;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.Patch;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public class PlayFlowchart : MonoBehaviour
    {
        private GameObject _go;
        private Flowchart  _flowchart;
        private bool       _isFlowchartNull;


        private void Awake()
        {
            _go = transform.Find("Flowchart").gameObject;
            _flowchart = _go.GetComponentInChildren<Flowchart>();
            _flowchart.enabled = false;
        }

        private async void Start()
        {
            _isFlowchartNull = _flowchart == null;
            if (_isFlowchartNull)
            {
                DestroyImmediate(this);
                return;
            }

            if (FungusUtils.IsTalkActive)
            {
                await RunFungus();
            }
        }

        public async UniTask RunFungus()
        {
            if (!_go.activeSelf) _go.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(0.8));
            _flowchart.enabled = true;
            var result = FungusUtils.TalkFunc?.Invoke(_flowchart);
            if (result != null && (bool)result)
            {
                FungusUtils.TalkOnComplete?.Invoke();
            }
            else
            {
                FungusUtils.TalkOnFailed?.Invoke();
            }

            DestroyImmediate(this);
        }

        private void OnDestroy()
        {
            FungusUtils.IsTalkActive = false;
            FungusUtils.TalkFunc = null;
            FungusUtils.TalkOnComplete = null;
            FungusUtils.TalkOnFailed = null;
            FungusUtils.TalkItemId = -1;
            FungusUtils.TalkBlockName = string.Empty;
        }
    }

    public class NextFlowchart
    {
        public GameObject GameObject => Flowchart.transform.parent.gameObject;
        public Flowchart  Flowchart;
        public string     Name => Flowchart.GetParentName() ?? Flowchart.gameObject.name;

        public NextFlowchart(Flowchart flowchart)
        {
            Flowchart = flowchart;
        }

        public Flowchart GetFlowchart()
        {
            Main.FPatch.PatchFlowchart(Flowchart);
            var go = GameObject.transform.Find("Flowchart").gameObject;
            go.SetActive(false);
            var flowchart = GameObject.GetComponentInChildren<Flowchart>();
            var gameObject = Object.Instantiate(GameObject);
            gameObject.AddComponent<PlayFlowchart>();
            go.SetActive(true);
            return flowchart;
        }
    }

    public static class FungusUtils
    {
        public static bool                              IsTalkActive = false;
        public static Func<Flowchart, bool>             TalkFunc;
        public static Action                            TalkOnComplete;
        public static Action                            TalkOnFailed;
        public static string                            TalkBlockName;
        public static int                               TalkItemId;
        public static Dictionary<string, NextFlowchart> Flowcharts { get; } = new Dictionary<string, NextFlowchart>();

        public static int FindIndex(this Flowchart flowchart, string tagBlock, int itemId, out Block block)
        {
            block = flowchart.FindBlock(tagBlock);
            if (block == null || itemId < 0) return -1;
            foreach (var command in block.commandList)
            {
                if (command.ItemId == itemId) return command.CommandIndex;
            }

            return -1;
        }

        public static Flowchart[] FlowchartsArray => Object.FindObjectsOfType<Flowchart>();
        public static Flowchart GetFlowchart(string key)
        {
            GameObject gameObject     = null;
            var        flowchartArray = FlowchartsArray;

            foreach (var flowchart in flowchartArray)
            {
                if (!flowchart.GetParentName().StartsWith(key)) continue;
                gameObject = flowchart.transform.parent.gameObject;
                if (gameObject.GetComponent<PlayFlowchart>() == null)
                {
                    gameObject.AddComponent<PlayFlowchart>();
                }
                return flowchart;
            }
            gameObject = GameObject.Find(key) ?? GameObject.Find(key + "(Clone)");
            if (gameObject != null)
            {
                if (gameObject.GetComponent<PlayFlowchart>() == null)
                {
                    gameObject.AddComponent<PlayFlowchart>();
                }

                MyPluginMain.LogInfo("GameObject.Find");
                return gameObject.GetComponentInChildren<Flowchart>();
            }

            if (Flowcharts.ContainsKey(key))
            {
                Flowcharts[key].GetFlowchart();
                MyPluginMain.LogInfo("Flowcharts[key].GetFlowchart();");
                return Flowcharts[key].Flowchart;
            }

            gameObject = Resources.Load<GameObject>($"talkPrefab/TalkPrefab/{key}");
            if (gameObject != null)
            {
                var nextFlowchart = new NextFlowchart(gameObject.GetComponentInChildren<Flowchart>());
                Flowcharts.Add(nextFlowchart.Name, nextFlowchart);
                nextFlowchart.GetFlowchart();
                MyPluginMain.LogInfo($"Resources.Load<GameObject>(talkPrefab/TalkPrefab/{key});");
                return nextFlowchart.Flowchart;
            }

            return null;
        }

        public static Flowchart GetTalk(int taskID) => GetFlowchart($"Talk{taskID.ToString()}");

        public static bool TryGetTalk(int taskID, out Flowchart flowchart) =>
            TryGetFlowchart($"Talk{taskID.ToString()}", out flowchart);

        public static bool TryGetFlowchart(string key, out Flowchart flowchart)
        {
            flowchart = GetFlowchart(key);
            return flowchart != null;
        }
    }
}