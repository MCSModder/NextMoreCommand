﻿using System;
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
            var newTask = command.GetInt(0,-1);
            if (newTask > 0 )
            {
                TaskUtils.AddTask(newTask);
                MyLog.FungusLog($"已添加任务，任务ID：{newTask.ToString()}");
            }
            else
            {
                MyLog.FungusLogError($"任务ID不能为{newTask.ToString()}的任务");
            }
            callback?.Invoke();
        }
    }
}
