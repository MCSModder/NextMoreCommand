using Newtonsoft.Json;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.CustomMapType
{
    [CustomMapType(nameof(NextTrigger),"Next触发器")]
    public class NextTrigger:ModCustomMapType
    {
        public override string Type => "NextTrigger";
        [JsonProperty] public string[] DialogTriggerType { get; set; }

        public override void Execute()
        {
            DialogAnalysis.TryTrigger(DialogTriggerType, new DialogEnvironment(), true);
        }
    }
}