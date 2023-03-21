// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using JSONClass;
// using KillSystem;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using UnityEngine;
// using Avatar = KBEngine.Avatar;
// using Random = UnityEngine.Random;
//
// namespace SkySwordKill.NextMoreCommand.Utils
// {
//     public class NpcShopType : IEnumerator, IEnumerable
//     {
//         public Dictionary<int, List<JSONObject>> ShopDictionary = new Dictionary<int, List<JSONObject>>();
//         private int _position = -1;
//         private List<int> _keys;
//         public List<int> Keys
//         {
//             get
//             {
//                 var list = ShopDictionary.Keys.ToList();
//                 if (_keys == null || _keys.Count != list.Count)
//                 {
//                     _keys = ShopDictionary.Keys.ToList();
//                 }
//                 return _keys;
//             }
//         }
//
//
//         public List<JSONObject> this[int index]
//         {
//             get => ShopDictionary[index];
//             set => ShopDictionary[index] = value;
//         }
//         public bool MoveNext()
//         {
//             _position++;
//             return (_position < ShopDictionary.Count);
//         }
//         public void Reset()
//         {
//             _position = -1;
//         }
//         public object Current
//         {
//             get => ShopDictionary[Keys[_position]];
//         }
//         public IEnumerator GetEnumerator()
//         {
//             return (IEnumerator)this;
//         }
//     }
//
//     public class TypeNpc : IEnumerator, IEnumerable
//     {
//         public Dictionary<int, List<JSONObject>> TypeDictionary = new Dictionary<int, List<JSONObject>>();
//         public List<JSONObject> this[int index]
//         {
//             get => TypeDictionary[index];
//             set => TypeDictionary[index] = value;
//         }
//         private List<int> _keys;
//         public List<int> Keys
//         {
//             get
//             {
//                 var list = TypeDictionary.Keys.ToList();
//                 if (_keys == null || _keys.Count != list.Count)
//                 {
//                     _keys = TypeDictionary.Keys.ToList();
//                 }
//                 return _keys;
//             }
//         }
//         private int _position = -1;
//         public bool MoveNext()
//         {
//             _position++;
//             return (_position < TypeDictionary.Count);
//         }
//         public void Reset()
//         {
//             _position = -1;
//         }
//         public object Current
//         {
//             get => TypeDictionary[Keys[_position]];
//         }
//         public IEnumerator GetEnumerator()
//         {
//             return (IEnumerator)this;
//         }
//     }
//
//     [JsonObject]
//     public class NpcStatus
//     {
//         [JsonProperty("StatusId")]
//         public int Id = 1;
//         [JsonProperty("StatusTime")]
//         public int Time = 60000;
//         public NpcStatus()
//         {
//         }
//         public NpcStatus(int id, int time)
//         {
//             Id = id;
//             Time = time;
//         }
//     }
//
//     public class NpcJsonData
//     {
//         public static Avatar Player => PlayerEx.Player;
//         [JsonProperty]
//         public NpcStatus Status = new NpcStatus();
//         [JsonProperty]
//         public int Id = 0;
//         [JsonIgnore]
//         private JSONObject NpcDate { get; }
//
//         [JsonIgnore]
//         private bool IsNewPlayer { get; }
//         [JsonIgnore]
//         private JSONObject ImportantJson { get; }
//         public NpcJsonData(JSONObject npcDate, bool isImportant, int zhiDingindex, bool isNewPlayer, JSONObject importantJson, NpcFactory npcFactory)
//             : this(npcDate, isImportant, zhiDingindex, isNewPlayer, importantJson)
//         {
//             SetNpcFactory(npcFactory);
//         }
//         public NpcJsonData(JSONObject npcDate, bool isImportant, int zhiDingindex, bool isNewPlayer, JSONObject importantJson, int sexType, NpcFactory npcFactory = null)
//             : this(npcDate, isImportant, zhiDingindex, isNewPlayer, importantJson, npcFactory)
//         {
//             SexType = sexType;
//         }
//         public NpcJsonData(JSONObject npcDate, bool isImportant, int zhiDingindex = 0, bool isNewPlayer = true, JSONObject importantJson = null)
//         {
//             var hasZhiDingIndex = zhiDingindex > 0;
//             Id = hasZhiDingIndex ? zhiDingindex : Player.NPCCreateIndex;
//             if (hasZhiDingIndex)
//             {
//                 Player.NPCCreateIndex++;
//             }
//
//             NpcDate = npcDate;
//             IsImportant = isImportant;
//             ImportantJson = importantJson;
//             IsNewPlayer = isNewPlayer;
//         }
//         [JsonIgnore]
//         private NpcFactory _npcFactory = new NpcFactory();
//         public void SetNpcFactory(NpcFactory npcFactory)
//         {
//             if (npcFactory == null)
//             {
//                 return;
//             }
//             _npcFactory = npcFactory;
//         }
//         public void Init()
//         {
//             InitBase();
//             InitTitle();
//             InitAvatar();
//             InitImportantData();
//             InitLevelData();
//             InitOther();
//         }
//         #region 初始化属性
//
//         [JsonProperty]
//         public string Name { get; set; } = string.Empty;
//         [JsonProperty]
//         public bool IsTag { get; set; } = false;
//         [JsonProperty]
//         public string FirstName { get; set; } = string.Empty;
//         [JsonProperty("face")]
//         public int Face { get; set; } = 0;
//         [JsonProperty("fightFace")]
//         public int FightFace { get; set; } = 0;
//         [JsonProperty("isImportant")]
//         public bool IsImportant { get; set; }
//         [JsonProperty("IsKnowPlayer")]
//         public bool IsKnowPlayer { get; set; } = false;
//         [JsonProperty("NPCTag")]
//         public int NpcTag { get; set; } = 0;
//         [JsonProperty("XingGe")]
//         public int XingGe { get; set; } = 0;
//         [JsonProperty("QingFen")]
//         public int QingFen { get; set; } = 0;
//         [JsonProperty("ActionId")]
//         public int ActionId { get; set; } = 1;
//         [JsonProperty("CyList")]
//         public List<int> CyList { get; set; } = new List<int>();
//         [JsonProperty("TuPoMiShu")]
//         public List<int> TuPoMiShu { get; set; } = new List<int>();
//         public void InitBase()
//         {
//             var npcDate = NpcDate;
//             FirstName = npcDate["FirstName"].Str;
//             var npcTag = npcDate["NPCTag"].ToList();
//             NpcTag = GetRandom(npcTag[0], npcTag[1]);
//             XingGe = GetRandomXingGe(_npcFactory.JsonData.NPCTagDate[NpcTag.ToString()]["zhengxie"].I);
//         }
//         public int GetRandomXingGe(int zhengXie)
//         {
//             var npcXingGeDictionary = _npcFactory.NpcXingGeDictionary;
//             if (!npcXingGeDictionary.ContainsKey(zhengXie))
//             {
//                 var npcXingGeDate = _npcFactory.JsonData.NpcXingGeDate;
//                 foreach (var xingGeDate in npcXingGeDate.list)
//                 {
//                     var zx = xingGeDate["zhengxie"].I;
//                     var id = xingGeDate["id"].I;
//                     if (npcXingGeDictionary.ContainsKey(zx))
//                     {
//                         npcXingGeDictionary[zx].Add(id);
//                     }
//                     else
//                     {
//                         npcXingGeDictionary.Add(zx, new List<int>()
//                         {
//                             id
//                         });
//
//                     }
//                 }
//             }
//             var zhengXieDict = npcXingGeDictionary[zhengXie];
//             return zhengXieDict[GetRandom(zhengXieDict.Count - 1)];
//         }
//
//         #endregion
//
//         #region 初始化称号
//
//         [JsonProperty]
//         public string Title { get; set; } = string.Empty;
//         [JsonProperty]
//         public int ChengHaoID { get; set; } = 0;
//         [JsonProperty]
//         public int Type { get; set; } = 0;
//         [JsonProperty]
//         public int Level { get; set; } = 0;
//         [JsonProperty]
//         public int GongXian { get; set; } = 0;
//
//
//         public void InitTitle()
//         {
//             Type = GetNpcDateInt("Type");
//             Level = GetNpcDateInt("Level");
//             var npcChengHaoData = _npcFactory.JsonData.NPCChengHaoData;
//             if (IsImportant && ImportantJson.HasField(nameof(ChengHaoID)))
//             {
//                 ChengHaoID = GeImportantJsonInt("ChengHaoID");
//                 var idStr = ChengHaoID.ToString();
//                 var str = npcChengHaoData[idStr]["ChengHao"].str;
//                 _npcFactory.NpcJieSuanManager.npcChengHao.npcOnlyChengHao.SetField(idStr, Id);
//                 Title = str.ToCN();
//                 return;
//             }
//
//             var chengHaoData = NPCChengHaoData.DataList.Find(data => data.NPCType == Type && Level >= data.Level[0] && Level <= data.Level[1] && data.GongXian == 0);
//             Title = chengHaoData.ChengHao.ToCN();
//             ChengHaoID = chengHaoData.id;
//         }
//         [JsonProperty]
//         public int SexType { get; set; } = 0;
//         [JsonProperty]
//         public int LiuPai { get; set; } = 0;
//         [JsonProperty]
//         public int MenPai { get; set; } = 0;
//         [JsonProperty]
//         public int AvatarType { get; set; } = 0;
//         [JsonProperty]
//         public int WuDaoValue { get; set; } = 0;
//         [JsonProperty]
//         public int WuDaoValueLevel { get; set; } = 0;
//         [JsonProperty("EWWuDaoDian")]
//         public int ExtraWuDao { get; set; } = 0;
//         [JsonProperty]
//         public bool IsNeedHelp { get; set; } = false;
//         public void InitAvatar()
//         {
//             if (SexType == 0)
//             {
//                 SexType = IsImportant ? GeImportantJsonInt("SexType") : Type == 3 ? 2 : GetRandom(1, 2);
//             }
//
//             LiuPai = GetNpcDateInt("LiuPai");
//             MenPai = GetNpcDateInt("MenPai");
//             AvatarType = GetNpcDateInt("AvatarType");
//         }
//
//   #endregion
//         #region 初始化固定时间
//
//         [JsonProperty]
//         public int BindingNpcID { get; set; } = 0;
//         [JsonProperty]
//         public string FlyTime { get; set; } = string.Empty;
//         [JsonProperty]
//         public string ZhuJiTime { get; set; } = string.Empty;
//         [JsonProperty]
//         public int LianQiAddSpeed { get; set; } = 0;
//         [JsonProperty]
//         public string JinDanTime { get; set; } = string.Empty;
//         [JsonProperty]
//         public int ZhuJiAddSpeed { get; set; } = 0;
//         [JsonProperty]
//         public string YuanYingTime { get; set; } = string.Empty;
//         [JsonProperty]
//         public int JinDanAddSpeed { get; set; } = 0;
//         [JsonProperty]
//         public string HuaShengTime { get; set; } = string.Empty;
//         [JsonProperty]
//         public int YuanYingAddSpeed { get; set; } = 0;
//
//         public void InitImportantData()
//         {
//             if (!IsImportant)
//             {
//                 return;
//             }
//             BindingNpcID = GeImportantJsonInt("BindingNpcID");
//             if (BindingNpcID == 2244) FlyTime = "950-01-01";
//             var nowTime = "0001-1-1";
//             var targetLevel = 4;
//             if (GetTime("ZhuJiTime", out var zhuJiTime))
//             {
//                 ZhuJiTime = zhuJiTime;
//                 LianQiAddSpeed = GetEWaiXiuLianSpeed(Level, targetLevel, nowTime, zhuJiTime);
//             }
//             targetLevel += 3;
//             if (GetTime("JinDanTime", out var jinDanTime))
//             {
//                 JinDanTime = zhuJiTime;
//                 var isZhuJi = Level > 3;
//                 ZhuJiAddSpeed = GetEWaiXiuLianSpeed(isZhuJi ? Level : 4, targetLevel, isZhuJi ? nowTime : ZhuJiTime, jinDanTime);
//             }
//             targetLevel += 3;
//             if (GetTime("YuanYingTime", out var yuanYingTime))
//             {
//                 YuanYingTime = yuanYingTime;
//                 var isJinDan = Level > 6;
//                 JinDanAddSpeed = GetEWaiXiuLianSpeed(isJinDan ? Level : 7, targetLevel, isJinDan ? nowTime : JinDanTime, yuanYingTime);
//             }
//             targetLevel += 3;
//             if (GetTime("HuaShengTime", out var huaShengTime))
//             {
//                 HuaShengTime = huaShengTime;
//                 var isHuaSheng = Level > 10;
//                 YuanYingAddSpeed = GetEWaiXiuLianSpeed(isHuaSheng ? Level : 10, targetLevel, isHuaSheng ? nowTime : YuanYingTime, huaShengTime);
//             }
//
//             _npcFactory.NpcJieSuanManager.ImportantNpcBangDingDictionary.Add(BindingNpcID, Id);
//         }
//
//         public int GetEWaiXiuLianSpeed(int curLevel, int targetLevel, string curTime, string targetTime)
//         {
//             try
//             {
//                 var num = (DateTime.Parse(targetTime) - DateTime.Parse(curTime)).Days / 30;
//                 var num2 = 0;
//                 var npcChuShiShuZiDate = _npcFactory.JsonData.NPCChuShiShuZiDate;
//                 for (var i = curLevel; i <= targetLevel; i++)
//                 {
//                     num2 += npcChuShiShuZiDate[i.ToString()]["xiuwei"].I;
//                 }
//                 return num2 / num;
//             }
//             catch (Exception message)
//             {
//                 Debug.Log(message);
//             }
//             return 0;
//         }
//         public bool GetTime(string name, out string str)
//         {
//             str = string.Empty;
//             var hasTime = ImportantJson.HasField(name);
//             if (hasTime)
//             {
//                 str = GeImportantJsonStr(name);
//             }
//             return hasTime;
//         }
//
//   #endregion
//         #region 初始化境界信息
//
//         [JsonProperty("HP")]
//         public int Hp { get; set; } = 0;
//         [JsonProperty("dunSu")]
//         public int DunSu { get; set; } = 0;
//         [JsonProperty("ziZhi")]
//         public int ZiZhi { get; set; } = 0;
//         [JsonProperty("wuXin")]
//         public int WuXin { get; set; } = 0;
//         [JsonProperty("shengShi")]
//         public int ShengShi { get; set; } = 0;
//         [JsonProperty("shaQi")]
//         public int ShaQi { get; set; } = 0;
//         [JsonProperty("shouYuan")]
//         public int ShouYuan { get; set; } = 0;
//         [JsonProperty("age")]
//         public int Age { get; set; } = 0;
//         [JsonProperty("exp")]
//         public int Exp { get; set; } = 0;
//         [JsonProperty]
//         public int NextExp { get; set; } = 0;
//         [JsonProperty]
//         public int MoneyType { get; set; } = 0;
//         [JsonProperty("equipWeapon")]
//         public int EquipWeapon { get; set; } = 0;
//         public void InitLevelData()
//         {
//             var npcChuShiShuZiDate = _npcFactory.JsonData.NPCChuShiShuZiDate;
//             var data = npcChuShiShuZiDate.list.Find(o => o["id"].I == Level);
//             if (data == null)
//             {
//                 return;
//             }
//             var dict = GetRandomData(data);
//             Hp = dict["Hp"];
//             DunSu = dict["dunSu"];
//             ZiZhi = dict["ziZhi"];
//             WuXin = dict["wuXin"];
//             ShengShi = dict["shengShi"];
//             ShouYuan = dict["shouYuan"];
//             Age = dict["age"];
//             MoneyType = dict["MoneyType"];
//             var nextLevel = Level + 1;
//             NextExp = Level == 15 ? 0 : npcChuShiShuZiDate[nextLevel.ToString()]["xiuwei"].I;
//         }
//
//         public string[] RandomKeyArray = new[]
//         {
//             "HP", "dunSu", "ziZhi", "wuXin", "shengShi", "shouYuan", "age", "MoneyType"
//         };
//         public Dictionary<string, int> GetRandomData(JSONObject data)
//         {
//             var dict = new Dictionary<string, int>();
//             foreach (var key in RandomKeyArray)
//             {
//                 if (!data.HasField(key))
//                 {
//                     dict.Add(key, 0);
//                     continue;
//                 }
//                 var list = data[key].ToList();
//                 dict.Add(key, GetRandom(list[0], key == "age" ? list[1] * 12 : list[1]));
//             }
//             return dict;
//         }
//
//   #endregion
//         #region 初始化其他
//
//         public static readonly List<int> KillerType = new List<int>()
//         {
//             21,
//             22
//         };
//         public void InitOther()
//         {
//             if (IsImportant)
//             {
//                 NpcTag = GeImportantJsonInt("NPCTag");
//                 XingGe = GeImportantJsonInt("XingGe");
//             }
//
//             _npcFactory.JsonData.AvatarJsonData.SetField(Id.ToString(), ToJsonObject());
//             if (IsImportant || !KillerType.Contains(Type))
//             {
//                 return;
//             }
//             KillManager.Inst.KillerService.CreateKiller(Id);
//         }
//
//   #endregion
//         public int GetNpcDateInt(string name) => NpcDate[name].I;
//         public string GetNpcDateStr(string name) => NpcDate[name].Str;
//         public int GeImportantJsonInt(string name) => ImportantJson[name].I;
//         public string GeImportantJsonStr(string name) => ImportantJson[name].Str;
//         public static int GetRandom(int min, int max) => Random.Range(min, max + 1);
//         public static int GetRandom(int max) => GetRandom(0, max);
//         #region 转换类型方法
//
//         public JSONObject ToJsonObject() => new JSONObject(ToString());
//         public override string ToString()
//         {
//             return ToString(false);
//         }
//         public string ToString(bool pretty)
//         {
//             return JObject.FromObject(this).ToString(pretty ? Formatting.Indented : Formatting.None);
//         }
//
//   #endregion
//
//     }
// /**/
//     public class NpcFactory
//     {
//     #region 字段
//
//         public static Avatar Player => PlayerEx.Player;
//         public bool IsNewGame { get; set; } = false;
//         public readonly Dictionary<int, List<string>> NpcAutoCreateDictionary = new Dictionary<int, List<string>>();
//         public readonly Dictionary<int, List<int>> NpcXingGeDictionary = new Dictionary<int, List<int>>();
//         public Random Random { get; set; } = new Random();
//         public readonly Dictionary<int, NpcShopType> ShopTypeDictionary = new Dictionary<int, NpcShopType>();
//         public readonly Dictionary<int, TypeNpc> TypeDictionary = new Dictionary<int, TypeNpc>();
//         public NpcJieSuanManager NpcJieSuanManager => NpcJieSuanManager.inst;
//         public jsonData JsonData => jsonData.instance;
//         public static int GetRandom(int min, int max) => Random.Range(min, max + 1);
//         public static int GetRandom(int max) => GetRandom(0, max);
//
//     #endregion
//
//     #region 初始化NPC
//
//         public void InitCreateNpc()
//         {
//             IsNewGame = true;
//             NpcJieSuanManager.ImportantNpcBangDingDictionary = new Dictionary<int, int>();
//             InitNpcDate();
//         }
//
//         public void InitNpcDate()
//         {
//             var jsonData = JsonData;
//             var npcChuShiHuaDate = jsonData.NPCChuShiHuaDate;
//             var count = npcChuShiHuaDate.Count;
//             if (count == 0)
//             {
//                 return;
//             }
//             var npcLeiXingDate = jsonData.NPCLeiXingDate.list;
//             foreach (var npcData in npcChuShiHuaDate.list)
//             {
//                 var levelList = npcData["Level"].ToList();
//                 var numList = npcData["Num"].ToList();
//                 var liuPai = npcData["LiuPai"].I;
//                 var levelCount = levelList.Count;
//                 for (var i = 0; i < levelCount; i++)
//                 {
//                     var level = levelList[i];
//                     var num = numList[i];
//                     var list = npcLeiXingDate.Where(leiXing => liuPai == leiXing["LiuPai"].I && level == leiXing["Level"].I)
//                         .Select(leiXing => CreateNpcJson(leiXing, num));
//                     foreach (var npcJsonList in list)
//                     {
//                         npcJsonList.ForEach(CreateNewGameRandomNpc);
//                     }
//                 }
//             }
//         }
//
//     #endregion
//     #region 创建NPC方法
//
//         public static List<JSONObject> CreateNpcJson(JSONObject jsonObject, int count) => CreateNpcJson(jsonObject.ToString(), count);
//         public static List<JSONObject> CreateNpcJson(string str, int count)
//         {
//             var list = new List<JSONObject>();
//             for (var i = 0; i < count; i++)
//             {
//                 list.Add(JSONObject.Create(str));
//             }
//             return list;
//         }
//         public void CreateNewGameRandomNpc(JSONObject npcDate) => CreateNpc(npcDate, false);
//         public int CreateNpc(JSONObject npcDate, bool isImportant, int zhiDingindex = 0, bool isNewPlayer = true, JSONObject importantJson = null)
//         {
//             var npc = new NpcJsonData(npcDate, isImportant, zhiDingindex, isNewPlayer, importantJson, this);
//             npc.Init();
//             return npc.Id;
//         }
//
//     #endregion
//     }
//
//     public static class NpcFactoryUtils
//     {
//
//     }
// }