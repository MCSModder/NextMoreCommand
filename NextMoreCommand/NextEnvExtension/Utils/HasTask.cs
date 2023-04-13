using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("HasTask")]
    public class HasTask : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var taskID = context.GetMyArgs(0, -1);
            return TaskUtils.HasTask(taskID);

        }
    }
}