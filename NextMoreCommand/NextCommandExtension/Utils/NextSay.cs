using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Puerts;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Puerts;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{

    [DialogEvent("NextSay")]
    public class NextSay : IDialogEvent
    {
        private List<string> field;
        public string DealSayText(string say, DialogEnvironment env)
        {
            var text = new StringBuilder(say);
            var traverse = Traverse.Create(env);
            field ??= traverse.Fields();
            
            foreach (var key in field)
            {
                var replaceKey = $"{{{{{key}}}}}";
                if(!text.ToString().Contains(replaceKey)) continue;
                text = text.Replace(replaceKey, (string)traverse.Field(key).GetValue());
            }
            return text.ToString();
        }
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            var charId = command.GetStr(0);
            var say = command.GetStr(1);
            command.LogInfos($"charId:{charId} say:{say}");
            // 处理对话角色ID
            if (!command.BindEventData.Character.TryGetValue(charId, out var charNum))
            {
                if (!DialogAnalysis.TmpCharacter.TryGetValue(charId, out charNum))
                {

                    charNum = 0;
                }
            }

            command.LogInfos($"charNum:{charNum} say:{say}");
            var text = DialogAnalysis.DealSayText(say, charNum);
            text = DealSayText(text, env);
            command.LogInfos($"say:{say} text:{text}");
            command.LogInfos($"callback:{callback} ");
            DialogAnalysis.SetCharacter(charNum);
            DialogAnalysis.Say(text, () => { callback?.Invoke(); });
            MyLog.LogCommand(command, false);
        }
    }

}