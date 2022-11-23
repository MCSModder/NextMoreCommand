using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.CustomMapType
{
    [MapType(nameof(Block),"空地")]
    public class Block:ModCustomMapType
    {
        public override string Type => "Block";
        public override FuBenMap.NodeType NodeType => FuBenMap.NodeType.NULL;
    }
}