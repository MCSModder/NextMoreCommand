using System;
using System.Collections.Generic;
using Fungus;
using JSONClass;
using KBEngine;

namespace SkySwordKill.NextMoreCommand.Utils
{
    public class TaskData
    {
        public TaskData(int taskId)
        {
            this.taskId = taskId;
        }

        private int taskId;
        public int TaskID => taskId;
        public int FinalIndex => TaskUtils.GetFinalIndex(taskId);
        public int NowIndex => TaskUtils.GetNowIndex(taskId);
        public JSONObject TaskIdData => TaskUtils.GetTaskIdData(taskId);
        public bool HasTask() => TaskUtils.HasTask(taskId);
        public bool NotHasTask() => !HasTask();
    }

    public static class TaskUtils
    {
        private static Avatar player => Tools.instance.getPlayer();
        public static TaskMag TaskMag => player.taskMag;
        public static JSONObject TaskData => TaskMag._TaskData;
        public static JSONObject Task => TaskData["Task"];
        public static JSONObject GetTaskIdData(int taskId) => Task[taskId.ToString()];
        public static int GetFinalIndex(int taskID) => TaskMag.getFinallyIndex(taskID);
        public static int GetNowIndex(int taskID) => TaskMag.GetTaskNowIndex(taskID);
        public static bool HasTask(int taskID) => TaskMag.isHasTask(taskID);
        public static bool NotHasTask(int taskID) => !HasTask(taskID);

        public static bool IsOverFinalIndex(int taskID, int index) => index > GetFinalIndex(taskID);

        public static bool AddTask(int taskID)
        {
            TaskMag.addTask(taskID);
            return NotHasTask(taskID);
        }

        public static bool SetTaskIndex(int taskId, int taskIndex, bool isFinish = false)
        {
            var taskIdData = GetTaskIdData(taskId);

            if (!taskIdData["AllIndex"].ToList().Contains(taskIndex))
            {
                taskIdData["AllIndex"].Add(taskIndex);
            }

            taskIdData.SetField("NowIndex", taskIndex);
            taskIdData.SetField("isComplete", isFinish);
            taskIdData.SetField("disableTask", isFinish);
            return true;
        }

        public static void SetTaskComplete(int taskId)
        {
            SetTaskIndexFinish.Do(taskId, GetFinalIndex(taskId));
            SetTaskCompelet.Do(taskId);
        }

        public static bool SetTaskNextIndex(int taskId)
        {
            if (NotHasTask(taskId))
            {
                return false;
            }

            var finalIndex = GetFinalIndex(taskId);
            var nowIndex = GetNowIndex(taskId);
            var nextIndex = nowIndex + 1;
            var isFinish = nowIndex == finalIndex;
            var index = nextIndex > finalIndex ? nowIndex : nextIndex;
            SetTaskNextIndex(taskId, index, isFinish);
            return true;
        }

        public static bool SetTaskNextIndex(int taskId, int taskIndex, bool isFinish = false)
        {
            if (NotHasTask(taskId) || IsOverFinalIndex(taskId, taskIndex))
            {
                return false;
            }


            MyLog.FungusLog("开始执行SetTaskIndex");
            if (SetTaskIndex(taskId, taskIndex, isFinish))
            {
                MyLog.FungusLog("成功执行SetTaskIndex");
            }
            else
            {
                MyLog.FungusLog("失败执行SetTaskIndex");
            }

            SetTaskPop(taskId, isFinish);
            return true;
        }

        public static string GetTaskNameById(int taskId) => TaskJsonData.DataDict[taskId].Name;
        public static string GetTaskName(this int taskId) => GetTaskNameById(taskId);

        public static void SetTaskPop(int taskId, bool isFinish = false)
        {
            var name = TaskJsonData.DataDict[taskId].Name;
            var isFinishMsg = isFinish ? "已完成" : "进度已更新";
            var msg = $"<color=#FF0000> {name} </color> {isFinishMsg}";
            var popTips = isFinish ? PopTipIconType.任务完成 : PopTipIconType.任务进度;
            UIPopTip.Inst.Pop(msg, popTips);
        }
    }
}