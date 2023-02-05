using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
   

    [DialogEnvQuery("GetTextRandomSizeColor")]
    public class GetTextRandomSizeColor : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var text = context.GetMyArgs(0, "");
            var min = context.GetMyArgs(1, 24);
            var max = context.GetMyArgs(2, 36);
            var size = context.GetMyArgs(3, 0);
            if (string.IsNullOrWhiteSpace(text) ||  min <= 0 || max <= 0 || min > max)
            {
                return text;
            }

            if (size == 0)
            {
                return text.RandomColor().Size(Random.Range(min,max));
            }
            return FungusTextUtils.TextHelper(text, size, item => item.RandomColor().Size(Random.Range(min, max)));
        }
    }
}