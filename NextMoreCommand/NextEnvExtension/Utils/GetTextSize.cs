using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetTextSize")]
    public class GetTextSize : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var text = context.GetMyArgs(0, "");
            var size = context.GetMyArgs(1, 32);
            var split = context.GetMyArgs(2, 0);
            var add = context.GetMyArgs(3, 0);
            if (string.IsNullOrWhiteSpace(text) || size <= 0)
            {
                return text;
            }

            if (split == 0 || add == 0)
            {
                return text.Size(size);
            }

            return FungusTextUtils.TextHelper(text, split, item =>
            {
                size += add;
                return item.Size(size);
            });
        }
    }

}