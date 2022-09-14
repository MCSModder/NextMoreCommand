using SkySwordKill.Next;
using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("SetFlowchart")]
    public class SetTempFlowchart : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var key = command.GetStr(0);
            var flowchart = env.flowchart;
            if (flowchart != null && key != string.Empty)
            {
                TempFlowchart.SetFlowchart(key, flowchart);
                MyLog.FungusLog($"成功保存临时Flowchart 为{key}键值");
            }
            else
            {
                MyLog.FungusLogError("环境设置对应Flowchart不存在 或者 储存的键值为空");
            }

            callback?.Invoke();
        }
    }
}