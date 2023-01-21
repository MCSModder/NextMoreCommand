using System.Collections.Generic;
using System.Linq;
using JSONClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.Custom.NPC;

public static class ExtendsMethods
{
    public static string IsNull(this string text, string defalutValue)
    {
        return string.IsNullOrEmpty(text) ? defalutValue : text;
    }

    public static int IsZero(this int num, int defalutValue)
    {
        return num == 0 ? defalutValue : num;
    }
}

public class CustomNpc
{
    public readonly static Dictionary<string, CustomNpc> CustomNpcs = new Dictionary<string, CustomNpc>();
    public static CustomNpc GetNpc(string npc) => CustomNpcs[npc];

    [JsonProperty("角色Id", Order = 0)]
    [JsonRequired]
    public int Id { get; set; }

    [JsonProperty("称号", Order = 1)]
    [JsonRequired]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("姓氏", Order = 2)] public string FirstName { get; set; } = string.Empty;
    [JsonProperty("名字", Order = 3)] public string Name { get; set; } = string.Empty;

    [JsonProperty("性别", Order = 4)] [JsonRequired]
    private SexType _sexType = NPC.SexType.男;


    [JsonIgnore]
    public int SexType
    {
        get => (int)_sexType;
        set
        {
            switch ((NPC.SexType)value)
            {
                case NPC.SexType.男:
                case NPC.SexType.女:
                case NPC.SexType.不男不女:
                    // ReSharper disable once HeapView.BoxingAllocation
                    _sexType = (NPC.SexType)value;
                    break;
                default:
                    _sexType = NPC.SexType.男;
                    break;
            }
        }
    }

    [JsonProperty("角色属性", Order = 5)]
    public List<CustomNpcAvatar> CustomNpcAvatar { get; set; } = new List<CustomNpcAvatar>();

    [JsonProperty("固定角色", Order = 6, NullValueHandling = NullValueHandling.Ignore)]
    public CustomNpcImportantDate CustomNpcImportantDate { get; set; }

    [JsonProperty("武将绑定", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
    public List<CustomWujiang> CustomWujiang { get; set; }

    [JsonProperty("角色背包", Order = 8, NullValueHandling = NullValueHandling.Ignore)]
    public CustomBackPack CustomBackPack;

    public void Init()
    {
        ToAvatarJsonDataList();
        ToBackPack();
        ToWuJiangBindingList();
        ToNpcImportantDate();
    }

    public List<CustomNpcAvatar> ToAvatarJsonDataList()
    {
        var list = new List<CustomNpcAvatar>();
        for (int i = 0; i < CustomNpcAvatar.Count; i++)
        {
            var avatar = CustomNpcAvatar[i];
            avatar.Id = Id + i;
            avatar.Title = avatar.Title.IsNull(Title);
            avatar.FirstName = avatar.FirstName.IsNull(FirstName);
            avatar.Name = avatar.Name.IsNull(Name);
            avatar.SexType = SexType;
            list.Add(avatar);
        }

        return list;
    }

    public JObject ToAvatarJsonData()
    {
        if (CustomNpcAvatar.Count == 0) return null;
        var avatar = CustomNpcAvatar[0];
        avatar.Id = Id;
        avatar.Title = avatar.Title.IsNull(Title);
        avatar.FirstName = avatar.FirstName.IsNull(FirstName);
        avatar.Name = avatar.Name.IsNull(Name);
        avatar.SexType = SexType;
        return avatar.ToJObject();
    }

    public JObject ToNpcImportantDate()
    {
        if (CustomNpcImportantDate == null) return null;
        CustomNpcImportantDate.Id = Id;
        CustomNpcImportantDate.SexType = SexType;
        return CustomNpcImportantDate.ToJObject();
    }

    public List<CustomWujiang> ToWuJiangBindingList()
    {
      
        var customWujiangs = new List<CustomWujiang>();
        if (CustomWujiang == null ||CustomWujiang .Count == 0) return customWujiangs;
        var id = CustomWujiang[0].Id.IsZero(Id);
      
        var list = ToAvatarJsonDataList().Select(i => i.Id);
        for (int i = 0; i < CustomWujiang .Count; i++)
        {
            var customWujiang= CustomWujiang[i];
            customWujiang.Id = id + i;
            customWujiang.Title = customWujiang.Title.IsNull(Title);
            customWujiang.Name = customWujiang.Name.IsNull(Name);
            customWujiang.Image = customWujiang.Image.IsZero(Id);
            customWujiang.PaiMaiHang = customWujiang.PaiMaiHang.IsZero(Id);
            customWujiang.TimeStart = customWujiang.TimeStart.IsNull("0001-01-01");
            customWujiang.TimeEnd = customWujiang.TimeEnd.IsNull("5000-12-30");
            
            if (customWujiang.Avatar.Count == 0)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                customWujiang.Avatar = list.ToList();
            }
            customWujiangs.Add(customWujiang);
        }

        return customWujiangs;
    }

    public CustomWujiang ToWuJiangBinding()
    {
        if (CustomWujiang == null ||CustomWujiang .Count == 0) return null;
        var customWujiang= CustomWujiang[0];
        customWujiang.Id = customWujiang.Id.IsZero(Id);
        customWujiang.Title = customWujiang.Title.IsNull(Title);
        customWujiang.Name = customWujiang.Name.IsNull(Name);
        customWujiang.Image = customWujiang.Image.IsZero(Id);
        customWujiang.PaiMaiHang = customWujiang.PaiMaiHang.IsZero(Id);
        customWujiang.TimeStart = customWujiang.TimeStart.IsNull("0001-01-01");
        customWujiang.TimeEnd = customWujiang.TimeEnd.IsNull("5000-12-30");
        var list = ToAvatarJsonDataList();
        if (customWujiang.Avatar.Count == 0)
        {
            foreach (var item in list)
            {
                customWujiang.Avatar.Add(item.Id);
            }
        }


        return customWujiang;
    }


    public JObject ToBackPack()
    {
        if (CustomBackPack == null) return null;
        CustomBackPack.Id = CustomBackPack.Id.IsZero(Id);
        CustomBackPack.AvatarId = CustomBackPack.AvatarId.IsZero(Id);
        CustomBackPack.BackpackName = CustomBackPack.BackpackName.IsNull(Name);
        return CustomBackPack.ToJObject();
    }

    public override string ToString()
    {
        return JObject.FromObject(this).ToString(Formatting.Indented);
    }
}