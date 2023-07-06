using System.Linq;
using BetterAiOptimization.Data;
using BetterAiOptimization.Data.Common;
using BetterAiOptimization.Data.Json;
using BetterAiOptimization.Manager;

namespace BetterAiOptimization.Realize
{
    [AiRealize("1")]
    public class AiRealizeCheckBuff : IAiRealize
    {

        public void Execute(AiSkillData aiSkillData)
        {
            if (!AiManager.TryGetAiRealizeJson<AiCheckBuffData>("1", aiSkillData.Id, out var aiCheckBuff))
            {
                return;
            }
            var avatar = aiSkillData.GetAvatar(aiCheckBuff);
            if (avatar is null)
            {
                return;
            }
            var buffByID = avatar.buffmag.getBuffByID(aiCheckBuff.BuffId);
            var buffNum = buffByID.Sum(buff => buff[1]);
            aiSkillData.SetFlagByOperator(aiCheckBuff, buffNum, aiCheckBuff.BuffNum);
        }
    }
}