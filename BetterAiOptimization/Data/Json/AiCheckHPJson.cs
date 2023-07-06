using System;
using BetterAiOptimization.Data.Common;
using Newtonsoft.Json;

namespace BetterAiOptimization.Data.Json
{
    [JsonObject]
    public class AiCheckHpData : IJsonData, IJsonTarget, IJsonOperator
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("value1")]
        public int Hp;
        [JsonProperty("panduan1")]
        public string Target = string.Empty;
        [JsonProperty("panduan2")]
        public string Operator = string.Empty;
        [JsonProperty("Yes")]
        public int Yes;
        [JsonProperty("No")]
        public int No;
        public int GetId()
        {
            return Id;
        }
        public int GetResult(bool isMatch)
        {
            return isMatch ? Yes : No;
        }
        public string GetTarget()
        {
            return Target;
        }
        public string GetOperator()
        {
            return Operator;
        }
    }

    [JsonManager("2")]
    public class AiCheckHpJson : BaseAiJsonData<AiCheckHpData>
    {

        public override void InitDataDict()
        {
            OnInitFinish();
        }
    }

}