using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    [DialogEnvQuery("HasNpcFace")]
    [DialogEnvQuery("检测角色捏脸")]
    public class HasNpcFace : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, -1);
            var face = context.GetMyArgs(1, -1);
            var lihui = context.GetMyArgs(2, "");
            var data = NpcJieSuanManager.inst.GetNpcData(npc);
            if (npc > 0 && data.HasField("workshoplihui"))
            {
                return data.GetField("face").I == face && data.GetField("workshoplihui").Str == lihui;
            }
            return false;
        }
    }
}