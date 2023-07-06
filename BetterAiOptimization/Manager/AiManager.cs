using System.Collections.Generic;
using BetterAiOptimization.Data;
using BetterAiOptimization.Data.Json;
using BetterAiOptimization.Realize;

namespace BetterAiOptimization.Manager
{
    public static class AiManager
    {
        public static readonly Dictionary<string, IAiRealize> AiRealizeDict = new Dictionary<string, IAiRealize>();
        public static readonly Dictionary<string, IJsonManager> AiJsonDict = new Dictionary<string, IJsonManager>();

        public static void InitDict()
        {
            foreach (var aiManager in AiJsonDict)
            {
                aiManager.Value.InitDataDict();
            }
        }
        public static void AiRealizeType(int seid, AiSkillData aiSkillData) => AiRealizeType(seid.ToString(), aiSkillData);
        public static void AiRealizeType(string seid, AiSkillData aiSkillData)
        {
            if (AiRealizeDict.TryGetValue(seid, out var aiRealize))
            {
                aiRealize.Execute(aiSkillData);
            }
        }
        public static bool TryGetAiRealizeJson<T>(string seid, int id, out T data) where T : IJsonData
        {
            data = default;
            return AiJsonDict.TryGetValue(seid, out var manager) && manager.TryGetData(id, out data);
        }

    }
}