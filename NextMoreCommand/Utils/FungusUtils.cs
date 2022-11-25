using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Fungus;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public class StopPlayDialog : MonoBehaviour
    {
        public Flowchart flowchart;
        private GameObject _sayDialog;
     
        private void Start()
        {
            _sayDialog = GameObject.Find("SayDialog");
            //_sayDialog.GetComponent<SayDialog>().Stop();
            SayDialog.GetSayDialog().Stop();
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
            Tools.instance.IsCanLoadSetTalk = false;
            Tools.instance.isNeedSetTalk = true;
            var gameObject = Object.Instantiate(GameObject);
            var flowchart = gameObject.GetComponentInChildren<Flowchart>();
            gameObject.AddComponent<StopPlayDialog>().flowchart = flowchart;
            SayDialog.GetSayDialog().Stop();
            return flowchart;
        }
    }

    public static class FungusUtils
    {
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
                return gameObject.GetComponentInChildren<Flowchart>();
            }

            if (Flowcharts.ContainsKey(key))
            {
                return Flowcharts[key].GetFlowchart();
            }

            gameObject = Resources.Load<GameObject>($"talkPrefab/TalkPrefab/{key}");
            if (gameObject != null)
            {
                var nextFlowchart = new NextFlowchart(gameObject.GetComponentInChildren<Flowchart>());
                Flowcharts.Add(nextFlowchart.Name, nextFlowchart);
                return nextFlowchart.GetFlowchart();
            }

            return null;
        }

        public static Flowchart GetTalk(int taskID) => GetFlowchart($"Talk{taskID.ToString()}");

        public static bool TryGetTalk(int taskID, out Flowchart flowchart) =>
            TryGetFlowchart($"Talk{taskID.ToString()}", out flowchart);

        public static bool TryGetFlowchart(string key, out Flowchart flowchart)
        {
            flowchart = GetFlowchart(key);
            Tools.instance.IsCanLoadSetTalk = true;
            Tools.instance.isNeedSetTalk = false;
            return flowchart != null;
        }
    }
}