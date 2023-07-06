using System.Collections.Generic;
using System.Linq;
using BetterAiOptimization.Data.Common;
using BetterAiOptimization.Data.Json;
using BetterAiOptimization.Manager;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace BetterAiOptimization.Data
{
    public class AiSkillData
    {
        public static Dictionary<int, _skillJsonData> SkillJsonDataDict => _skillJsonData.DataDict;
        public static Dictionary<int, FightAIData> FightAIDataDict => FightAIData.DataDict;
        public static Dictionary<int, JSONObject> AiJsonData => jsonData.instance.AIJsonDate;
        public static bool TryGetValue(int skillId, out AiSkillData aiSkillData)
        {
            aiSkillData = null;
            if (!SkillJsonDataDict.TryGetValue(skillId, out var skillJsonData)) return false;
            aiSkillData = new AiSkillData(skillId, skillJsonData);
            return true;
        }
        public static bool TryGetValue(AI ai, int skillId, out AiSkillData aiSkillData)
        {
            aiSkillData = null;
            if (!SkillJsonDataDict.TryGetValue(skillId, out var skillJsonData)) return false;
            aiSkillData = new AiSkillData(ai, skillId, skillJsonData);
            return true;
        }
        public AiSkillData(AI ai, int skillId, _skillJsonData skillJsonData) : this(skillId, skillJsonData)
        {
            Ai = ai;
        }
        public AiSkillData(int skillId, _skillJsonData skillJsonData)
        {
            Id = skillId;
            SkillJsonData = skillJsonData;
            Weight = SkillJsonData.Skill_Type;
        }

        public _skillJsonData SkillJsonData { get; }
        public int Id { get; }
        public AI Ai { get; set; }
        public int Weight { get; set; }
        public Avatar GetAvatar<T>(T data) where T : IJsonTarget
        {
            var avatar = (Avatar)Ai.entity;
            Avatar result = null;
            switch (data.GetTarget())
            {
                case "self":
                    result = avatar;
                    break;
                case "other":
                    result = avatar.OtherAvatar;
                    break;
                default:
                    Debug.LogError("AI表中" + Id + "技能（对象判定）字段错误");
                    break;
            }
            return result;
        }
        public Avatar GetAvatar(JSONObject AiInfo)
        {
            var avatar = (Avatar)Ai.entity;
            Avatar result = null;
            switch (AiInfo["panduan1"].str)
            {
                case "self":
                    result = avatar;
                    break;
                case "other":
                    result = avatar.OtherAvatar;
                    break;
                default:
                    Debug.LogError("AI表中" + Id + "技能（对象判定）字段错误");
                    break;
            }
            return result;
        }
        public void SetFlag(int num)
        {
            if (num != 0)
            {
                Weight = num;
            }
        }
        public void SetFlagByOperator<T>(T data, int left, int right) where T : IJsonOperator, IJsonData
        {
            var condition = false;
            switch (data.GetOperator())
            {
                case ">":
                    condition = left > right;
                    break;
                case "<":
                    condition = left < right;
                    break;
                default:
                    Debug.LogError("AI表中" + Id + "技能（判断大小）字段错误");
                    return;
            }
            SetFlag(data.GetResult(condition));
        }
        public void OrderSkill()
        {

            if (FightAIDataDict.TryGetValue(Id, out var fightAIData))
            {
                var orderList = fightAIData.ShunXu;
                foreach (var order in orderList)
                {
                    AiManager.AiRealizeType(order, this);
                }
            }
            var aiJsonData = AiJsonData;
            var id = Id.ToString();
            foreach (var aiData in aiJsonData.Where(aiData => aiData.Value.HasField(id)))
            {
                AiManager.AiRealizeType(aiData.Key, this);
            }
        }

    }
}