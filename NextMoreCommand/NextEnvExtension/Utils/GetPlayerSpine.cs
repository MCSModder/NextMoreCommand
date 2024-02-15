using System.Collections.Generic;
using System.Linq;
using MCSSubscribeDependencies;
using script.Steam;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetPlayerSpine")]
    public class GetPlayerSpine : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var spine     = DialogAnalysis.GetStr("PLAYER_SPINE");
            var spineSkin = DialogAnalysis.GetStr("PLAYER_SPINE_SKIN");

            return new
            {
                Name = spine,
                Skin = spineSkin,
                IsDefault = spineSkin == "default"
            };
        }
    }
}