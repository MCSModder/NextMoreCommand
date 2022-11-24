using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SkySwordKill.Next;
using System.Linq;
using SkySwordKill.NextMoreCommand.CustomMap.CustomMapType;

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

            MapTypes.Reverse();
        }

        public void Debug()
        {
            InitHighAndWide();
            InitMapTypes();
            Main.LogInfo(JsonConvert.SerializeObject(MapTypes, Formatting.Indented));
        }

        public MapData MapData;

        public void ToMap()
        {
            MapData = new MapData(High, Wide);
            var index = 1;
            for (int y = 0; y < MapTypes.Count; y++)
            {
                var high = MapTypes[y];
                for (int x = 0; x < high.Count; x++)
                {
                    var wide = high[x];
                    if (CustomMapManager.TryGetCustomMapType(wide, out var value))
                    {
                        var type = value.GetMapNodeType();
                        switch (type)
                        {
                            case MapNodeType.Null:
                                MapData.CreateNode(index, type);
                                break;
                            case MapNodeType.Road:
                                MapData.CreateNode(index, type);
                                switch (value.GetMapType())
                                {
                                    case "NextDialog":
                                        MapData.NextDialog.Add(new NextEvent(index, value.ID, value.Cid));
                                        break;
                                    case "NextTrigger":
                                        MapData.NextTrigger.Add(new NextEvent(index, value.ID, value.Cid));
                                        break;
                                }
                                break;
                            case MapNodeType.Exit:
                                MapData.CreateExit(index);
                                break;
                            case MapNodeType.Entrance:
                                MapData.CreateEntrance(index);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else if (CustomMapManager.TryGetMapType(wide, out value))
                    {
                        var type = value.GetMapNodeType();
                        switch (type)
                        {
                            case MapNodeType.Null:
                                MapData.CreateNode(index, type);
                                break;
                            case MapNodeType.Road:
                                MapData.CreateNode(index, type);
                                switch (value.GetMapType())
                                {
                                    case "NextDialog":
                                        MapData.NextDialog.Add(new NextEvent(index, value.ID, value.Cid));
                                        break;
                                    case "NextTrigger":
                                        MapData.NextTrigger.Add(new NextEvent(index, value.ID, value.Cid));
                                        break;
                                }


                                break;
                            case MapNodeType.Exit:
                                MapData.CreateExit(index);
                                break;
                            case MapNodeType.Entrance:
                                MapData.CreateEntrance(index);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    if (value != null && value.Block.Name != string.Empty) 
                    {
                        MapData.Blocks.Add(new MyBlock(index,value.Block.Name));
                    }
                    index++;
                }
            }
        }


        public override CustomMap ToCustomMap()
        {
            ToMap();
            return new CustomMap()
            {
                Name = Name,
                Uuid = Tools.getUUID(),
                Map = MapData.Map,
                Wide = Wide,
                High = High,
                ShouldReset = ShouldReset,
                Award = MapData.Award.ToArray(),
                Event = MapData.Event.ToArray(),
                NanDu = NanDu,
                Type = 1,
                ShuXing = 1,
                Entrance = MapData.Entrance,
                Exit = MapData.Exit,
                NextDialog = MapData.NextDialog,
                NextTrigger = MapData.NextTrigger,
                Blocks = MapData.Blocks
            };
        }
    }
}