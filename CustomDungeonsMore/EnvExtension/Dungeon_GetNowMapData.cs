using CustomDungeonsMore.Data;
using CustomDungeonsMore.Utils;
using SkySwordKill.Next.DialogSystem;
using UniqueCream.CustomDungeons.Dungeon;

namespace CustomDungeonsMore.EnvExtension
{
    [DialogEnvQuery("Dungeon_GetMapData")]
    public class Dungeon_GetMapData : IDialogEnvQuery
    {


        public object Execute(DialogEnvQueryContext context)
        {
            return DungeonUtils.GetNowMapData();
        }
    }
}