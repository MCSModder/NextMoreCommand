using System;
using System.Linq;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.CustomModData;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("RegisterChatInfo")]
    public class RegisterChatInfo : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var id = command.GetStr(0);
            var condition = command.GetStr(1);
            var @event = command.GetStr(2).Split('|').ToList();
            var priority = command.GetInt(3);
            var bindNpc = command.GetInt(4);
            if (!string.IsNullOrWhiteSpace(id))
            {
                var chat = new ChatInfo()
                {
                    Id = id,
                    Condition = condition,
                    Event = @event,
                    Priority = priority,
                    BindNpc = bindNpc
                };
                chat.Init();
                ChatRandomManager.RegisterChat(chat);
            }
            callback?.Invoke();
        }
    }
}