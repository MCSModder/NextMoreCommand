using System;
using System.Collections.Generic;
using JSONClass;

namespace SkySwordKill.NextMoreCommand.Utils;

public class Scene
{
    public int     MapIndex  { get; private set; }
    public string  Name      { get; private set; }
    public string  SceneName { get; private set; }
    public MapArea MapArea   { get; set; }

    public Scene(string name, string scene, int mapIndex, MapArea mapArea)
    {
        Name = name;
        SceneName = scene;
        MapIndex = mapIndex;
        MapArea = mapArea;
    }
}

public static class SceneUtils
{
    public static readonly List<Scene> Scenes = new List<Scene>();
    public static void Init()
    {
        foreach (var scene in SceneNameJsonData.DataList)
        {
            MapArea mapArea;
            switch (scene.MoneyType)
            {
                case 1:
                    mapArea = MapArea.NingZhou;
                    break;
                case 2:
                case 3:
                    mapArea = MapArea.Sea;
                    break;
                default:
                    mapArea = MapArea.Unknow;
                    break;

            }

            int mapIndex = 0;
            if (scene.id.StartsWith("S"))
            {
                mapIndex = Convert.ToInt32(scene.id.Replace("S", String.Empty));
            }
            else if (scene.id.StartsWith("F"))
            {
                mapIndex = Convert.ToInt32(scene.id.Replace("F", String.Empty));
            }
            else if (scene.id.StartsWith("Sea"))
            {
                mapIndex = Convert.ToInt32(scene.id.Replace("Sea", String.Empty));
            }

            Scenes.Add(new Scene(scene.MapName, scene.id, mapIndex, mapArea));
        }
    }
}