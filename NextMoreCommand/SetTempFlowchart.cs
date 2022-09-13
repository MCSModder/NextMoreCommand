using SkySwordKill.Next;
using System;

using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
namespace SkySwordKill.NextMoreCommand
{
   
    [DialogEvent("SetFlowchart")]
    public class SetTempFlowchart:IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            string key = command.GetStr(0);
            var flowchart = env.flowchart;
            if (flowchart != null)
            {
                TempFlowchart.SetFlowchart(key,flowchart);
            }
           
            callback?.Invoke();
        }
    }
}