using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Fight;

[DialogEnvQuery("GetFightMonsterId")]
[DialogEnvQuery("获得敌方编号")]
public class GetFightMonsterId : IDialogEnvQuery
{
    public object Execute(DialogEnvQueryContext context)
    {
        return Tools.instance.MonstarID;
    }
}