using System;
using System.Collections.Generic;
using HarmonyLib;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using XLua;

namespace SkySwordKill.NextMoreCommand.Custom
{


    public class CustomEnvMethod : IDialogEnvQuery
    {
        private static readonly Dictionary<string, IDialogEnvQuery> RegisterEnvQueries = Traverse.Create(typeof(DialogAnalysis)).Field<Dictionary<string, IDialogEnvQuery>>("_registerEnvQueries").Value;
        public static CustomEnvMethod CreateByJs(string name, Func<DialogEnvQueryContext, object> getResult)
        {
            var envMethod = new CustomEnvMethod()
            {
                Name = name,
                GetResult = getResult
            };
            RegisterEnvQueries[name] = envMethod;
            return envMethod;
        }
        public static CustomEnvMethod CreateByLua(string name, LuaFunction luaFunction)
        {
            var envMethod = new CustomEnvMethod()
            {
                Name = name,
                GetResultLua = luaFunction
            };
            RegisterEnvQueries[name] = envMethod;
            return envMethod;
        }
        public string                              Name;
        public Func<DialogEnvQueryContext, object> GetResult;
        public LuaFunction                         GetResultLua;
        public object Execute(DialogEnvQueryContext context)
        {
            return GetResult != null ? GetResult.Invoke(context) : GetResultLua?.Func<DialogEnvQueryContext, object>(context);
        }
    }
}