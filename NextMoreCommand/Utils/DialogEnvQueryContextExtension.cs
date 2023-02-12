using System;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class DialogEnvQueryContextExtension
{
    public static T GetMyArgs<T>(this DialogEnvQueryContext instance, int index, T defaultValue = default)
    {
        var args = JArray.FromObject(instance.Args);
        if (index >= args.Count || index < 0)
        {
            return defaultValue;
        }

        if (typeof(T) == typeof(int) && instance.Args[index] is string value)
        {
            if (!int.TryParse(value, out int _))
            {
                return defaultValue;
            }
        }

        var result = args[index].ToObject<T>();
        return result == null ? defaultValue : result;
    }

    public static int GetNpcID(this DialogEnvQueryContext instance, int index, int defaultValue = default)
    {
        return instance.GetMyArgs(index, defaultValue).ToNpcNewId();
    }
}