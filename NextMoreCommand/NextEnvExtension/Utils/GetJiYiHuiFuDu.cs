using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetJiYiHuiFuDu")]
    public class GetJiYiHuiFuDu:IDialogEnvQuery
    {


        public object Execute(DialogEnvQueryContext context)
        {
            return context.Env.player.jianLingManager.GetJiYiHuiFuDu();
        }
    }
}