
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils
{
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
            var gameObject = Object.Instantiate(GameObject);
            var flowchart = gameObject.GetComponentInChildren<Flowchart>();
            flowchart.StopAllBlocks();
            return Flowchart;
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
            if (!Flowcharts.TryGetValue(key,out  NextFlowchart nextFlowchart))
            {
                flowchart = GetFlowchart(key);
                return flowchart != null;
            }
            flowchart = nextFlowchart.GetFlowchart();
            return true;
        }
    }
}