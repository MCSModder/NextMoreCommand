﻿using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [RegisterCommand]
    [DialogEvent("SetTaskNext")]
    [DialogEvent("设置任务下一步")]
    public class SetTaskNext : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            var taskID = command.GetInt(0, -1);
            if (taskID < 0 || !TaskUtils.HasTask(taskID))
            {
                MyLog.Log(command,
                    $"[执行失败] 任务ID:{taskID} 不能小于0 或者 玩家没有该任务", true);
            }
            else
            {
                var index        = TaskUtils.GetNowIndex(taskID);
                var finallyIndex = TaskUtils.GetFinalIndex(taskID);
                MyLog.Log(command, $"任务ID:{taskID} 任务名字:{taskID.GetTaskName()} 当前任务索引:{index} 最终任务索引:{finallyIndex}");
                if (finallyIndex == 0)
                {
                    MyLog.Log(command,
                        $"[执行失败] 任务ID:{taskID} 任务名字:{taskID.GetTaskName()} 最终任务索引:{finallyIndex} 找不到最终索引的任务 请你检查'IsFinal'字段等于1", true);
                }
                else if (finallyIndex == index)
                {
                    MyLog.Log(command, $"开始设置完成任务 索引:{index}");
                    TaskUtils.SetTaskComplete(taskID);
                    TaskUtils.SetTaskPop(taskID, true);
                }
                else if (finallyIndex > index)
                {
                    MyLog.Log(command, $"开始设置下一步任务 索引:{index + 1}");
                    TaskUtils.SetTaskIndex(taskID, index + 1);
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