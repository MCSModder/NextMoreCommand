using CustomDungeonsMore.Data;
using UniqueCream.CustomDungeons.Dungeon;

namespace CustomDungeonsMore.Utils
{
    public static class DungeonUtils
    {
        public static MapPointData GetNowMapPoint(this DungeonData dungeonData)
        {
            return dungeonData.GetMapPoint(dungeonData.NowIndex);
        }
        public static MapPointData GetPreMapPoint(this DungeonData dungeonData)
        {
            return dungeonData.GetMapPoint(dungeonData.PreIndex);
        }
        public static MapData GetNowMapData(this DungeonData dungeonData)
        {
            var pointMap = dungeonData.GetNowMapPoint();
            return new MapData(pointMap, dungeonData);
        }
        public static MapData GetMapData(this DungeonData dungeonData, int index)
        {
            var pointMap = dungeonData.GetMapPoint(index);
            return pointMap is null ? null : new MapData(pointMap, dungeonData);
        }
        public static MapData GetMapData(int index)
        {
            var dungeonController = DungeonController.Instance;
            return !dungeonController.IsShow ? null : dungeonController.DungeonData.GetMapData(index);
        }
        public static MapData GetMapData(this DungeonData dungeonData, int x, int y)
        {
            var pointMap = dungeonData.GetMapPoint(x, y);
            return pointMap is null ? null : new MapData(pointMap, dungeonData);
        }
        public static MapData GetMapData(int x, int y)
        {
            var dungeonController = DungeonController.Instance;
            return !dungeonController.IsShow ? null : dungeonController.DungeonData.GetMapData(x, y);
        }
        public static MapData GetNowMapData()
        {
            var dungeonController = DungeonController.Instance;
            return !dungeonController.IsShow ? null : dungeonController.DungeonData.GetNowMapData();
        }
    }
}