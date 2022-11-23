using System.Collections.Generic;
using Newtonsoft.Json;
using SkySwordKill.Next;
using System.Linq;
namespace SkySwordKill.NextMoreCommand.CustomMap
{
    public class CustomMapData : BaseMapData
    {
        [JsonProperty("Map")] public List<List<string>> MapTypes = new List<List<string>>();

        public CustomMapData(int high, int wide)
        {
            High = high;
            Wide = wide;
            InitHighAndWide();
        }

        public void InitHighAndWide()
        {
            High = High >= MaxHigh ? MaxHigh : High;
            Wide = Wide >= MaxWide ? MaxWide : Wide;
        }

        public void InitMapTypes()
        {
            var mapTypesHighRest = High - MapTypes.Count;
            if (mapTypesHighRest != 0)
            {
                var wideList = new List<string>();
                for (int i = 0; i < Wide; i++)
                {
                    wideList.Add("Block");
                }

                for (int i = 0; i < mapTypesHighRest; i++)
                {
                    MapTypes.Add(wideList);
                }


                foreach (var mapType in MapTypes)
                {
                    if (mapType.Count != Wide)
                    {
                        var mapTypesWideRest = Wide - mapType.Count;
                        for (int i = 0; i < mapTypesWideRest; i++)
                        {
                            mapType.Add("Block");
                        }
                    }
                }
            }
        }

        public void Debug()
        {
            InitHighAndWide();
            InitMapTypes();
            Main.LogInfo(JsonConvert.SerializeObject(MapTypes, Formatting.Indented));
        }

        public FuBenMap.NodeType[,] ToMap()
        {
            FuBenMap.NodeType[,] map = new FuBenMap.NodeType[Wide, High];
            for (int y = 0; y < MapTypes.Count; y++)
            {
                var high = MapTypes[y];
                for (int x = 0; x < high.Count; x++)
                {
                    var wide = high[x];
                    if (CustomMapManager.TryGetCustomMapType(wide, out var value))
                    {
                        map[x, y] = value.GetMapNodeType();
                    }
                    else if (CustomMapManager.TryGetMapType(wide, out value))
                    {
                        map[x, y] = value.GetMapNodeType();
                    }
                }
            }

            return map;
        }


        public override CustomMap ToCustomMap()
        {
            return new CustomMap()
            {
                Name = Name,
                Uuid = Tools.getUUID(),
                Map = ToMap(),
                Wide = Wide,
                High = High,
                ShouldReset = ShouldReset,
                Award = new CustomMapAward[] { },
                Event = new CustomMapEvent[] { },
                NanDu = NanDu,
                Type = 1,
                ShuXing = 1,
            };
        }
    }
}