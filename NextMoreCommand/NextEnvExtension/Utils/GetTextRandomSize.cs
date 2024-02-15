using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{


    [DialogEnvQuery("GetTextRandomSize")]
    public class GetTextRandomSize : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var text  = context.GetMyArgs(0, "");
            var min   = context.GetMyArgs(1, 24);
            var max   = context.GetMyArgs(2, 32);
            var split = context.GetMyArgs(3, 0);
            if (string.IsNullOrWhiteSpace(text) || min <= 0 || max <= 0 || min > max)
            {
                return text;
            }

            if (split == 0)
            {
                return text.Size(Random.Range(min, max));
            }

            return FungusTextUtils.TextHelper(text, split, item => item.Size(Random.Range(min, max)));
        }
    }
}