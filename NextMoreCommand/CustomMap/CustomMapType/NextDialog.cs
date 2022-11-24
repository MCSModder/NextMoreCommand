using Newtonsoft.Json;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.CustomMapType
{
    [CustomMapType(nameof(NextDialog), "Next剧情")]
    public class NextDialog : ModCustomMapType
    {
        public override string Type => "NextDialog";
        [JsonProperty] public string DialogEventID { get; set; }
        [JsonProperty] public string Condition { get; set; }

        public override void Execute()
        {
            var env = new DialogEnvironment();
            if (DialogAnalysis.CheckCondition(Condition, env))
            {
                

                DialogAnalysis.StartDialogEvent(DialogEventID, env);
            }
        }
    }
}