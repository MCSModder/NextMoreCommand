using System.Collections.Generic;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    public class MenPaiShopManager
    {
        /// <summary>
        /// 商店信息
        /// </summary>
        public static Dictionary<string, MenPaiShopInfo> MenPaiShopInfos = new Dictionary<string, MenPaiShopInfo>();
        /// <summary>
        /// 子类商店信息
        /// </summary>
        public static Dictionary<string, MenPaiShopChildInfo> MenPaiShopChildInfos = new Dictionary<string, MenPaiShopChildInfo>();
    }
}