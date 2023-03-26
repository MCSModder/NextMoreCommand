using System;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    public interface IMenPaiShopType
    {
        public string Type { get; set; }
        void OnClick(CustomMenPaiShopItem customMenPaiShopItem);
        void OnComplete(CustomMenPaiShopItem customMenPaiShopItem);
        void OnCancel(CustomMenPaiShopItem customMenPaiShopItem);
    }
}