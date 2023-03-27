using System;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Puerts;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetJavascriptStr")]
    public class GetJavascriptStr : IDialogEnvQuery
    {
        private IDialogEnvQuery _dialogEnvQueryImplementation;
        public void Execute()
        {

          
        }
        public object Execute(DialogEnvQueryContext context)
        {
            var scr = context.GetMyArgs(0, "");
            var funcName =context.GetMyArgs(1, "");
            JsEnvManager.Inst.RunJavaScript("dialog", "getEvent",
                new object[]
                {
                    scr, funcName, context.Env
                });
            return _dialogEnvQueryImplementation.Execute(context);
        }
    }
}
