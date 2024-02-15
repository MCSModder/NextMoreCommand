using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using JSONClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.Patch;

namespace SkySwordKill.NextMoreCommand.Custom.NPC;

[JsonObject]
public class CustomNpcAvatar : CustomBase
{
    [JsonProperty("立绘")] public int Face { get; set; } = 0;

    [JsonProperty("战场形象")] public int FightFace { get; set; } = 0;


    [JsonProperty("种族")] public int AvatarType { get; set; } = 1;

    [JsonProperty("境界")] public int Level { get; set; } = 1;

    [JsonProperty("气血")] public int Hp { get; set; } = 30;

    [JsonProperty("遁速")] public int DunSu { get; set; } = 4;

    [JsonProperty("资质")] public int ZiZhi { get; set; } = 15;

    [JsonProperty("悟性")] public int WuXin { get; set; } = 15;

    [JsonProperty("神识")] public int ShengShi { get; set; } = 5;

    [JsonProperty("煞气")] public int ShaQi { get; set; } = 0;

    [JsonProperty("寿元")] public int ShouYuan { get; set; } = 100;

    [JsonProperty("出场年龄")] public int Age { get; set; } = 16;

    [JsonProperty("门派")] public string MenPai { get; set; } = string.Empty;

    [JsonProperty("武器")] public int EquipWeapon { get; set; }

    [JsonProperty("防具")] public int EquipClothing { get; set; }

    [JsonProperty("饰品")] public int EquipRing { get; set; }


    [JsonProperty("灵根权重")] public List<int> LingGen { get; set; } = new List<int>()
    {
        10,
        10,
        10,
        10,
        10
    };


    [JsonProperty("神通")] public List<int> Skills { get; set; } = new List<int>()
    {
        1,
        201,
        101,
        301,
        401,
        501,
        504
    };


    [JsonProperty("功法")] public List<int> StaticSkills { get; set; } = new List<int>()
    {
        1
    };


    [JsonProperty("元婴特有功法")] public int YuanYing { get; set; }

    [JsonProperty("化神领域")] public int HuaShenLingYu { get; set; }

    [JsonProperty("富有度")] public int MoneyType { get; set; } = 1;

    [JsonProperty("死亡是否刷新")] public bool _IsRefresh;


    [JsonProperty("战场掉落方式")] public int DropType { get; set; } = 1;

    [JsonProperty("是否参加拍卖")] public int CanjiaPaiMai { get; set; } = 0;

    [JsonProperty("悟道类型")] public int WudaoType { get; set; } = 2;

    [JsonProperty("感兴趣的物品大类型")] public int XinQuType { get; set; }

    [JsonProperty("是否固定价格出售物品")] public int Gudingjiage { get; set; }

    [JsonProperty("出售物品固定系数")] public int SellPercent { get; set; }


    [JsonProperty("拍卖分组")] public List<int> Paimaifenzu { get; set; } = new List<int>();


    [JsonIgnore]
    public int IsRefresh
    {
        get => _IsRefresh ? 1 : 0;
        set => _IsRefresh = value != 0;
    }

    [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)] public int Id { get; set; }

    [JsonProperty("称号", NullValueHandling = NullValueHandling.Ignore)] public string Title { get; set; } = string.Empty;

    [JsonProperty("姓氏", NullValueHandling = NullValueHandling.Ignore)] public string FirstName { get; set; } = string.Empty;

    [JsonProperty("名字", NullValueHandling = NullValueHandling.Ignore)] public string Name { get; set; } = string.Empty;

    [JsonIgnore] private SexType _sexType;

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

    public void SetLingGen(int gold = 10, int wood = 10, int water = 10, int fire = 10, int earth = 10)
    {
        LingGen = new List<int>()
        {
            gold,
            wood,
            water,
            fire,
            earth
        };
    }
    public override JObject ToJObject()
    {
        var avatar = new AvatarJsonData()
        {
            age = Age,
            AvatarType = AvatarType,
            id = Id,
            face = Face,
            fightFace = FightFace,
            SexType = SexType,
            Level = Level,
            HP = Hp,
            dunSu = DunSu,
            ziZhi = ZiZhi,
            wuXin = WuXin,
            shengShi = ShengShi,
            shaQi = ShaQi,
            shouYuan = ShouYuan,
            equipWeapon = EquipWeapon,
            equipClothing = EquipClothing,
            equipRing = EquipRing,
            yuanying = YuanYing,
            HuaShenLingYu = HuaShenLingYu,
            MoneyType = MoneyType,
            IsRefresh = IsRefresh,
            dropType = DropType,
            canjiaPaiMai = CanjiaPaiMai,
            wudaoType = WudaoType,
            XinQuType = XinQuType,
            gudingjiage = Gudingjiage,
            sellPercent = SellPercent,
            Title = Title,
            FirstName = FirstName,
            Name = Name,
            menPai = MenPai,
            LingGen = LingGen,
            skills = Skills,
            staticSkills = StaticSkills,
            paimaifenzu = Paimaifenzu,
        };
        var obj = JObject.FromObject(avatar);
        obj.Add("zizhi",  ZiZhi);
        obj.Add("wuxing", WuXin);
        return obj;
    }
}