using SkySwordKill.Next;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public class MyLog
    {
        public static void FungusLog(object msg) =>MyPluginMain.LogInfo($"FungusEvent : {msg}");
        public static void FungusLogError(object msg) =>MyPluginMain.LogError($"FungusEvent : {msg}");
    }
}