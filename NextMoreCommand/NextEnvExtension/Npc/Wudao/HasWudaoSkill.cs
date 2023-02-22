using JSONClass;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextEnvExtension.Npc.Wudao
{
    [DialogEnvQuery("HasWudaoSkill")]
    [DialogEnvQuery("检测角色悟道技能")]
    public class HasWudaoSkill : IDialogEnvQuery
    {
        private DialogEnvQueryContext _context;

        public object Execute(DialogEnvQueryContext context)
        {
            _context = context;

            switch (context.Args.Length)
            {
                case 1:
                    var wudaoSkill = _context.GetMyArgs(0, -1);
                    return context.Env.player.wuDaoMag.IsStudy(wudaoSkill);
                case 2:
                    return GetNpcWudao();
            }


            return false;
        }

        public object GetNpcWudao()
        {
            var npc = _context.GetNpcID(0, 1);
            var wudaoSkill = _context.GetMyArgs(1, -1);
            if (npc > 0)
            {
                var wudaoMag = _context.Env.player.wuDaoMag;
                switch (npc)
                {
                    case 1:

                        return wudaoMag.IsStudy(wudaoSkill);
                    default:
                        return wudaoMag.MonstarGetAllWuDaoSkills(npc).Exists(item => item.itemId == wudaoSkill);
                }
            }

            return false;
        }
    }
}