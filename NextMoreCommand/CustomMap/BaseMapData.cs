using Newtonsoft.Json;

namespace SkySwordKill.NextMoreCommand.CustomMap
{
    public class BaseMapData
    {
        public const int MaxHigh = 15;
        public const int MaxWide = 13;   
        [JsonProperty("Name")]
        public string Name;
        [JsonProperty("ShouldReset")]
        public bool ShouldReset;
        [JsonProperty("NanDu")]
        public int NanDu;
        [JsonProperty("High")]
        public int High;
        [JsonProperty("Wide")]
        public int Wide;
        [JsonProperty("ID")]
        public int ID;
        public virtual CustomMap ToCustomMap()
        {
            return new CustomMap() { };
        }
    }
}