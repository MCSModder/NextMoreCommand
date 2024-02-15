using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetTextColor")]
    public class GetTextColor : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var text  = context.GetMyArgs(0, "");
            var color = context.GetMyArgs(1, "");
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(color))
            {
                return text;
            }

            return text.Color(color.GetColor());
        }
    }
}