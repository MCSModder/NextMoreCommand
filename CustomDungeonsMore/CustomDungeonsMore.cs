using BepInEx;

namespace CustomDungeonsMore
{
    [BepInPlugin("zerxz.plugin.CustomDungeonsMore", "CustomDungeonsMore", "1.0.0")]
    [BepInDependency("UniqueCream.CustomDungeons", BepInDependency.DependencyFlags.HardDependency)]
    public class CustomDungeonsMore:BaseUnityPlugin
    {
        public static CustomDungeonsMore Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
    }
}