using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    public class GetNpcDataJson
    {
        public GetNpcDataJson(int id)
        {
            Id = id.ToNpcNewId();
        }
        public int Id { get; }
        public JSONObject AvatarJsonData => jsonData.instance.AvatarJsonData[Id.ToString()];
        public string Str => AvatarJsonData?.ToString(true);

    }
    [DialogEnvQuery("GetNpcData")]
    [DialogEnvQuery("获取NPC捏脸数据")]
    public class GetNpcData : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npcId = context.GetNpcID(0, 0);
            return jsonData.instance.AvatarJsonData.HasField(npcId.ToString()) ? new GetNpcDataJson(npcId) : null;
        }
    }
}