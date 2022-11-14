using SkySwordKill.Next;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public class MyLog
    {
        public static void FungusLog(object msg) => Main.LogInfo($"FungusEvent : {msg}");
        public static void FungusLogError(object msg) => Main.LogError($"FungusEvent : {msg}");
    }
}