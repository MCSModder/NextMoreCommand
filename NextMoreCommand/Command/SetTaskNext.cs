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
    [RegisterCommand]
    [DialogEvent("SetTaskNext")]
    public class SetTaskNext : IDialogEvent
    {
        public   TaskMag  taskMag => Tools.instance.getPlayer().taskMag;
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
            MyLog.FungusLog("触发SSetTaskNext");
            int taskID = command.GetInt(0, -1);
            
            var taskMag = Tools.instance.getPlayer().taskMag;
            var finalIndex = taskMag.getFinallyIndex(taskID);
            var nowIndex = taskMag.GetTaskNowIndex(taskID);
            var nextIndex = nowIndex + 1;
            if (nowIndex > 0 )
            {
                if (nowIndex == finalIndex )
                {
                    
                    Fungus.SetTaskIndexFinish.Do(taskID, nowIndex);
                    setTaskIndex(taskID,nowIndex,true);
                    return;
                }
                Fungus.SetTaskIndexFinish.Do(taskID, nextIndex);
                setTaskIndex(taskID,nextIndex,false);
               
            }
            callback?.Invoke();
        }
    }
}