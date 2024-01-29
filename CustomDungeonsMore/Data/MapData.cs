using System;
using CustomDungeonsMore.Utils;
using UniqueCream.CustomDungeons.Dungeon;

namespace CustomDungeonsMore.Data
{
    public enum EMapPoint
    {
        Center,
        Up,
        Down,
        Left,
        Right,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft,

    }

    public class MapData
    {
        public  MapPointData NowPointData { get; private set; }
        public readonly DungeonData DungeonData;
        public MapData(MapPointData nowPointData, DungeonData dungeonData)
        {
            NowPointData = nowPointData;
            DungeonData = dungeonData;
        }
        public void RefreshMapPoint()
        {
            NowPointData = DungeonData.GetNowMapPoint();
        }
        
        public int MaxX => DungeonData.MaxX;
        public int MaxY => DungeonData.MaxY;
        public MapPointData GetMapPointDataByPosition(EMapPoint mapPoint)
        {
            var y = 0;
            var x = 0;
            switch (mapPoint)
            {

                case EMapPoint.UpRight:
                    x = 1;
                    y = -1;
                    break;
                case EMapPoint.UpLeft:
                    x = -1;
                    y = -1;
                    break;
                case EMapPoint.Up:
                    y = -1;
                    break;
                case EMapPoint.Down:
                    y = 1;
                    break;
                case EMapPoint.Left:
                    x = -1;
                    break;
                case EMapPoint.Right:
                    x = 1;
                    break;
                case EMapPoint.DownRight:
                    x = 1;
                    y = 1;
                    break;
                case EMapPoint.DownLeft:
                    x = -1;
                    y = 1;
                    break;
                case EMapPoint.Center:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapPoint), mapPoint, null);
            }
            return DungeonData.GetMapPoint(NowPointData.X + x, NowPointData.Y + y,false);
        }
        public MapPointData PrevPointData => DungeonData.GetMapPoint(DungeonData.PreIndex);
        public MapData GetMapData(EMapPoint mapPoint)
        {
            
            var point =mapPoint switch
            {

                EMapPoint.Center => NowPointData,
                EMapPoint.Up => PointUp,
                EMapPoint.Down => PointDown,
                EMapPoint.Left => PointLeft,
                EMapPoint.Right => PointRight,
                EMapPoint.UpRight => PointUpRight,
                EMapPoint.UpLeft => PointUpLeft,
                EMapPoint.DownRight => PointDownRight,
                EMapPoint.DownLeft => PointDownLeft,
                _ => throw new ArgumentOutOfRangeException(nameof(mapPoint), mapPoint, null)
            };
            return point is null ? null : new MapData(point, DungeonData);
        }
        public bool PointIsNull(EMapPoint mapPoint)=>mapPoint switch
        {

            EMapPoint.Center => NowPointData is null,
            EMapPoint.Up => PointUp  is  null,
            EMapPoint.Down => PointDown  is  null,
            EMapPoint.Left => PointLeft  is  null,
            EMapPoint.Right => PointRight  is  null,
            EMapPoint.UpRight => PointUpRight is  null,
            EMapPoint.UpLeft => PointUpLeft  is  null,
            EMapPoint.DownRight => PointDownRight is  null,
            EMapPoint.DownLeft => PointDownLeft  is  null,
            _ => true
        };

        public MapPointData PointUp => GetMapPointDataByPosition(EMapPoint.Up);
        public MapPointData PointUpRight => GetMapPointDataByPosition(EMapPoint.UpRight);
        public MapPointData PointUpLeft => GetMapPointDataByPosition(EMapPoint.UpLeft);
        public MapPointData PointDown => GetMapPointDataByPosition(EMapPoint.Down);
        public MapPointData PointDownRight => GetMapPointDataByPosition(EMapPoint.DownRight);
        public MapPointData PointDownLeft => GetMapPointDataByPosition(EMapPoint.DownLeft);
        public MapPointData PointRight => GetMapPointDataByPosition(EMapPoint.Right);
        public MapPointData PointLeft => GetMapPointDataByPosition(EMapPoint.Left);
        public MapData MapDataUp => IsNullPointUp ? null :  GetMapData(EMapPoint.UpRight);
        public MapData MapDataUpRight => IsNullPointUp ? null :  GetMapData(EMapPoint.UpRight);
        public MapData MapDataUpLeft => IsNullPointUp ? null :  GetMapData(EMapPoint.UpLeft);
        public MapData MapDataDown => IsNullPointUp ? null :  GetMapData(EMapPoint.Down);
        public MapData MapDataDownRight => IsNullPointUp ? null :  GetMapData(EMapPoint.DownRight);
        public MapData MapDataDownLeft => IsNullPointUp ? null :  GetMapData(EMapPoint.DownLeft);
        public MapData MapDataRight => IsNullPointUp ? null :  GetMapData(EMapPoint.Right);
        public MapData MapDataLeft => IsNullPointUp ? null :  GetMapData(EMapPoint.Left);
        public bool IsNullPointUp => PointIsNull(EMapPoint.Up);
        public bool IsNullPointUpRight => PointIsNull(EMapPoint.UpRight);
        public bool IsNullPointUpLeft => PointIsNull(EMapPoint.UpLeft);
        public bool IsNullPointDown => PointIsNull(EMapPoint.Down);
        public bool IsNullPointDownRight => PointIsNull(EMapPoint.DownRight);
        public bool IsNullPointDownLeft => PointIsNull(EMapPoint.DownLeft);
        public bool IsNullPointRight => PointIsNull(EMapPoint.Right);
        public bool IsNullPointLeft => PointIsNull(EMapPoint.Left);
        
        public bool CanSeeUp => PointUp?.CanSee ?? false;
        public bool CanSeeUpRight => PointUpRight?.CanSee ?? false;
        public bool CanSeeUpLeft => PointUpLeft?.CanSee ?? false;
        public bool CanSeeDown => PointDown?.CanSee ?? false;
        public bool CanSeeDownRight => PointDownRight?.CanSee ?? false;
        public bool CanSeeDownLeft => PointDownLeft?.CanSee ?? false;
        public bool CanSeeLeft => PointLeft?.CanSee ?? false;
        public bool CanSeeRight => PointRight?.CanSee ?? false;
    }
}