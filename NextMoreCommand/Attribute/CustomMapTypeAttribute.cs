using System;

namespace SkySwordKill.NextMoreCommand.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomMapTypeAttribute:System.Attribute
    {
        public string Type;
        public string ChineseType;

        public CustomMapTypeAttribute(string type,string chineseType)
        {
            Type = type;
            ChineseType = chineseType;
        }
    }
}