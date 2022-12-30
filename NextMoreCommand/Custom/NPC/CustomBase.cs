using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.Custom.NPC;

public abstract class CustomBase
{
    public abstract JObject ToJObject();
}