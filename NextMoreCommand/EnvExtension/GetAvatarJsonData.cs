using System;
using System.Linq;
using JSONClass;
using Newtonsoft.Json;
using SkySwordKill.Next.DialogSystem;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension;

public partial class AvatarJsonData
{
    [JsonIgnore] private JSONObject AvatarRandomJsonData => jsonData.instance.AvatarRandomJsonData[Id];
    [JsonIgnore] public int Id { get; set; }

    [JsonIgnore]
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            AvatarRandomJsonData.SetField("Name", value);
        }
    }

    [JsonIgnore]
    public string BirthdayTime
    {
        get => _birthdayTime;
        set
        {
            _birthdayTime = value;
            AvatarRandomJsonData.SetField("BirthdayTime", value);
        }
    }

    // ReSharper disable once HeapView.BoxingAllocation
    public void SetBirthdayTime(int year, int mouth, int day) =>
        BirthdayTime = $"{year:0000}-{mouth.ToString()}-{day.ToString()}";

    [JsonIgnore]
    public int Sex
    {
        get => _sex;
        set
        {
            _sex = value;
            AvatarRandomJsonData.SetField("Sex", value);
        }
    }

    [JsonIgnore]
    public int Feature
    {
        get => _feature;
        set
        {
            _feature = value;
            AvatarRandomJsonData.SetField("feature", value);
        }
    }

    [JsonIgnore]
    public int YanYing
    {
        get => _yanying;
        set
        {
            _yanying = value;
            AvatarRandomJsonData.SetField("yanying", value);
        }
    }

    [JsonIgnore]
    public int Mask
    {
        get => _aMask;
        set
        {
            _aMask = value;
            AvatarRandomJsonData.SetField("a_mask", value);
        }
    }
    [JsonIgnore]
    public int ShawlHair
    {
        get => _shawlHair;
        set
        {
            _shawlHair = value;
            AvatarRandomJsonData.SetField("Shawl_hair", value);
        }
    }
    [JsonIgnore]
    public int BackGown
    {
        get => _backGown;
        set
        {
            _backGown = value;
            AvatarRandomJsonData.SetField("back_gown", value);
        }
    }
    [JsonIgnore]
    public int RightArm
    {
        get => _rARM;
        set
        {
            _rARM = value;
            AvatarRandomJsonData.SetField("r_arm", value);
        }
    }
    [JsonIgnore]
    public int Gown
    {
        get => _gown;
        set
        {
            _gown = value;
            AvatarRandomJsonData.SetField("gown", value);
        }
    }
    [JsonIgnore]
    public int LeftArm
    {
        get => _lARM;
        set
        {
            _lARM = value;
            AvatarRandomJsonData.SetField("l_arm", value);
        }
    }
    [JsonIgnore]
    public int LeftBigArm
    {
        get => _lBigARM;
        set
        {
            _lBigARM = value;
            AvatarRandomJsonData.SetField("l_big_arm", value);
        }
    }
    [JsonIgnore]
    public int LowerBody
    {
        get => _lowerBody;
        set
        {
            _lowerBody = value;
            AvatarRandomJsonData.SetField("lower_body", value);
        }
    }
    [JsonIgnore]
    public int RightBigArm
    {
        get => _rBigARM;
        set
        {
            _rBigARM = value;
            AvatarRandomJsonData.SetField("r_big_arm", value);
        }
    }
    [JsonIgnore]
    public int Blush
    {
        get => _blush;
        set
        {
            _blush = value;
            AvatarRandomJsonData.SetField("blush", value);
        }
    }
    [JsonIgnore]
    public int Tattoo
    {
        get => _tattoo;
        set
        {
            _tattoo = value;
            AvatarRandomJsonData.SetField("tattoo", value);
        }
    }
    [JsonIgnore]
    public int Shoes
    {
        get => _shoes;
        set
        {
            _shoes = value;
            AvatarRandomJsonData.SetField("shoes", value);
        }
    }
    [JsonIgnore]
    public int UpperBody
    {
        get => _upperBody;
        set
        {
            _upperBody = value;
            AvatarRandomJsonData.SetField("upper_body", value);
        }
    }
    [JsonIgnore]
    public int YanQiu
    {
        get => _yanqiu;
        set
        {
            _yanqiu = value;
            AvatarRandomJsonData.SetField("yanqiu", value);
        }
    }
    [JsonIgnore]
    public int MouthColor
    {
        get => _mouthColor;
        set
        {
            _mouthColor = value;
            AvatarRandomJsonData.SetField("mouthColor", value);
        }
    }
    [JsonIgnore]
    public int TattooColor
    {
        get => _tattooColor;
        set
        {
            _tattooColor = value;
            AvatarRandomJsonData.SetField("tattooColor", value);
        }
    }
    [JsonIgnore]
    public int BlushColor
    {
        get => _blushColor;
        set
        {
            _blushColor = value;
            AvatarRandomJsonData.SetField("blushColor", value);
        }
    }
    [JsonIgnore]
    public int HaoGanDu
    {
        get => _haoGanDu;
        set
        {
            _haoGanDu = value;
            AvatarRandomJsonData.SetField("HaoGanDu", value);
        }
    }
    [JsonIgnore]
    public int Head
    {
        get => _head;
        set
        {
            _head = value;
            AvatarRandomJsonData.SetField("head", value);
        }
    }
    [JsonIgnore]
    public int Eyes
    {
        get => _eyes;
        set
        {
            _eyes = value;
            AvatarRandomJsonData.SetField("eyes", value);
        }
    }
    [JsonIgnore]
    public int Mouth
    {
        get => _mouth;
        set
        {
            _mouth = value;
            AvatarRandomJsonData.SetField("mouth", value);
        }
    }
    [JsonIgnore]
    public int Nose
    {
        get => _nose;
        set
        {
            _nose = value;
            AvatarRandomJsonData.SetField("nose", value);
        }
    }
    [JsonIgnore]
    public int Eyebrow
    {
        get => _eyebrow;
        set
        {
            _eyebrow = value;
            AvatarRandomJsonData.SetField("eyebrow", value);
        }
    }
    [JsonIgnore]
    public int Hair
    {
        get => _hair;
        set
        {
            _hair = value;
            AvatarRandomJsonData.SetField("hair", value);
        }
    }
    [JsonIgnore]
    public int AHair
    {
        get => _aHair;
        set
        {
            _aHair = value;
            AvatarRandomJsonData.SetField("a_hair", value);
        }
    }
    [JsonIgnore]
    public int BHair
    {
        get => _bHair;
        set
        {
            _bHair = value;
            AvatarRandomJsonData.SetField("b_hair", value);
        }
    }
    [JsonIgnore]
    public int Characteristic
    {
        get => _characteristic;
        set
        {
            _characteristic = value;
            AvatarRandomJsonData.SetField("characteristic", value);
        }
    }
    [JsonIgnore]
    public int Suit
    {
        get => _aSuit;
        set
        {
            _aSuit = value;
            AvatarRandomJsonData.SetField("a_suit", value);
        }
    }
    [JsonIgnore]
    public int YanZhuColor
    {
        get => _yanzhuColor;
        set
        {
            _yanzhuColor = value;
            AvatarRandomJsonData.SetField("yanzhuColor", value);
        }
    }
    [JsonIgnore]
    public int TeZhengColor
    {
        get => _tezhengColor;
        set
        {
            _tezhengColor = value;
            AvatarRandomJsonData.SetField("tezhengColor", value);
        }
    }
    [JsonIgnore]
    public int EyebrowColor
    {
        get => _eyebrowColor;
        set
        {
            _eyebrowColor = value;
            AvatarRandomJsonData.SetField("eyebrowColor", value);
        }
    }
    #region 头发颜色相关

    [JsonIgnore]
    public int HairColorRed
    {
        get => _hairColorRed;
        set
        {
            _hairColorRed = value;
            AvatarRandomJsonData.SetField("hairColorR", value);
        }
    }

    [JsonIgnore]
    public int HairColorBlue
    {
        get => _hairColorBlue;
        set
        {
            _hairColorBlue = value;
            AvatarRandomJsonData.SetField("hairColorB", value);
        }
    }

    [JsonIgnore]
    public int HairColorGreen
    {
        get => _hairColorGreen;
        set
        {
            _hairColorGreen = value;
            AvatarRandomJsonData.SetField("hairColorG", value);
        }
    }

    public bool SetHairColor(int red, int blue, int green)
    {
        try
        {
            HairColorRed = red;
            HairColorBlue = blue;
            HairColorGreen = green;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion
}

[DialogEnvQuery("GetAvatarJsonData")]
public class GetAvatarJsonData : IDialogEnvQuery
{
    public object Execute(DialogEnvQueryContext context)
    {
        Main.LogInfo($"Args:{JArray.FromObject(context.Args)}");
        var npcId = NPCEx.NPCIDToNew(context.GetMyArgs(0, 0));
        Main.LogInfo($"npcID:{npcId.ToString()}");
        var data = jsonData.instance.AvatarRandomJsonData.TryGetField(npcId.ToString()).ToString();
        Main.LogInfo($"npc:{data}");
        var avatar = string.IsNullOrWhiteSpace(data) || data == "null"? null : JObject.Parse(data).ToObject<AvatarJsonData>();
        if (avatar != null)
        {
            avatar.Id = npcId;
        }

        return avatar;
    }
}