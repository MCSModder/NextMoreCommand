using System;

namespace SkySwordKill.NextMoreCommand.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomMenPaiShopType : System.Attribute
    {
        public string Name;
        public string ChineseType;

        public CustomMenPaiShopType(string name)
        {
            Name = name;
        }
    }
}