using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc
{
    public  class AvatarJsonData
    {

        public AvatarJsonData(int id)
        {
            Id = id.ToNpcNewId();
        }
        private JSONObject AvatarRandomJsonData => jsonData.instance.AvatarRandomJsonData[Id.ToString()];
        private string Str => AvatarRandomJsonData?.ToString(true);
        public int Id { get; }


        public string Name
        {
            get => AvatarRandomJsonData?.GetField("Name").Str;
            set => AvatarRandomJsonData?.SetField("Name", value);
        }


        public string BirthdayTime
        {
            get => AvatarRandomJsonData?.GetField("BirthdayTime").Str;
            private set => AvatarRandomJsonData?.SetField("BirthdayTime", value);
        }

        // ReSharper disable once HeapView.BoxingAllocation
        public void SetBirthdayTime(int year, int mouth, int day) =>
            BirthdayTime = $"{year:0000}-{mouth.ToString()}-{day.ToString()}";


        public int Sex
        {
            get => AvatarRandomJsonData.GetField("Sex").I;
            private set => AvatarRandomJsonData?.SetField("Sex", value);
        }


        public int Feature
        {
            get => AvatarRandomJsonData.GetField("feature").I;
            set => AvatarRandomJsonData?.SetField("feature", value);
        }


        public int YanYing
        {
            get => AvatarRandomJsonData.GetField("yanying").I;
            set => AvatarRandomJsonData?.SetField("yanying", value);
        }


        public int Mask
        {
            get => AvatarRandomJsonData.GetField("a_mask").I;
            set => AvatarRandomJsonData?.SetField("a_mask", value);
        }


        public int ShawlHair
        {
            get => AvatarRandomJsonData.GetField("Shawl_hair").I;
            set => AvatarRandomJsonData?.SetField("Shawl_hair", value);
        }


        public int BackGown
        {
            get => AvatarRandomJsonData.GetField("back_gown").I;
            set => AvatarRandomJsonData?.SetField("back_gown", value);
        }


        public int RightArm
        {
            get => AvatarRandomJsonData.GetField("r_arm").I;
            set => AvatarRandomJsonData?.SetField("r_arm", value);
        }


        public int Gown
        {
            get => AvatarRandomJsonData.GetField("gown").I;
            set => AvatarRandomJsonData?.SetField("gown", value);
        }


        public int LeftArm
        {
            get => AvatarRandomJsonData.GetField("l_arm").I;
            set => AvatarRandomJsonData?.SetField("l_arm", value);
        }


        public int LeftBigArm
        {
            get => AvatarRandomJsonData.GetField("l_big_arm").I;
            set => AvatarRandomJsonData?.SetField("l_big_arm", value);
        }


        public int LowerBody
        {
            get => AvatarRandomJsonData.GetField("lower_body").I;
            set => AvatarRandomJsonData?.SetField("lower_body", value);

        }

        public int RightBigArm
        {
            get => AvatarRandomJsonData.GetField("r_big_arm").I;
            set => AvatarRandomJsonData?.SetField("r_big_arm", value);
        }
        public int Blush
        {
            get => AvatarRandomJsonData.GetField("blush").I;
            set => AvatarRandomJsonData?.SetField("blush", value);
        }
        public int Tattoo
        {
            get => AvatarRandomJsonData.GetField("tattoo").I;
            set => AvatarRandomJsonData?.SetField("tattoo", value);
        }
        public int Shoes
        {
            get => AvatarRandomJsonData.GetField("shoes").I;
            set => AvatarRandomJsonData?.SetField("shoes", value);
        }
        public int UpperBody
        {
            get => AvatarRandomJsonData.GetField("upper_body").I;
            set => AvatarRandomJsonData?.SetField("upper_body", value);
        }
        public int YanQiu
        {
            get => AvatarRandomJsonData.GetField("yanqiu").I;
            set => AvatarRandomJsonData?.SetField("yanqiu", value);
        }
        public int MouthColor
        {
            get => AvatarRandomJsonData.GetField("mouthColor").I;
            set => AvatarRandomJsonData?.SetField("mouthColor", value);
        }
        public int TattooColor
        {
            get => AvatarRandomJsonData.GetField("tattooColor").I;
            set => AvatarRandomJsonData?.SetField("tattooColor", value);
        }
        public int BlushColor
        {
            get => AvatarRandomJsonData.GetField("blushColor").I;
            set => AvatarRandomJsonData?.SetField("blushColor", value);
        }
        public int HaoGanDu
        {
            get => AvatarRandomJsonData.GetField("HaoGanDu").I;
            set => AvatarRandomJsonData?.SetField("HaoGanDu", value);
        }
        public int Head
        {
            get => AvatarRandomJsonData.GetField("head").I;
            set => AvatarRandomJsonData?.SetField("head", value);
        }
        public int Eyes
        {
            get => AvatarRandomJsonData.GetField("eyes").I;
            set => AvatarRandomJsonData?.SetField("eyes", value);
        }
        public int Mouth
        {
            get => AvatarRandomJsonData.GetField("mouth").I;
            set => AvatarRandomJsonData?.SetField("mouth", value);
        }
        public int Nose
        {
            get => AvatarRandomJsonData.GetField("nose").I;
            set => AvatarRandomJsonData?.SetField("nose", value);
        }
        public int Eyebrow
        {
            get => AvatarRandomJsonData.GetField("eyebrow").I;
            set => AvatarRandomJsonData?.SetField("eyebrow", value);
        }
        public int Hair
        {
            get => AvatarRandomJsonData.GetField("hair").I;
            set => AvatarRandomJsonData?.SetField("hair", value);
        }
        public int AHair
        {
            get => AvatarRandomJsonData.GetField("a_hair").I;
            set => AvatarRandomJsonData?.SetField("a_hair", value);
        }
        public int BHair
        {
            get => AvatarRandomJsonData.GetField("b_hair").I;
            set => AvatarRandomJsonData?.SetField("b_hair", value);
        }
        public int Characteristic
        {
            get => AvatarRandomJsonData.GetField("characteristic").I;
            set => AvatarRandomJsonData?.SetField("characteristic", value);
        }
        public int Suit
        {
            get => AvatarRandomJsonData.GetField("a_suit").I;
            set => AvatarRandomJsonData?.SetField("a_suit", value);
        }
        public int YanZhuColor
        {

            get => AvatarRandomJsonData.GetField("yanzhuColor").I;
            set => AvatarRandomJsonData?.SetField("yanzhuColor", value);
        }
        public int TeZhengColor
        {

            get => AvatarRandomJsonData.GetField("tezhengColor").I;
            set => AvatarRandomJsonData?.SetField("tezhengColor", value);
        }
        public int EyebrowColor
        {

            get => AvatarRandomJsonData.GetField("eyebrowColor").I;
            set => AvatarRandomJsonData?.SetField("eyebrowColor", value);
        }
    #region 头发颜色相关

        public int HairColorRed
        {

            get => AvatarRandomJsonData.GetField("hairColorR").I;
            set => AvatarRandomJsonData?.SetField("hairColorR", value);
        }
        public int HairColorBlue
        {


            get => AvatarRandomJsonData.GetField("hairColorB").I;
            set => AvatarRandomJsonData?.SetField("hairColorB", value);
        }
        public int HairColorGreen
        {

            get => AvatarRandomJsonData.GetField("hairColorG").I;
            set => AvatarRandomJsonData?.SetField("hairColorG", value);
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

    [DialogEnvQuery("GetAvatarRandomJsonData")]
    [DialogEnvQuery("获取角色捏脸数据")]
    public class GetAvatarRandomJsonData : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npcId = context.GetNpcID(0, 0);
            return jsonData.instance.AvatarRandomJsonData.HasField(npcId.ToString()) ? new AvatarJsonData(npcId) : null;
        }
    }
}