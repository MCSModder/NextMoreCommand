using CustomDungeonsMore.Data;
using CustomDungeonsMore.Utils;
using SkySwordKill.Next.DialogSystem;
using UniqueCream.CustomDungeons.Dungeon;

namespace CustomDungeonsMore.EnvExtension
{
    [DialogEnvQuery("Dungeon_GetNowMapData")]
    public class Dungeon_GetNowMapData : IDialogEnvQuery
    {


        public object Execute(DialogEnvQueryContext context)
        {
            return DungeonUtils.GetNowMapData();
        }
    }
}