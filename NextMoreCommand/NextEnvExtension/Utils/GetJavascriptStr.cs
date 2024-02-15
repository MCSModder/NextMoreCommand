// using System;
// using SkySwordKill.Next.DialogSystem;
// using SkySwordKill.NextMoreCommand.Puerts;
// using SkySwordKill.NextMoreCommand.Utils;
//
// namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
// {
//
//     [DialogEnvQuery("GetJavascriptValue")]
//     public class GetJavascriptValue : IDialogEnvQuery
//     {
//   
//         public object Execute(DialogEnvQueryContext context)
//         {
//           
//             var scr = context.GetMyArgs(0, "");
//             var funcName =context.GetMyArgs(1, "");
//             
//             return JsEnvManager.Inst.GetJavaScript<object>("dialog", "getEvent",
//                 new object[]
//                 {
//                     scr, funcName, context.Env
//                 });
//         }
//     }
// }

