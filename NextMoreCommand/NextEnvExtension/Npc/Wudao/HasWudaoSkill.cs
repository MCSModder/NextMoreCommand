using JSONClass;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc.Wudao
{
    [DialogEnvQuery("HasWudaoSkill")]
    [DialogEnvQuery("检测角色悟道技能")]
    public class HasWudaoSkill : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            var npc = context.GetNpcID(0, 1);
            var wudaoSkill = context.GetMyArgs(1, -1);
            if (npc > 0)
            {
                var wudaoMag = context.Env.player.wuDaoMag;
                switch (npc)
                {
                    case 1:

                        return wudaoMag.GetAllWuDaoSkills().Exists(item => item.itemId == wudaoSkill);
                    default:
                        return wudaoMag.MonstarGetAllWuDaoSkills(npc).Exists(item => item.itemId == wudaoSkill);
                }
            }

            return false;
        }
    }
}