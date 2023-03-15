using System;
using System.Linq;
using System.Text;
using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using XLua;
using static SkySwordKill.NextMoreCommand.MyPluginMain;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class MyLog
    {
        public static string PadRightEx(this string msg, int totalWidth)
        {
            var coding = Encoding.GetEncoding("UTF-8");
            var count = msg.ToCharArray().Count(ch => coding.GetByteCount(ch.ToString()) > 1);

            return msg.PadRight(totalWidth - count);
        }

        public static string PadRightEx(this string msg, int totalWidth, char paddingChar)
        {
            var coding = Encoding.GetEncoding("UTF-8");
            var count = 0;
            foreach (var ch in msg.ToCharArray())
            {
                if (coding.GetByteCount(ch.ToString()) > 1)
                {
                    count++;
                }
            }

            return msg.PadRight(totalWidth - count, paddingChar);
        }

        public static string FungusEvent(this object msg) => $"[{"FungusEvent".PadRightEx(16)}] {msg}";
        public static string LogEvent(this object msg, string pre) => $"[{pre.PadRightEx(16)}] {msg}";
        public static void Log(string msg, bool isError = false) => Log("开始执行指令", msg, isError);
        public static void Log(object msg, bool isError = false) => Log("开始执行指令", msg, isError);
        public readonly static string Split = "".PadRight(80, '=');

        public static void LogCommand(DialogCommand command, bool isStart = true)
        {
            if (isStart)
            {
                LogStartCommand(command);
            }
            else
            {
                LogFinishCommand(command);
            }
        }

        public static void LogStartCommand(DialogCommand command)
        {
            LogInfo(Split);
            LogStatus($"当前剧情ID: {command.BindEventData.ID}".LogEvent(command.Command));
            LogInfo($"[{command.Command}] 指令".LogEvent("开始执行指令"));
        }

        public static void LogFinishCommand(DialogCommand command)
        {
            LogStatus($"[{command.Command}] 指令".LogEvent("结束执行指令"));
            LogInfo(Split);
        }

        public static void LogStatus(object str, bool isError = false)
        {
            if (!MyPluginMain.IsDebugMode)
            {
                return;
            }
            if (isError)
            {
                LogError(str);
            }
            else
            {
                LogInfo(str);
            }
        }

        public static void Log(DialogCommand command, object msg, bool isError = false, bool canShow = true)
        {
            if (isError)
            {
                LogStatus($"剧情ID: {command.BindEventData.ID}".LogEvent(command.Command), canShow);
                LogStatus($"指令文本:{command.RawCommand}".LogEvent(command.Command), canShow);
            }

            LogStatus(msg.LogEvent(command.Command), isError);
        }

        public static void Log(string pre, object msg, bool isError = false)
        {
            LogStatus(msg.LogEvent(pre), isError);
        }

        public static void FungusLog(object msg) => LogInfo(msg.FungusEvent());
        public static void FungusLogError(object msg) => LogError(msg.FungusEvent());
        public  static void DoString(this string instance)
        {
            var obj = Main.Lua.LuaEnv.DoString(Convert.FromBase64String(instance));
            if (obj.Length != 0 && obj[0] is LuaTable luaTable)
            {
                var list = Traverse
                    .Create(Type.GetType("SkySwordKill.NextMoreCommand.Patchs.DialogAnalysisTryTriggerPatch"))
                    .Field("OnTryTrigger");
                luaTable.Get<LuaFunction>("Init").Call(list.GetValue());
            }
        }
    }
  
}