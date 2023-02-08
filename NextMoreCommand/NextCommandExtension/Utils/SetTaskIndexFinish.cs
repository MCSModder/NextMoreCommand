using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [RegisterCommand]
    [DialogEvent("SetTaskIndexFinish")]
    [DialogEvent("设置任务索引完成")]
    public class SetTaskIndexFinish : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            var taskID = command.GetInt(0, -1);
            var index = command.GetInt(1, -1);
            if (taskID < 0 || index < 0)
            {
                MyLog.Log(command, $"[执行失败] 任务ID:{taskID} 任务索引:{index} 不能小于0 ", true);
            }
            else
            {
                var finallyIndex = TaskUtils.GetFinalIndex(taskID);
                MyLog.Log(command, $"任务ID:{taskID} 任务名字:{taskID.GetTaskName()} 当前任务索引:{index} 最终任务索引:{finallyIndex}");


                if (finallyIndex == index)
                {
                    MyLog.Log(command, $"开始设置完成任务 索引:{index}");
                    TaskUtils.SetTaskComplete(taskID);
                    TaskUtils.SetTaskPop(taskID, true);
                }
                else if (finallyIndex > index)
                {
                    MyLog.Log(command, $"开始设置下一步任务 索引:{index }");
                    TaskUtils.SetTaskIndex(taskID, index );
                    TaskUtils.SetTaskPop(taskID);
                }
                else
                {
                    MyLog.Log(command,
                        $"[执行失败] 任务ID:{taskID} 任务名字:{taskID.GetTaskName()} 当前任务索引:{index} 最终任务索引:{finallyIndex}", true);
                }
            }


            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}