using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.MapType
{
    [MapType(nameof(Exit),"出口")]
    public class Exit:ModCustomMapType
    {
        public override string Type => "Exit";
        public override MapNodeType NodeType =>MapNodeType.Exit;
    }
}