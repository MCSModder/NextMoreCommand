using SkySwordKill.Next;

namespace SkySwordKill.NextMoreCommand
{
    public class MyLog
    {
        public static void FungusLog(string msg) => Main.LogInfo($"FungusEvent : {msg}");
        public static void FungusLogError(string msg) => Main.LogError($"FungusEvent : {msg}");
    }
}