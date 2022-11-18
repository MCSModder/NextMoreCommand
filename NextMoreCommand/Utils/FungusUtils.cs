using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using Newtonsoft.Json;
using SkySwordKill.Next;
using SkySwordKill.Next.FCanvas;
using UnityEngine;
using Object =UnityEngine.Object;
namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class FungusUtils
    {
        private static readonly Dictionary<string, GameObject> _flowcharts = new Dictionary<string, GameObject>();

        public static Dictionary<string, GameObject> Flowcharts
        {
            get
            {
                if (_flowcharts.Count == 0)
                {
                    InitFlowchart();
                }

                return _flowcharts;
            }
        }

        public static void InitFlowchart()
        {
            if (_flowcharts.Count != 0) return;
            
            var go = Resources.LoadAll<GameObject>("");
            List<string> list = new List<string>();
            foreach (var gameObject in go)
            {
                var flowchart =gameObject.GetComponentInChildren<Flowchart>();
                if (flowchart == null)
                    continue;

                try
                {
                    var name = flowchart.ConvertToFFlowchart().Name;
                    if (_flowcharts.ContainsKey(name))
                    {
                        list.Add(name);
                    }
                    else
                    {
                        _flowcharts.Add(name, gameObject);
                        Main.LogInfo($"添加{name} 到 FungusUtils.Flowcharts");
                    }
                  
                 
                }
                catch (Exception e)
                {
                    Main.LogError(e);
                }

               
            }
            foreach (var name in list)
            {
                Main.LogInfo($"已重复存在{name} 到 FungusUtils.Flowcharts");
            }
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

        public static bool TryGetFlowchart(string key, out Flowchart flowchart)
        {
           var go = GameObject.Find($"{key}(Clone)");
            if (go != null && go.GetComponentInChildren<Flowchart>() != null)
            {
                flowchart = go.GetComponentInChildren<Flowchart>();
                return true;
            }
            if (!Flowcharts.TryGetValue(key, out GameObject gameObject))
            {
                flowchart = null;
                return false;
            }

            flowchart =Object.Instantiate(gameObject).GetComponentInChildren<Flowchart>();
            flowchart.StopAllBlocks();

            return true;
        }
    }
}
