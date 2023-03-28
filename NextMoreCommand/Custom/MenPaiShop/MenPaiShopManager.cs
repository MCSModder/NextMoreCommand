using System;
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
        public static Dictionary<string, IMenPaiShopType> MenPaiShopTypes = new Dictionary<string, IMenPaiShopType>();
        public static BaseShopType CreateBaseType(string typeName)
        {
            var type = new BaseShopType(typeName);
            RegisterType(typeName, type);
            return type;
        }
        public static void RegisterType(string name, IMenPaiShopType menPaiShopType)
        {
            MenPaiShopTypes[name] = menPaiShopType;
        }
    }
}