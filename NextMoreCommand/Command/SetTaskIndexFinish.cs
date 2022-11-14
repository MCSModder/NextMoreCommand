using Fungus;
using JSONClass;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.Utils;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkySwordKill;
using SkySwordKill.Next;
using KBEngine;
using MarkerMetro.Unity.WinLegacy.Reflection;
using System.Reflection;
using BindingFlags = System.Reflection.BindingFlags;
using Task;
using TaskMag = KBEngine.TaskMag;

namespace SkySwordKill.NextMoreCommand.Command
{
    static class ExtensionTask
    {
        
    }
    [RegisterCommand]
    [DialogEvent("SetTaskIndexFinish")]
    public class SetTaskIndexFinish : IDialogEvent
    {
       TaskMag  taskMag = Tools.instance.getPlayer().taskMag;
        public  void setTaskIndex( int taskId, int index, bool isFinish)
        {
            
            if (!taskMag._TaskData["Task"].HasField(taskId.ToString()))
                return;
            taskMag._TaskData["Task"][taskId.ToString()].SetField("NowIndex", index);
            if (! taskMag._TaskData["Task"][taskId.ToString()]["AllIndex"].ToList().Contains(index))
            {
                taskMag._TaskData["Task"][taskId.ToString()]["AllIndex"].Add(index);
            }
               
            taskMag._TaskData["Task"][taskId.ToString()].SetField("disableTask", isFinish);
            
        }
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.FungusLog("触发SetTaskIndexFinish");
            int taskID = command.GetInt(0, -1);
            int taskIndex = command.GetInt(1, -1);
            
            
            var finalIndex = taskMag.getFinallyIndex(taskID);
            var nowIndex = taskMag.GetTaskNowIndex(taskID);
            MyLog.FungusLog($"taskID:{taskID.ToString()} taskIndex:{taskIndex.ToString()} finalIndex:{finalIndex.ToString()} nowIndex:{nowIndex.ToString()}");
            if (!taskMag.isHasTask(taskID))
            {
                MyLog.FungusLog(string.Format("任务ID为{0}的任务不存在。", taskID));
                return;
            }else if (taskIndex <= 0 )
            {
                MyLog.FungusLog(string.Format("任务序号为{0}的步骤不存在。", taskIndex));
                return;
            }
            if ( nowIndex == finalIndex && taskIndex == nowIndex)
            {
                Fungus.SetTaskIndexFinish.Do(taskID, taskIndex);
                setTaskIndex(taskID,taskIndex,true );
                MyLog.FungusLog(string.Format("已完成任务ID为{0}，任务序号为{1}的步骤。", taskID, taskIndex));
                MyLog.FungusLog(string.Format("任务ID为{0}的任务已完成。", taskID));
                return;
            }
            Fungus.SetTaskIndexFinish.Do(taskID, taskIndex );
                
            setTaskIndex(taskID,taskIndex ,false);
            MyLog.FungusLog(string.Format("已完成任务ID为{0}，任务序号为{1}的步骤。", taskID, taskIndex));
            callback?.Invoke();
        }
    }
}