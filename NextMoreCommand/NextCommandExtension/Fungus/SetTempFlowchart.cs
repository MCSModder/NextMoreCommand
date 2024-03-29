﻿using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fungus
{
    [RegisterCommand]
    [DialogEvent("SetFlowchart")]
    [DialogEvent("设置临时对话")]
    public class SetTempFlowchart : IDialogEvent
    {
        string key;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            key = command.GetStr(0);
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