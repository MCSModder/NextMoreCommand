using System;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
    [RequireComponent(typeof(UIMenPaiShopItem))]
    public class CustomMenPaiShopItem : MonoBehaviour
    {
        private static Sprite SpriteMoney => UiUtils.SpriteMoney;
        public UIMenPaiShopItem ShopItem;
        private static CustomMenPaiShop CustomMenPaiShop => CustomMenPaiShop.Instance;
        private void Awake()
        {
            ShopItem = GetComponent<UIMenPaiShopItem>();
        }
        public UIIconShow IconShow => ShopItem.IconShow;
        public void SetItem(int item, int count = 1)
        {
            var iconShow = ShopItem.IconShow;
            iconShow.SetItem(item);
            iconShow.Count = count;
            var itemData = CustomMenPaiShop.GetItemData(item);
            ShopItem.PriceIcon.sprite = CustomMenPaiShop.GetMoneySprite();
            ShopItem.PriceText.text = itemData.itemPrice.ToString();
        }
        public Action<CustomMenPaiShopItem> m_onDetroy;
        private void OnDestroy()
        {
            m_onDetroy?.Invoke(this);
        }
    }
}