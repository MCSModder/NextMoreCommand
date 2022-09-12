using System;
using BepInEx;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextCommandExamplePlugin
{
    [BepInPlugin("skyswordkill.plugin.NextCommandExamplePlugin", "NextCommandExamplePlugin", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        private void Awake()
        {
            
            // 注册 CostMoney 事件
            DialogAnalysis.RegisterCommand("CostMoney",new CostMoneyEvent());
        }
    }
}