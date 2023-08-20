using System;
using BepInEx;

namespace BetterAiOptimization
{
    [BepInPlugin("zerxz.plugin.NextTileMapMore", "NextTileMapMore", "1.0.0")]
    [BepInDependency("skyswordkill.plugin.Next", BepInDependency.DependencyFlags.HardDependency)]
    public class NextTileMapMore:BaseUnityPlugin
    {
        public static NextTileMapMore Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
    }
}