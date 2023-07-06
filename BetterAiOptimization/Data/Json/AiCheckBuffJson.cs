using System;
using BetterAiOptimization.Data.Common;
using Newtonsoft.Json;

namespace BetterAiOptimization.Data.Json
{

    [JsonObject]
    public class AiCheckBuffData : IJsonData,IJsonTarget,IJsonOperator
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("value1")]
        public int BuffId;
        [JsonProperty("value2")]
        public int BuffNum;
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

    [JsonManager("1")]
    public class AiCheckBuffJson : BaseAiJsonData<AiCheckBuffData>
    {

        public override void InitDataDict()
        {
            OnInitFinish();
        }
    }

}