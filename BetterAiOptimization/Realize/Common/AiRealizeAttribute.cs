using System;

namespace BetterAiOptimization.Data.Common
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AiRealizeAttribute: System.Attribute
    {
        public string Seid;

        public AiRealizeAttribute(string seid)
        {
            Seid = seid;
        }
    }
}