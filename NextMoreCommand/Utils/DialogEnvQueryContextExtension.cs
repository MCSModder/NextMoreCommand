using System;
using HarmonyLib;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Patchs;
using SkySwordKill.NextMoreCommand.Utils.Npc;

namespace SkySwordKill.NextMoreCommand.Utils;

public class DialogEnvironmentContext
{
    public DialogEnvironment RawEnv;
    public IDialogEnvQuery GetEnvQuery(string methodName) => DialogAnalysis.GetEnvQuery(methodName);
    public DialogEnvQueryContext GetDialogEnvQueryContext(object[] param) => new(RawEnv, param);

    public object GetMethod(string methodName, object[] param = default) =>
        GetEnvQuery(methodName)?.Execute(GetDialogEnvQueryContext(param));

    public void SetInt(string key, int value) => DialogAnalysis.SetInt(key, value);
    public void SetStr(string key, string value) => DialogAnalysis.SetStr(key, value);
    public object[] GetArray( int index) => new object[index];
    public Traverse GetTraverse(string name)=>Traverse.Create(Type.GetType(name));
    public void Log(string pre, object msg, bool isError = false)=>MyLog.Log(pre, msg,  isError);
    public DialogEnvironmentContext()
    {
        RawEnv = new DialogEnvironment();
     
    }
    public DialogEnvironmentContext(DialogEnvironment env)
    {
        RawEnv = env;
     
    }
}

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

    public static DialogEnvironmentContext GetDialogEnvironmentContext(this DialogEnvironment instance)
    {
        return new DialogEnvironmentContext(instance);
    }
}