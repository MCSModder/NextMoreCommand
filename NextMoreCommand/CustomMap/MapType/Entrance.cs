using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.MapType
{
    [MapType(nameof(Entrance),"入口")]
    public class Entrance:ModCustomMapType
    {
        public override string Type => "Entrance";
        public override MapNodeType NodeType => MapNodeType.Entrance;
       
    }
}