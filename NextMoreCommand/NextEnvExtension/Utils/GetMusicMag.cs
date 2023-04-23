using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using YSGame;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetMusicMag")]
    public class GetMusicMag : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return MusicMag.instance;
        }
    }
}