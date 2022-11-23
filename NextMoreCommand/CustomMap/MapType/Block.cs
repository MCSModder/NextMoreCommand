using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.MapType
{
    [MapType(nameof(Block),"空")]
    public class Block:ModCustomMapType
    {
        public override string Type => "Block";
        public override FuBenMap.NodeType NodeType => FuBenMap.NodeType.NULL;
    }
}