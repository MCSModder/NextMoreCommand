using System;
using SkySwordKill.Next.DialogSystem;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    public class BaseShopType : IMenPaiShopType
    {
        public string Type { get; set; } = string.Empty;
        public void OnClick(CustomMenPaiShopItem customMenPaiShopItem)
        {
            
            OnClickAction?.Invoke(customMenPaiShopItem);
        }
        public void OnComplete(CustomMenPaiShopItem customMenPaiShopItem)
        {
            OnCompleteAction?.Invoke(customMenPaiShopItem);
        }
        public void OnCancel(CustomMenPaiShopItem customMenPaiShopItem)
        {
            OnCancelAction?.Invoke(customMenPaiShopItem);
        }
        public Action<CustomMenPaiShopItem> OnClickAction { get; set; }
        public Action<CustomMenPaiShopItem> OnCompleteAction { get; set; }
        public Action<CustomMenPaiShopItem> OnCancelAction { get; set; }
    }
}