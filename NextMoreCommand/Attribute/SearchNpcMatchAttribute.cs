using System;

namespace SkySwordKill.NextMoreCommand.Attribute
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SearchNpcMatchAttribute : System.Attribute
    {
        public string Name;

        public SearchNpcMatchAttribute(string name)
        {
            Name = name;
        }
    }
}