using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{


    [DialogEnvQuery("GetTextRandomColor")]
    public class GetTextRandomColor : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var text = context.GetMyArgs(0, "");
            var size = context.GetMyArgs(1, 1);
            if (string.IsNullOrWhiteSpace(text) || size <= 0)
            {
                return text;
            }

            return FungusTextUtils.TextHelper(text, size, item => item.RandomColor());
        }
    }

  
}