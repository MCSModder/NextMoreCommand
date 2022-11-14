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

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("SetTaskNext")]
    public class SetTaskNext : IDialogEvent
    {
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
                if ( nowIndex  < finalIndex)
                {
                    Fungus.SetTaskIndexFinish.Do(taskID, nextIndex);
                    taskMag.setTaskIndex(taskID,nextIndex,false);
                }else if (nowIndex == finalIndex)
                {
                    
                    Fungus.SetTaskIndexFinish.Do(taskID, nowIndex);
                    taskMag.setTaskIndex(taskID,nowIndex,true);
                }
               
            }
            callback?.Invoke();
        }
    }
}