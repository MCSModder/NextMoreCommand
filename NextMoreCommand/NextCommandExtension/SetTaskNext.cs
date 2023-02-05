using System;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetTaskNext")]
    [DialogEvent("设置任务下一步")]
    public class SetTaskNext : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            int taskID = command.GetInt(0, -1);
            if (TaskUtils.SetTaskNextIndex(taskID))
            {
               MyPluginMain.LogInfo("触发SetTaskNext");
            }
            callback?.Invoke();
        }
    }
}