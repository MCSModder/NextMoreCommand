using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using System;
using SkySwordKill.Next;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("SetTaskIndexFinish")]
    public class SetTaskIndexFinish : IDialogEvent
    {
    
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            int taskID = command.GetInt(0, -1);
            int taskIndex = command.GetInt(1, -1);
            
            
            var finalIndex = TaskUtils.GetFinalIndex(taskID);
            var nowIndex = TaskUtils.GetNowIndex(taskID);
            
            MyLog.FungusLog($"taskID:{taskID.ToString()} taskIndex:{taskIndex.ToString()} finalIndex:{finalIndex.ToString()} nowIndex:{nowIndex.ToString()}");
            if (TaskUtils.NotHasTask(taskID) ||taskIndex <= 0  )
            {
                MyLog.FungusLog(string.Format("任务ID为{0}的任务不存在。", taskID.ToString()));
                return;
            }

            if (TaskUtils.IsOverFinalIndex(taskID,taskIndex))
            {
                MyLog.FungusLog(string.Format("任务ID为{0}的任务,索引为{1}超出范围。", taskID.ToString(),taskIndex.ToString()));
            }
            var isFinish = nowIndex == finalIndex && taskIndex == nowIndex;
            var index = isFinish ? finalIndex : taskIndex;
            MyLog.FungusLog($"isFinish:{isFinish.ToString()} index:{index.ToString()}");
            if (TaskUtils.SetTaskNextIndex(taskID, index, isFinish))
            {
                MyLog.FungusLog(string.Format("已完成任务ID为{0}，任务序号为{1}的步骤。", taskID.ToString(), taskIndex.ToString()));
                if (isFinish)MyLog.FungusLog(string.Format("任务ID为{0}的任务已完成。", taskID.ToString()));
            }
            callback?.Invoke();
        }
    }
}