using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class JsonUtils
{
    public static JObject ToJObject(this JSONObject jsonObject)
    {
        return JObject.Parse(jsonObject.ToString());
    }

    public static JArray ToJArray(this JSONObject jsonObject)
    {
        return JArray.Parse(jsonObject.ToString());
    }
}