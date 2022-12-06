using SkySwordKill;
using SkySwordKill.Next;
using System;
using Fungus;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Command
{
    [RegisterCommand]
    [DialogEvent("RunFungusTalkItemId")]
    public class RunFungusTalkItemId : IDialogEvent
    {
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var talkID = command.GetInt(0, -1);
            var tagBlock = command.GetStr(1);
            var itemId = command.GetInt(2, -1);

            DialogAnalysis.CancelEvent();


            if (FungusUtils.GetTalk(talkID) == null)
            {
                Main.LogError($"FungusEvent : 对应Talk{talkID.ToString()} flowchart不存在");
            }
            else
            {
                FungusUtils.TalkBlockName = tagBlock;
                FungusUtils.TalkItemId = itemId;
                FungusUtils.TalkFunc = flowchart =>
                {
                    var index = flowchart.FindIndex(FungusUtils.TalkBlockName, FungusUtils.TalkItemId, out Block block);
                    if (block == null)
                    {
                        MyLog.FungusLogError($"Block名称不存在 {FungusUtils.TalkBlockName}");
                        return false;
                    }

                    if (index < 0)
                    {
                        var msg = itemId == -1 ? "ItemId不能为空和字符串" : $"ItemId不存在 {FungusUtils.TalkItemId.ToString()}";
                        MyLog.FungusLogError(msg);
                        return false;
                    }

                    flowchart.ExecuteBlock(block, index);
                    Main.LogInfo(
                        $"FungusEvent : 跳转FungusBlock {FungusUtils.TalkBlockName} ItemId:{FungusUtils.TalkItemId.ToString()} index:{index.ToString()} ");
                    return true;
                };
                FungusUtils.isTalkActive = true;
            }


            callback?.Invoke();
        }
    }
}