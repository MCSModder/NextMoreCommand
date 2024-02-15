using System;

namespace SkySwordKill.NextMoreCommand.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MapTypeAttribute : System.Attribute
    {
        public string Type;
        public string ChineseType;

        public MapTypeAttribute(string type, string chineseType)
        {
            Type = type;
            ChineseType = chineseType;
        }
    }
}