using System.Collections.Generic;
using System.Linq;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextSeachNpcExtension
{
    [SearchNpcMatch("搜索功法")]
    public class SearchStaticSkill : ISearchNpcMatch
    {

        public List<string> Alias { get; } = new List<string>()
        {
            "staticskill",
            "ss",
            "功",
            "功法"
        };
        public bool Match(SearchNpcDataInfo searchNpcDataInfo)
        {
            var skillList = searchNpcDataInfo.GetStaticSkillList().Select(skill => Tools.instance.getStaticSkillName(skill.skill_ID));
            var skillMatch = searchNpcDataInfo.ValueSplit('|').Where(text => !string.IsNullOrWhiteSpace(text)).Select(text =>
            {
                if (int.TryParse(text, out var id) && SearchNpcDataManager.TryGetStaticSkill(id, out var skill))
                {
                    return Tools.instance.getStaticSkillName(skill.skill_ID);
                }
                return text;
            }).ToList();
            foreach (var skillName in skillList)
            {
                foreach (var match in skillMatch.Where(match => skillName.Contains(match)))
                {
                    MyLog.Log("触发NPC搜索匹配", $"[匹配字段:{match} 功法:{skillName}]");
                    return true;
                }

            }
            return false;
        }
    }
}