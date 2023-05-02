using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using Newtonsoft.Json;
using SkySwordKill.Next;

namespace SkySwordKill.NextMoreCommand.NextSeachNpcExtension
{
    public class SearchNpcDataInfo
    {
        private static jsonData JsonData => jsonData.instance;
        private static Tools Tools => Tools.instance;
        public static SearchNpcDataInfo Inst { get; } = new SearchNpcDataInfo();
        public int Id { get; private set; }
        public string RawMatchStr { get; private set; }
        public SearchNpcData SearchNpcData { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public JSONObject NpcJson { get; private set; }
        public List<item> Items { get; private set; } = new List<item>();
        public UINPCData GetNpcData()
        {
            var npcData = new UINPCData(SearchNpcData.ID);
            npcData.RefreshData();
            return npcData;
        }
        public void SetSearchNpcDataInfo(string matchStr, SearchNpcData searchNpcData)
        {
            RawMatchStr = matchStr;
            var split = RawMatchStr.Split(new[]
            {
                ':'
            }, 2);
            Key = split[0].ToLower();
            Value = split[1];
            if (SearchNpcData == searchNpcData)
            {
                return;
            }
            SearchNpcData = searchNpcData;
            Id = SearchNpcData.ID;
            NpcJson = Id.NPCJson();
            GetItemList(true);
        }
        public List<Skill> GetSkillList()
        {
            if (!NpcJson.HasField("skills"))
            {
                return new List<Skill>();
            }
            var list = DeserializeNpcField<List<int>>("skills");
            return list.Select(SearchNpcDataManager.GetSkill).Where(skill => skill != null).ToList();
        }
        public List<Skill> GetStaticSkillList()
        {
            if (!NpcJson.HasField("staticSkills"))
            {
                return new List<Skill>();
            }
            var list = DeserializeNpcField<List<int>>("staticSkills");
            return list.Select(SearchNpcDataManager.GetSkill).Where(skill => skill != null).ToList();
        }
        public List<item> GetItemList(bool isRefresh = false)
        {

            if (!JsonData.AvatarBackpackJsonData.HasField(Id.ToString()) || isRefresh)
            {
                Items.Clear();
            }
            if (isRefresh)
            {
                var backpackJsonData = JsonData.AvatarBackpackJsonData.GetField(Id.ToString());
                var backpack = JsonConvert.DeserializeObject<List<ItemInfo>>(backpackJsonData.GetField("Backpack").ToString());
                Items = backpack.Select(item => item.ToItem()).ToList();
            }

            return Items;
        }
        public T DeserializeNpcField<T>(string key)
        {
            return NpcJson.HasField(key) ? JsonConvert.DeserializeObject<T>(NpcJson.GetField(key).ToString()) : default;
        }
        public string[] ValueSplit(params char[] separator)
        {
            return Value.Split(separator);
        }
        public string[] ValueSplit(char[] separator, int count)
        {
            return Value.Split(separator, count);
        }
    }
}