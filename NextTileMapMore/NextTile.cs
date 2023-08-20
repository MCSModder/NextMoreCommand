
using UnityEngine;
using UnityEngine.Tilemaps;

namespace NextTileMapMore
{
    public class NextTile:Tile
    {
        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            return base.StartUp(position, tilemap, go);
        }
    }
}