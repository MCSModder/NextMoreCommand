using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using XLua;

namespace SkySwordKill.NextMoreCommand.Custom
{
    public class CustomCommand : IDialogEvent
    {
        public static CustomCommand CreateByJs(string name, Action<DialogCommand, DialogEnvironment> onExecute)
        {
            var command = new CustomCommand()
            {
                Name = name,
                OnExecute = onExecute
            };
            DialogAnalysis.RegisterCommand(name, command);
            return command;
        }
        public static CustomCommand CreateByLua(string name, LuaFunction onExecuteLua)
        {
            var command = new CustomCommand()
            {
                Name = name,
                OnExecuteLua = onExecuteLua
            };
            DialogAnalysis.RegisterCommand(name, command);
            return command;
        }
        public string                                   Name;
        public Action<DialogCommand, DialogEnvironment> OnExecute;
        public LuaFunction                              OnExecuteLua;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            if (OnExecuteLua != null)
            {
                OnExecuteLua.Call(command, env);
            }
            else
            {
                OnExecute?.Invoke(command, env);
            }
            callback?.Invoke();
        }
    }
}