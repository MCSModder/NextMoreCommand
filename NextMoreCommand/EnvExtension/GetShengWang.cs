using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension;
[DialogEnvQuery("GetShengWang")]
public class GetShengWang:IDialogEnvQuery
{
    public object Execute(DialogEnvQueryContext context)
    {
        var id = context.GetMyArgs(0, 0);
        return PlayerEx.GetShengWang(id);
    }
}