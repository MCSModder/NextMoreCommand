// using System;
// using HarmonyLib;
// using Puerts;
// using SkySwordKill.Next;
// using SkySwordKill.Next.DialogEvent;
// using SkySwordKill.Next.DialogSystem;
// using SkySwordKill.NextMoreCommand.Puerts;
//
// namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
// {
//     [DialogEvent("RunJavaScript")]
//     [DialogEvent("运行JavaScript")]
//     [DialogEvent("RunJS")]
//     [DialogEvent("运行JS")]
//     public class RunJavaScript: IDialogEvent
//     {
//         public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
//         {
//            
//             var scr = command.GetStr(0);
//             var funcName = command.GetStr(1);
//             JsEnvManager.Inst.RunJavaScript("dialog", "runEvent",
//                 new object[] { scr, funcName, command, env, callback });
//         }
//     }
//  
// }