using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.DialogTrigger;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight
{

    [DialogEnvQuery("GetCurrentSkill")]
    [DialogEnvQuery("获得当前神通")]
    public class GetCurrentSkill : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {


            return OnUseSkill.NowSkill;
        }
    }
}