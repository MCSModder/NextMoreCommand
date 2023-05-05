using System;

namespace SkySwordKill.NextMoreCommand.Attribute
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ModDataAttribute: System.Attribute
    {
        public string Name;

        public ModDataAttribute(string name)
        {
            Name = name;
        }
    }
}