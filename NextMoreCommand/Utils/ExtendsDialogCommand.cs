using System;
using System.Collections.Generic;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class ExtendsDialogCommand
    {
        public static List<T> GetRest<T>(DialogCommand instance, int index, T defaultValue, Func<string, T>
            callback)
        {
            var list = new List<T>();
            if (index >= instance.ParamList.Length)
            {
                list.Add(defaultValue);
            }
            else
            {
                for (var i = index; i < instance.ParamList.Length; i++)
                {
                    var param = instance.ParamList[index];
                    var result = callback(param);
                    list.Add(result);
                }
            }


            return list;
        }

        public static List<int> GetRestInts(
            DialogCommand instance, int index, int defaultValue = 0
        )
        {
           return GetRest(instance, index, defaultValue, (value) => Convert.ToInt32(value));
        }

        public static List<string> GetRestStrings(
            DialogCommand instance, int index, string defaultValue = ""
        )
        {
            return  GetRest(instance, index, defaultValue, (value) => value);
        }
        public static List<bool> GetRestBooleans(
            DialogCommand instance, int index, bool defaultValue = false
        )
        {
            return  GetRest(instance, index, defaultValue, (value) =>  Convert.ToInt32(value) != 0);
        }
        public static List<float> GetRestFloats(
            DialogCommand instance, int index, float defaultValue = 0.0f
        )
        {
            return  GetRest(instance, index, defaultValue, (value) =>  Convert.ToSingle(value));
        }
    }
}