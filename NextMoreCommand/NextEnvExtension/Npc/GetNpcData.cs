using System;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

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
        public UINPCJiaoHu UiNpcJiaoHu => UINPCJiaoHu.Inst;
        public int Hp
        {
            get => GetInt("HP");
            set => SetField("HP", value);
        }

   #region 方法

        public void RefreshJiaoHu()
        {
            var jiaoHu = UiNpcJiaoHu;
            if (!jiaoHu.NowIsJiaoHu || jiaoHu.NowJiaoHuNPC?.ID != Id) return;
            jiaoHu.NowJiaoHuNPC?.RefreshData();
            if (jiaoHu.InfoPanel.gameObject.activeSelf)
            {
                jiaoHu.InfoPanel.RefreshUI();
            }
        }
        public void SetField(string name, int value)
        {
            AvatarJsonData?.SetField(name, value);
            RefreshJiaoHu();
        }
        public void SetField(string name, string value)
        {
            AvatarJsonData?.SetField(name, value);
            RefreshJiaoHu();
        }
        public JSONObject GetField(string name)
        {
            return AvatarJsonData?.TryGetField(name);
        }
        public int GetInt(string name)
        {
            return GetField(name)?.I ?? 0;
        }
        public string GetStr(string name)
        {
            return GetField(name)?.Str ?? string.Empty;
        }

  #endregion
    }

    [DialogEnvQuery("GetNpcData")]
    [DialogEnvQuery("获取角色数据")]
    public class GetNpcData : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npcId = context.GetNpcID(0, 0);
            return jsonData.instance.AvatarJsonData.HasField(npcId.ToString()) ? new GetNpcDataJson(npcId) : null;
        }
    }
}