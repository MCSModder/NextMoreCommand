using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.CustomModData;
using SkySwordKill.NextMoreCommand.Utils;
using XLua;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{
    [DialogEnvQuery("GetRandomChatInfo")]
    public class GetRandomChatInfo : IDialogEnvQuery
    {

        public object Execute(DialogEnvQueryContext context)
        {
            var args = context.Args;
            if (args.Length == 0) return ChatRandomManager.GetRandomChatInfo();
            var list = ChatRandomManager.GetChatInfoList();
            if (list.Count == 0)
            {
                return new ChatInfo();
            }
            if (args[0] is not LuaFunction luaFunction) return ChatRandomManager.GetRandomChatInfo();
            foreach (var info in list)
            {
                var result = luaFunction.Call(info);
                if (result[0] is bool match && match.Equals(true))
                {
                    return info;
                }
            }
            return ChatRandomManager.GetRandomChatInfo();
        }
    }

    [DialogEnvQuery("TryGetChatInfo")]
    public class TryGetChatInfo : IDialogEnvQuery
    {

        public object Execute(DialogEnvQueryContext context)
        {
            var key = context.GetMyArgs(0, "");
            return ChatRandomManager.ChatRandomInfo.TryGetValue(key, out var chatInfo) ? chatInfo : null;
        }
    }

    [DialogEnvQuery("HasChatInfo")]
    public class HasChatInfo : IDialogEnvQuery
    {

        public object Execute(DialogEnvQueryContext context)
        {
            var key = context.GetMyArgs(0, "");
            return ChatRandomManager.ChatRandomInfo.ContainsKey(key);
        }
    }
}