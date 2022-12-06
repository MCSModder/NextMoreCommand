using Newtonsoft.Json;

namespace SkySwordKill.NextMoreCommand.EnvExtension;

public partial class AvatarJsonData
{
     #region Json字段

    [JsonProperty("Sex")] private int _sex;
    [JsonProperty("feature")] private int _feature;
    [JsonProperty("yanying")] private int _yanying;
    [JsonProperty("a_mask")] private int _aMask;
    [JsonProperty("Shawl_hair")] private int _shawlHair;
    [JsonProperty("back_gown")] private int _backGown;
    [JsonProperty("r_arm")] private int _rARM;
    [JsonProperty("gown")] private int _gown;
    [JsonProperty("l_arm")] private int _lARM;
    [JsonProperty("l_big_arm")] private int _lBigARM;
    [JsonProperty("lower_body")] private int _lowerBody;
    [JsonProperty("r_big_arm")] private int _rBigARM;
    [JsonProperty("blush")] private int _blush;
    [JsonProperty("tattoo")] private int _tattoo;
    [JsonProperty("shoes")] private int _shoes;
    [JsonProperty("upper_body")] private int _upperBody;
    [JsonProperty("yanqiu")] private int _yanqiu;
    [JsonProperty("hairColorG")] private int _hairColorGreen;
    [JsonProperty("hairColorB")] private int _hairColorBlue;
    [JsonProperty("mouthColor")] private int _mouthColor;
    [JsonProperty("tattooColor")] private int _tattooColor;
    [JsonProperty("blushColor")] private int _blushColor;
    [JsonProperty("HaoGanDu")] private int _haoGanDu;
    [JsonProperty("head")] private int _head;
    [JsonProperty("eyes")] private int _eyes;
    [JsonProperty("mouth")] private int _mouth;
    [JsonProperty("nose")] private int _nose;
    [JsonProperty("eyebrow")] private int _eyebrow;
    [JsonProperty("hair")] private int _hair;
    [JsonProperty("a_hair")] private int _aHair;
    [JsonProperty("b_hair")] private int _bHair;
    [JsonProperty("characteristic")] private int _characteristic;
    [JsonProperty("a_suit")] private int _aSuit;
    [JsonProperty("hairColorR")] private int _hairColorRed;
    [JsonProperty("yanzhuColor")] private int _yanzhuColor;
    [JsonProperty("tezhengColor")] private int _tezhengColor;
    [JsonProperty("eyebrowColor")] private int _eyebrowColor;
    [JsonProperty("BirthdayTime")] private string _birthdayTime;
    [JsonProperty("Name")] private string _name;

    #endregion

}