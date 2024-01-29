using System;
using CustomDungeonsMore.Data;
using CustomDungeonsMore.Utils;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UniqueCream.CustomDungeons.Dungeon;

namespace CustomDungeonsMore.EnvExtension
{
    [DialogEnvQuery("Dungeon_GetMapData")]
    public class Dungeon_GetMapData : IDialogEnvQuery
    {


        public object Execute(DialogEnvQueryContext context)
        {
            switch (context.Args.Length)
            {
                case 1:
                    var index = context.GetMyArgs(0, 0);
                    return DungeonUtils.GetMapData(index);
                case >= 2:
                    var x = context.GetMyArgs(0, 0);
                    var y = context.GetMyArgs(1, 0);
                    return DungeonUtils.GetMapData(x, y);
            }
            Main.LogError("地图点不存在！");
            return null;
        }
    }
}