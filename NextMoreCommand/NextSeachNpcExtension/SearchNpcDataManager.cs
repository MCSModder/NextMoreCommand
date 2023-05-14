using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GUIPackage;
using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.NextSeachNpcExtension
{
    public static class SearchNpcDataManager
    {
        public static readonly Dictionary<string, ISearchNpcMatch> SearchNpcMatchEvent = new Dictionary<string, ISearchNpcMatch>();

        public static void RegisterSearchNpcMatch(string key, ISearchNpcMatch searchNpcMatch) => SearchNpcMatchEvent[key] = searchNpcMatch;

        public static void Init()
        {
            foreach (var types in AppDomain.CurrentDomain.GetAssemblies()
                         .Select(assembly => assembly.GetTypes()))
            {
                foreach (var type in types)
                {
                    if (!typeof(ISearchNpcMatch).IsAssignableFrom(type)) continue;
                    foreach (var attribute in type.GetCustomAttributes<SearchNpcMatchAttribute>())
                    {

                        var key = attribute.Name;
                        RegisterSearchNpcMatch(key, Activator.CreateInstance(type) as ISearchNpcMatch);
                    }
                }
            }
        }
        public static bool Match(SearchNpcDataInfo searchNpcDataInfo)
        {
            foreach (var searchNpcMatch in SearchNpcMatchEvent)
            {
                var name = searchNpcMatch.Key;
                var value = searchNpcMatch.Value;
                if (!value.Alias.Contains(searchNpcDataInfo.Key) || !value.Match(searchNpcDataInfo)) continue;
                //MyPluginMain.LogInfo($"[触发NPC搜索匹配][{name}]");
                return true;
            }
            return false;
        }
        // public delegate bool SearchNpcDataMatch(SearchNpcDataInfo searchNpcData);
        //
        // public static event SearchNpcDataMatch OnSearchNpcDataMatch;
        public static SkillDatebase SkillDataBase => SkillDatebase.instence;
        public static SkillStaticDatebase StaticSkillDataBase => SkillStaticDatebase.instence;
        public static ItemDatebase ItemDataBase => ItemDatebase.Inst;
        public static bool TryGetSkill(int id, out Skill skill)
        {
            var dict = SkillDataBase.dicSkills;
            return dict.TryGetValue(id, out skill);
        }
        public static bool TryGetStaticSkill(int id, out Skill staticSkill)
        {
            var dict = StaticSkillDataBase.dicSkills;
            return dict.TryGetValue(id, out staticSkill);
        }
        public static bool TryGetItem(int id, out item item)
        {
            var dict = ItemDataBase.items;
            return dict.TryGetValue(id, out item);
        }
        public static Skill GetSkill(int id)
        {
            var dict = SkillDataBase.dicSkills;
            return dict.TryGetValue(id, out var value) ? value : null;
        }
        public static Skill GetStaticSkill(int id)
        {
            var dict = StaticSkillDataBase.dicSkills;
            return dict.TryGetValue(id, out var value) ? value : null;
        }
        public static item GetItem(int id)
        {
            var dict = ItemDataBase.items;
            return dict.TryGetValue(id, out var value) ? value : null;
        }
    }
}