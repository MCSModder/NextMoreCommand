using System.Linq;
using JSONClass;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Utils
{

    [DialogEnvQuery("GetExpMax")]
    public class GetExpMax : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var level = context.GetMyArgs(0, -1);
            var dict = LevelUpDataJsonData.DataDict;
            if (level <= 0)
            {
                level = context.Env.GetLevel();
            }
            var maxLevel = LevelUpDataJsonData.DataList.Count;
            if (level >= maxLevel)
            {
                level = LevelUpDataJsonData.DataList[maxLevel- 1].id;
            }

            return dict[level].MaxExp;

        }
    }
}