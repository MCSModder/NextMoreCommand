using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension;
[DialogEnvQuery("GetShengWangByName")]
public class GetShengWangByName:IDialogEnvQuery
{
    public object Execute(DialogEnvQueryContext context)
    {
        var type = context.GetMyArgs(0, "宁州");
        var id = type switch
        {
            "海域"=>19,
            "龙族"=>23,
            "宗门" => PlayerEx.Player.menPai,
            "白帝楼" => 24,
            "风雨楼" => 10,
            _ => 0
        }; 
        return PlayerEx.GetShengWang(id);
    }
}