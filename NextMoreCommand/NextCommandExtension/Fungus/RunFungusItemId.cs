using System;
using Fungus;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fungus
{
    [RegisterCommand]
    [DialogEvent("RunFungusItemId")]
    [DialogEvent("运行官方流程对话ID")]
    public class RunFungusItemId : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            var tagBlock = command.GetStr(0);
            var itemId = command.GetInt(1, -1);

            DialogAnalysis.CancelEvent();
            MyLog.Log(command, $"开始调用官方流程对话ID  模块名:{tagBlock} 对话Id:{itemId}");
            if (env.flowchart == null)
            {
                MyLog.Log(command, $"失败调用官方流程对话ID 流程不存在 请检查是不是用Fungus补丁执行该指令",true);
            }
            else
            {
                var flowchart = env.flowchart;
                var flowchartName = flowchart.GetName();
                var index = flowchart.FindIndex(tagBlock, itemId, out var block);
                MyLog.Log(command,$"开始执行官方流程对话ID 流程名:{flowchartName} 模块名:{tagBlock} 对话Id:{itemId}");
                if (block == null)
                {
                      MyLog.Log(command,$"失败执行官方流程对话ID 模块名:{tagBlock} 不存在",true);
                }
                else if (index < 0)
                {
                    var msg = itemId == -1 ? "对话Id不能为空和字符串" : $" {FungusUtils.TalkItemId.ToString()} 对话Id不存在";
                    MyLog.Log(command,$"失败执行官方流程对话ID {msg}",true);
                }
                else
                {
                    MyLog.Log(command,$"官方流程对话ID开始跳转 流程名:{flowchartName} 模块名:{tagBlock} 对话Id:{itemId}");
                    flowchart.ExecuteBlock(block, index);
                    MyLog.Log(command,$"官方流程对话ID跳转完毕 流程名:{flowchartName} 模块名:{tagBlock} 对话Id:{itemId}");
                }
            }


            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}