using System;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    public class BaseShopType : IMenPaiShopType
    {
        public BaseShopType()
        {

        }
        public BaseShopType(string type)
        {
            Type = type;
        }
        public string Type { get; set; } = string.Empty;
        public void OnClick(CustomMenPaiShopItem customMenPaiShopItem, MenPaiShopItemInfo itemInfo, DialogEnvironmentContext env = null)
        {

            OnClickAction?.Invoke(customMenPaiShopItem, itemInfo, env ?? new DialogEnvironmentContext());
        }
        public void OnComplete(CustomMenPaiShopItem customMenPaiShopItem, MenPaiShopItemInfo itemInfo, DialogEnvironmentContext env = null)
        {
            OnCompleteAction?.Invoke(customMenPaiShopItem, itemInfo, env ?? new DialogEnvironmentContext());
        }
        public void OnCancel(CustomMenPaiShopItem customMenPaiShopItem, MenPaiShopItemInfo itemInfo, DialogEnvironmentContext env = null)
        {
            OnCancelAction?.Invoke(customMenPaiShopItem, itemInfo, env ?? new DialogEnvironmentContext());
        }
        public Action<CustomMenPaiShopItem, MenPaiShopItemInfo, DialogEnvironmentContext> OnClickAction    { get; set; }
        public Action<CustomMenPaiShopItem, MenPaiShopItemInfo, DialogEnvironmentContext> OnCompleteAction { get; set; }
        public Action<CustomMenPaiShopItem, MenPaiShopItemInfo, DialogEnvironmentContext> OnCancelAction   { get; set; }
    }
}