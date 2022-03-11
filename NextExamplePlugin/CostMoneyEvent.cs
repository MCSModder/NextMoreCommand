using System;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;

namespace SkySwordKill.NextCommandExamplePlugin
{
    public class CostMoneyEvent : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            // 获取参数 - 该处获取第一个参数，转换为int
            var money = command.GetInt(0);
            if (env.GetMoney() > (ulong)money)
            {
                // 将临时变量CostMoney设置为1
                // 可以使用 [&GetArg("CostMoney")&] 来获取临时变量
                env.tmpArgs["CostMoney"] = 1;
                // 执行自定义单行指令
                var newCommandData = new DialogCommand("ChangeMoney*" + money, command.BindEventData, env, command.IsEnd);
                // 使用执行的指令运行回调
                DialogAnalysis.RunDialogEventCommand(newCommandData, env, callback);
            }
            else
            {
                // 指令结束一定要调用回调，否则无法运行下一条指令
                callback?.Invoke();
            }
        }
    }
}