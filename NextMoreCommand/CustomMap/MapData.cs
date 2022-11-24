using System.Collections.Generic;
using System.Linq;
using SkySwordKill.NextMoreCommand.CustomMap.CustomMapType;

namespace SkySwordKill.NextMoreCommand.CustomMap;

public enum MapNodeType
{
    Null,
    Road,
    Exit,
    Entrance,
}

public struct Position
{
    public Position(int x, int y, int index)
    {
        X = x;
        Y = y;
        Index = index;
    }

    public int X { get; }
    public int Y { get; }
    public int Index { get; }
    public MapNodeType Type { get; set; } = MapNodeType.Null;
}

public class NextEvent
{
    public NextEvent(int index, string id,string cid)
    {
        Index = index;
        ID = id;
        CID = cid;
    }
    public int Index;
    public string ID;
    public string CID;
}
public class MyBlock
{
    public MyBlock(int index, string name)
    {
        Index = index;
        Name = name;
    }
    public int Index;
    public string Name;
}
public class MapData
{
    public MapNodeType[,] Map { get; private set; }
    public int[,] MapIndex { get; private set; }
    public List<Position> Positions { get; private set; }

    public int High { get; private set; }
    public int Wide { get; private set; }
    public List<CustomMapAward> Award { get; private set; }
    public List<CustomMapEvent> Event { get; private set; }
    public List<Position> Exit { get; private set; }
    public List<Position> Entrance { get; private set; }
    public List<NextEvent> NextTrigger { get; private set; }
    public List<NextEvent> NextDialog { get; private set; }
    public List<MyBlock> Blocks{ get; private set; }
    public MapData(int high, int wide)
    {
        High = high;
        Wide = wide;
        Map = new MapNodeType[Wide, High];
        MapIndex = CreateMap(Wide, High);
        Award = new List<CustomMapAward>();
        Event = new List<CustomMapEvent>();
        Exit = new List<Position>();
        Entrance = new List<Position>();
        Positions = CreatePosition(wide, high);
        NextTrigger = new List<NextEvent>();
        NextDialog = new List<NextEvent>();
        Blocks = new List<MyBlock>();
    }

    public void CreateExit(int x, int y)
    {
        CreateNode(x, y, MapNodeType.Exit, out var position);
        Exit.Add(position);
    }

    public void CreateExit(int index)
    {
        CreateNode(index, MapNodeType.Exit, out var position);
        Exit.Add(position);
    }

    public void CreateEntrance(int index)
    {
        CreateNode(index, MapNodeType.Entrance, out var position);
        Entrance.Add(position);
    }

    public void CreateEntrance(int x, int y)
    {
        CreateNode(x, y, MapNodeType.Entrance, out var position);
        Entrance.Add(position);
    }

    public void CreateNode(int x, int y, MapNodeType type, out Position position)
    {
        position = GetPosition(x, y);
        position.Type = type;
        Map[position.X, position.Y] = type;
    }

    public void CreateNode(int index, MapNodeType type, out Position position)
    {
        position = GetPosition(index);
        position.Type = type;
        Map[position.X, position.Y] = type;
    }

    public void CreateNode(int index, MapNodeType type)
    {
        var position = GetPosition(index);
        position.Type = type;
        Map[position.X,  position.Y] = type;
    }

    public void CreateNode(int x, int y, MapNodeType type)
    {
        var position = GetPosition(x, y);
        position.Type = type;
        Map[position.X,  position.Y] = type;
    }



    public void GetIndexPosition(int index, out int x, out int y)
    {
        var position = GetPosition(index);
        x = position.X;
        y = position.Y;
    }

    public int GetIndex(int x, int y) => GetPosition(x, y).Index;
    public Position GetPosition(int x, int y) => Positions.First(position => position.X == x && position.Y == y);

    public Position GetPosition(int index) => Positions.First(position => position.Index == index);

    public static List<Position> CreatePosition(int wide, int high)
    {
        var list = new List<Position>();
        int index = 1;
        for (int x = 0; x < high; x++)
        {
            for (int y = 0; y < wide; y++)
            {
                list.Add(new Position(y, x, index));
                index++;
            }
        }

        return list;
    }

    public static int[,] CreateMap(int wide, int high)
    {
        int[,] array = new int[wide, high];
        int num = 1;
        for (int x = 0; x < high; x++)
        {
            for (int y = 0; y < wide; y++)
            {
                array[y, x] = num;
                num++;
            }
        }

        return array;
    }
}