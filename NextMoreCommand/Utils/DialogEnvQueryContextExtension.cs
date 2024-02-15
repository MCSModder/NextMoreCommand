using System;
using System.Collections.Generic;
using HarmonyLib;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Patchs;

namespace SkySwordKill.NextMoreCommand.Utils;

public class DialogEnvironmentContext
{
    public DialogEnvironment     RawEnv;
    public IDialogEnvQuery       GetEnvQuery(string                methodName) => DialogAnalysis.GetEnvQuery(methodName);
    public DialogEnvQueryContext GetDialogEnvQueryContext(object[] param)      => new(RawEnv, param);

    public object GetMethod(string methodName, object[] param = default) =>
        GetEnvQuery(methodName)?.Execute(GetDialogEnvQueryContext(param));

    public void     SetInt(string      key, int    value) => DialogAnalysis.SetInt(key, value);
    public void     SetStr(string      key, string value) => DialogAnalysis.SetStr(key, value);
    public object[] GetArray(int       index)                                 => new object[index];
    public Traverse GetTraverse(string name)                                  => Traverse.Create(Type.GetType(name));
    public void     Log(string         pre, object msg, bool isError = false) => MyLog.Log(pre, msg, isError);
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

    public static T GetMyArgs<T>(this DialogEnvQueryContext instance, int index, T defaultValue = default(T))
    {
        if (index >= instance.Args.Length || index < 0)
        {
            return defaultValue;
        }
        var arg = instance.Args[index];
        if (typeof(T) == arg.GetType())
        {
            return (T)arg;
        }
        var result = JToken.FromObject(arg).ToObject<T>();
        return result is null ? defaultValue : result;
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