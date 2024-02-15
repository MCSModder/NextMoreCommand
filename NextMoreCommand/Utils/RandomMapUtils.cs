namespace SkySwordKill.NextMoreCommand.Utils
{
    public static class RandomMapUtils
    {
        public static RandomFuBenMag RandomFuBenMag                          => Tools.instance.getPlayer().randomFuBenMag;
        public static void           LoadFuben(int fubenID, int specialType) => RandomFuBenMag.GetInRandomFuBen(fubenID, specialType);
        public static void           LoadFuben(int fubenID) => RandomFuBenMag.GetInRandomFuBen(fubenID);
    }
}