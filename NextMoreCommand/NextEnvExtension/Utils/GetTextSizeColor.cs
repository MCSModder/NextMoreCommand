using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetTextSizeColor")]
    public class GetTextSizeColor : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var text  = context.GetMyArgs(0, "");
            var size  = context.GetMyArgs(1, 32);
            var color = context.GetMyArgs(2, "");
            var split = context.GetMyArgs(3, 0);
            var add   = context.GetMyArgs(4, 0);
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(color) || size <= 0)
            {
                return text;
            }

            if (split == 0 || add == 0)
            {
                return text.Size(size).Color(color.GetColor());
            }

            return FungusTextUtils.TextHelper(text, split, item =>
            {
                if ((size + add) != 0)
                {
                    size += add;
                }
                return item.Size(size).Color(color.GetColor());
            });
        }
    }

}