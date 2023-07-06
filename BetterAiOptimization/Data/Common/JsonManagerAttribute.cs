using System;

namespace BetterAiOptimization.Data.Common
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class JsonManagerAttribute: System.Attribute
    {
        public string Seid;

        public JsonManagerAttribute(string seid)
        {
            Seid = seid;
        }
    }
}