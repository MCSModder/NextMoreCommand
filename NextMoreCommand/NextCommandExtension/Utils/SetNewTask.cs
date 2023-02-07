using System;
using JSONClass;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension
{
    [RegisterCommand]
    [DialogEvent("SetNewTask")]
    [DialogEvent("设置新任务")]
    public class SetNewTask : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            var newTask = command.GetInt(0, -1);
            if (newTask > 0 && TaskJsonData.DataDict.ContainsKey(newTask))
            {
                if (!TaskUtils.HasTask(newTask))
                {
                    TaskUtils.AddTask(newTask);
                    MyLog.Log(command, $"[开始添加任务]任务ID：{newTask} 任务名字：{newTask.GetTaskName()}");
                }
                else
                {
                    MyLog.Log(command, $"[已经存在任务]任务ID：{newTask} 任务名字：{newTask.GetTaskName()}");
                }
            }
            else
            {
                MyLog.Log(command, $"[添加任务错误]任务ID不能为{newTask.ToString()}的任务或者该任务不存在", true);
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}