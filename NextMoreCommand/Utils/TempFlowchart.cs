using System.Collections.Generic;
using Fungus;
using JetBrains.Annotations;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public class TempFlowchart
    {
        private static Dictionary<string, Flowchart> m_flowcharts = new Dictionary<string, Flowchart>();

        public static Dictionary<string, Flowchart> Flowcharts
        {
            get => m_flowcharts;
        }

        public static void SetFlowchart(string key, Flowchart flowchart)
        {
            if (flowchart == null) return;
            if (HasFlowchart(key) && Flowcharts[key] != flowchart)
            {
                Flowcharts[key] = flowchart;
            }
            else
            {
                Flowcharts.Add(key, flowchart);
            }
        }

        public static bool HasFlowchart(string key)
        {
            return Flowcharts.ContainsKey(key);
        }

        [CanBeNull]
        public static Flowchart GetFlowchart(string key)
        {
            if (!HasFlowchart(key)) return null;

            var result = Flowcharts[key];
            Flowcharts.Remove(key);

            return result;
        }
    }
}