using System;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    [RequireComponent(typeof(UIMenPaiShopItem))]
    public class CustomMenPaiShopItem : MonoBehaviour
    {
        private static Sprite           SpriteMoney => UiUtils.SpriteMoney;
        public         UIMenPaiShopItem ShopItem;
        private static CustomMenPaiShop CustomMenPaiShop => CustomMenPaiShop.Instance;
        private        IMenPaiShopType  _shopType;
        private void Awake()
        {
            ShopItem = GetComponent<UIMenPaiShopItem>();
        }
        public UIIconShow IconShow => ShopItem.IconShow;
        public void SetItem(int item, int count = 1)
        {
            if (MenPaiShopManager.MenPaiShopTypes.TryGetValue("", out _shopType))
            {

            }
            var iconShow = ShopItem.IconShow;
            iconShow.SetItem(item);
            iconShow.Count = count;
            var itemData = CustomMenPaiShop.GetItemData(item);
            ShopItem.PriceIcon.sprite = CustomMenPaiShop.GetMoneySprite();
            ShopItem.PriceText.text = itemData.itemPrice.ToString();
            ShopItem.IconShow.OnClick += p =>
            {
                _shopType?.OnClick(this, null, new DialogEnvironmentContext());
            };
        }
        public Action<CustomMenPaiShopItem> m_onDetroy;
        private void OnDestroy()
        {
            m_onDetroy?.Invoke(this);
        }
    }
}