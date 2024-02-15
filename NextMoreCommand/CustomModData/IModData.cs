using SkySwordKill.Next.Mod;

namespace SkySwordKill.NextMoreCommand.CustomModData
{
    public interface IModData
    {
        void Read(ModConfig  modConfig);
        bool Check(ModConfig modConfig);
    }
}