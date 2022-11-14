using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBEngine;


namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("SetNewTask")]
    public class SetNewTask : IDialogEvent
    {
        private TaskMag TaskManager => PlayTutorial.Player.taskMag;
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var newTask = command.GetInt(0,-1);
            if (newTask > 0 )
            {
                TaskManager.addTask(newTask);
                MyLog.FungusLog($"已添加任务，任务ID：{newTask.ToString()}");
            }
            else
            {
                MyLog.FungusLogError($"任务ID不能为{newTask.ToString()}的任务");
            }
        }
    }
}
