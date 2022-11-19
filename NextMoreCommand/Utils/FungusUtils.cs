using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fungus;
using HarmonyLib;
using Newtonsoft.Json;
using SkySwordKill.Next;
using SkySwordKill.Next.FCanvas;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class FungusUtils
    {
        public struct NextFlowchart
        {
            public GameObject GameObject =>Flowchart.transform.parent.gameObject;
            public Flowchart Flowchart;
            public string Name => Flowchart.GetParentName() ?? Flowchart.gameObject.name;
            
            public NextFlowchart(Flowchart flowchart)
            {
                Flowchart = flowchart;
            }
        }
        public static bool isActive = false;
        private static readonly Dictionary<string, NextFlowchart> _flowcharts = new Dictionary<string, NextFlowchart>();

        public static Dictionary<string, NextFlowchart> Flowcharts => _flowcharts;

        public static void InitFlowchart()
        {
            if (_flowcharts.Count != 0) return;
            var go = Resources.LoadAll<GameObject>("").Where(gameObject => gameObject.GetComponentsInChildren<Flowchart>().Length >= 1 );
            var list = new List<NextFlowchart>();
            foreach (var gameObject in go)
            {
                try
                {
                    var flowcharts = gameObject.gameObject.GetComponentsInChildren<Flowchart>();
                    foreach (var flowchart in flowcharts)
                    {
                        var nextFlowchart = new NextFlowchart(flowchart);
                        if (!Flowcharts.ContainsKey(nextFlowchart.Name))
                        {
                            Main.LogInfo($"添加 {nextFlowchart.Name} 到 FungusUtils.Flowcharts");
                            Flowcharts.Add(nextFlowchart.Name,nextFlowchart);
                        }
                        else
                        {
                            list.Add(nextFlowchart);
                        }
                       
                    }
                    
                
                }
                catch (Exception e)
                {
                    Main.LogError(e);
                }
            }

            foreach (var nextFlowchart in list)
            {
                Main.LogInfo($"已重复存在 {nextFlowchart.Name} 到 FungusUtils.Flowcharts");
            }
            Main.LogInfo($"重复:List {list.Count.ToString()} FungusUtils.Flowcharts:{Flowcharts.Count.ToString()}");
            Resources.UnloadUnusedAssets();

        }

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
            TryGetFlowchart(key, out Flowchart flowchart);
            return flowchart;
        }

        public static bool TryGetFlowchart(string key, out Flowchart flowchart)
        {
            if (!TryGetFlowchart(key, out flowchart, out _))
            {
                flowchart = null;
                return false;
            }

            return true;
        }

        public static bool TryGetFlowchart(string key, out Flowchart flowchart, out GameObject gameObject)
        {
            if (!Flowcharts.TryGetValue(key, out NextFlowchart nextFlowchart))
            {
                flowchart = null;
                gameObject = null;
                return false;
            }

            gameObject = GameObject.Find($"{key}(Clone)") ?? Object.Instantiate(nextFlowchart.GameObject);
            flowchart = gameObject.GetComponentInChildren<Flowchart>();
            flowchart.StopAllBlocks();
            return true;
        }
    }
}