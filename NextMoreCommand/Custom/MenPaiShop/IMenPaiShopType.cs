using System;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    public interface IMenPaiShopType
    {
        public string Type { get; set; }
        void OnClick(CustomMenPaiShopItem customMenPaiShopItem,MenPaiShopItemInfo itemInfo,DialogEnvironmentContext env);
        void OnComplete(CustomMenPaiShopItem customMenPaiShopItem,MenPaiShopItemInfo itemInfo,DialogEnvironmentContext env);
        void OnCancel(CustomMenPaiShopItem customMenPaiShopItem,MenPaiShopItemInfo itemInfo,DialogEnvironmentContext env);
    }
}