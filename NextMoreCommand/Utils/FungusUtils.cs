using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fungus;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public class PlayFlowchart : MonoBehaviour
    {
        private GameObject go;
        private Flowchart _flowchart;

        private void Awake()
        {
            go = transform.Find("Flowchart").gameObject;
            _flowchart = go.GetComponent<Flowchart>();
            _flowchart.enabled = false;
        }

        // ReSharper disable once Unity.IncorrectMethodSignature
        async void Update()
        {
            if (FungusUtils.isTalkActive)
            {
                if (!go.activeSelf)
                {
                    go.SetActive(true);
                    await System.Threading.Tasks.Task.Delay(1000);
                    _flowchart.enabled = true;
                }

                var result = FungusUtils.TalkFunc?.Invoke(_flowchart);
                if (result != null && (bool)result)
                {
                    FungusUtils.TalkOnComplete?.Invoke();
                }
                else
                {
                    FungusUtils.TalkOnFailed?.Invoke();
                }

                FungusUtils.isTalkActive = false;
                FungusUtils.TalkFunc = null;
                FungusUtils.TalkOnComplete = null;
                FungusUtils.TalkOnFailed = null;
                FungusUtils.TalkItemId = -1;
                FungusUtils.TalkBlockName = string.Empty;
                Destroy(this);
            }
        }
    }

    public class NextFlowchart
    {
        public GameObject GameObject => Flowchart.transform.parent.gameObject;
        public Flowchart Flowchart;
        public string Name => Flowchart.GetParentName() ?? Flowchart.gameObject.name;

        public NextFlowchart(Flowchart flowchart)
        {
            Flowchart = flowchart;
        }

        public Flowchart GetFlowchart()
        {
            var go = GameObject.transform.Find("Flowchart").gameObject;
            go.SetActive(false);
            var gameObject = Object.Instantiate(GameObject);
            var flowchart = gameObject.GetComponentInChildren<Flowchart>();
            gameObject.AddComponent<PlayFlowchart>();
            go.SetActive(true);
            return flowchart;
        }
    }

    public static class FungusUtils
    {
        public static bool isTalkActive = false;
        public static Func<Flowchart, bool> TalkFunc;
        public static Action TalkOnComplete;
        public static Action TalkOnFailed;
        public static string TalkBlockName;
        public static int TalkItemId;
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

        public static Flowchart GetFlowchart(string key)
        {
            GameObject gameObject = GameObject.Find($"{key}(Clone)");

            if (gameObject != null)
            {
                if (gameObject.GetComponent<PlayFlowchart>() == null)
                {
                    gameObject.AddComponent<PlayFlowchart>();
                }

                return gameObject.GetComponentInChildren<Flowchart>();
            }

            if (Flowcharts.ContainsKey(key))
            {
                Flowcharts[key].GetFlowchart();
                return Flowcharts[key].Flowchart;
            }

            gameObject = Resources.Load<GameObject>($"talkPrefab/TalkPrefab/{key}");
            if (gameObject != null)
            {
                var nextFlowchart = new NextFlowchart(gameObject.GetComponentInChildren<Flowchart>());
                Flowcharts.Add(nextFlowchart.Name, nextFlowchart);
                nextFlowchart.GetFlowchart();
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