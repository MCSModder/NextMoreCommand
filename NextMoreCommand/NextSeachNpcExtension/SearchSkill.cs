using System.Collections.Generic;
using System.Linq;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextSeachNpcExtension
{
    [SearchNpcMatch("搜索神通")]
    public class SearchSkill : ISearchNpcMatch
    {

        public List<string> Alias { get; } = new List<string>()
        {
            "skill",
            "sk",
            "神",
            "神通"
        };
        public bool Match(SearchNpcDataInfo searchNpcDataInfo)
        {
            var skillList = searchNpcDataInfo.GetSkillList().Select(skill =>  Tools.instance.getSkillName(skill.skill_ID));
            var skillMatch = searchNpcDataInfo.ValueSplit('|').Where(text => !string.IsNullOrWhiteSpace(text)).Select(text =>
            {
                if (int.TryParse(text, out var id) && SearchNpcDataManager.TryGetSkill(id, out var skill))
                {
                    return Tools.instance.getSkillName(skill.skill_ID);
                }
                return text;
            }).ToList();
            foreach (var skillName in skillList)
            {
                foreach (var match in skillMatch.Where(match => skillName.Contains(match)))
                {
                    MyLog.Log("触发NPC搜索匹配",$"[匹配字段:{match} 神通:{skillName}]");
                    return true;
                }

            }
            return false;
        }
    }
}