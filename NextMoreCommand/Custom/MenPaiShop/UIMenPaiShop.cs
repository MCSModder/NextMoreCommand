using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using JSONClass;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;
using UnityEngine.UI;
using YSGame;
using Avatar = KBEngine.Avatar;
using Object = UnityEngine.Object;
using static UnityEngine.Random;

namespace SkySwordKill.NextMoreCommand.Custom.MenPaiShop
{
 

    [RequireComponent(typeof(UIMenPaiShop))]
    public class CustomMenPaiShop : MonoBehaviour, IESCClose
    {
        public static bool m_isCustomMenPaiShop = false;

        public static void Test()
        {
            var inst = Instance;
            inst.SetShop(0, "测试");
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < Range(10, 50); j++)
                {
                    inst.CreateItem(GetRandomItem(), i, Range(1, 100));
                }

            }
        }
        public List<int> ItemKey;
        public static int GetRandomItem()
        {

            if (Instance.ItemKey == null)
            {
                Instance.ItemKey = ItemJsonData.Keys.ToList();
            }
            var count = Instance.ItemKey.Count;
            return Instance.ItemKey[Range(0, count - 1)];
        }
        private static Sprite SpriteMoney => UiUtils.SpriteMoney;
        public static CustomMenPaiShop Instance => _instance ? _instance : Create();
        private static CustomMenPaiShop _instance;
        private static CustomMenPaiShop Create()
        {
            UIMenPaiShop.Inst.gameObject.AddMissingComponent<CustomMenPaiShop>();
            return _instance;
        }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            _instance = this;
            transform.Find("Scale/CloseBtn")?.GetComponent<Button>()?.onClick.AddListener(Close);

        }
        private static UIMenPaiShop UIMenPaiShop => UIMenPaiShop.Inst;
        private static ItemDatebase ItemDatabase => ItemDatebase.Inst;
        private static Dictionary<int, _ItemJsonData> ItemJsonData => _ItemJsonData.DataDict;
        private static Avatar Player => PlayerEx.Player;
        private GameObject ScaleObj => UIMenPaiShop.ScaleObj;
        public string ShopTitle
        {
            get => UIMenPaiShop.ShopTitle.text;
            set => UIMenPaiShop.ShopTitle.text = value;
        }
        public void DestroyAllChildShop()
        {
            var menPai = UIMenPaiShop;
            for (var i = 0; i < 3; i++)
            {
                menPai.ShopName[i].text = "无";
                menPai.ShopRT[i].DestoryAllChild();
            }
        }
        public void ResetShop()
        {
            ShopTitle = "门派商店";
            DestroyAllChildShop();
            Items.Clear();
        }
        public void SetShop(int moneyID, string shopTitle)
        {
            ResetShop();

            m_isCustomMenPaiShop = true;
            ShopTitle = shopTitle;
            SetMoney(moneyID);
            Show();
        }
        public Dictionary<int, List<CustomMenPaiShopItem>> Items = new Dictionary<int, List<CustomMenPaiShopItem>>();
        public void SetItemDict(int position, CustomMenPaiShopItem customMenPaiShopItem)
        {
            List<CustomMenPaiShopItem> list;
            if (Items.ContainsKey(position))
            {
                list = Items[position];


            }
            else
            {
                list = new List<CustomMenPaiShopItem>()
                {
                    customMenPaiShopItem
                };
                Items.Add(position, list);
            }
            list.Add(customMenPaiShopItem);
            customMenPaiShopItem.m_onDetroy = item =>
            {

                list.Remove(item);
            };
        }
        public void CreateItem(int item, int position, int count = 1)
        {
            var itemShop = CreateItem(position);
            if (itemShop == null)
            {
                return;
            }
            var customMenPaiShopItem = itemShop.gameObject.AddMissingComponent<CustomMenPaiShopItem>();
            SetItemDict(position, customMenPaiShopItem);
            customMenPaiShopItem.SetItem(item, count);
        }
        public UIMenPaiShopItem CreateItem(int position)
        {
            Transform parent = null;
            switch (position)
            {
                case 0:
                case 1:
                case 2:
                    parent = UIMenPaiShop.ShopRT[position];
                    break;
            }
            return parent == null ? null : Instantiate(UIMenPaiShop.UIMenPaiShopItemPrefab, parent).GetComponent<UIMenPaiShopItem>();
        }
        public int moneyType = 0;
        public Sprite GetMoneySprite() => moneyType == 0 ? SpriteMoney : ItemDatabase.items[moneyType].itemIconSprite;
        public item GetItemData(int itemId) => ItemDatabase.items[itemId];
        public void SetMoney(int moneyID)
        {
            moneyType = moneyID;
            var player = Player;
            string money;
            Sprite sprite = null;
            if (moneyID == 0)
            {

                sprite = SpriteMoney;
                money = player.money.ToString();
            }
            else
            {
                var item = ItemDatabase.items[moneyID];
                money = player.getItemNum(moneyID).ToString();
                sprite = item.itemIconSprite;
            }
            var menPaiShop = UIMenPaiShop;
            menPaiShop.MoneyIcon.sprite = sprite;
            menPaiShop.MoneyText.text = money;
        }
        public void Show()
        {
            ScaleObj.SetActive(true);
            MusicMag.instance.PlayEffectMusic("open_tradepage");
            ESCCloseManager.Inst.RegisterClose(this);
        }
        public static void ShowCustomShop(string title, float ratio = 1f)
        {
            var spriteMoney = SpriteMoney;
            if (spriteMoney == null)
            {
                return;
            }
            var all = Array.Empty<NomelShopJsonData>();
            var uiMenPaiShop = UIMenPaiShop;


            uiMenPaiShop.ScaleObj.SetActive(true);
            uiMenPaiShop.ShopTitle.text = title;
            var player = Player;
            var levelType = player.getLevelType();
            for (var index = 0; index < 3; ++index)
            {
                var count = 0;
                if (index > count)
                {
                    uiMenPaiShop.ShopName[index].text = "无";
                    uiMenPaiShop.ShopRT[index].DestoryAllChild();
                    continue;
                }
                var nomelShopJsonData = all[index];
                uiMenPaiShop.ShopName[index].text = nomelShopJsonData.ChildTitle;
                var shopGoods = UIDuiHuanShop.GetShopGoods(nomelShopJsonData.ExShopID);
                shopGoods.Sort();
                uiMenPaiShop.ShopRT[index].DestoryAllChild();
                foreach (var jiaoHuanShopGoods in shopGoods)
                {
                    var good = jiaoHuanShopGoods;
                    var item = _ItemJsonData.DataDict[good.GoodsID];
                    if (nomelShopJsonData.SType == 1 && levelType < item.quality && (item.type == 3 || item.type == 4)) continue;
                    var shopItem = Object.Instantiate(uiMenPaiShop.UIMenPaiShopItemPrefab, uiMenPaiShop.ShopRT[index]).GetComponent<UIMenPaiShopItem>();
                    var price = item.price;
                    if (ratio > 0)
                    {
                        price = (int)(price * ratio);
                    }

                    shopItem.PriceText.text = price.ToString();
                    shopItem.PriceIcon.sprite = spriteMoney;
                    shopItem.IconShow.SetItem(good.GoodsID);
                    shopItem.IconShow.Count = 1;
                    shopItem.IconShow.OnClick += (p =>
                    {
                        var num = (int)(player.money / (ulong)price);
                        var maxNum = Mathf.Min(num, item.maxNum);


                        switch (maxNum)
                        {
                            case 0:
                                UIPopTip.Inst.Pop("灵石不足");
                                break;
                            case 1:
                                USelectBox.Show("是否兑换" + item.name + " x1", (() =>
                                {
                                    var itemName = _ItemJsonData.DataDict[good.GoodsID].name;
                                    var name = "灵石";
                                    var avatar = PlayerEx.Player;
                                    var isShop = avatar.money >= (ulong)price;
                                    if (isShop)
                                    {
                                        avatar.AddMoney(-price);

                                        avatar.addItem(good.GoodsID, 1, Tools.CreateItemSeid(good.GoodsID));
                                        UIPopTip.Inst.Pop("兑换了" + itemName + "x1", PopTipIconType.包裹);
                                    }
                                    else
                                        UIPopTip.Inst.Pop(name + "不足");
                                }));
                                break;
                            default:
                                USelectNum.Show("兑换数量 x{num}", 1, maxNum, (num =>
                                {
                                    var itemName = _ItemJsonData.DataDict[good.GoodsID].name;
                                    var avatar = PlayerEx.Player;
                                    var total = num * price;
                                    var isShop = avatar.money >= (ulong)total;
                                    if (isShop)
                                    {
                                        avatar.AddMoney(-price);
                                        avatar.addItem(good.GoodsID, num, Tools.CreateItemSeid(good.GoodsID));

                                        UIPopTip.Inst.Pop($"兑换了{itemName}x{num.ToString()}", PopTipIconType.包裹);

                                    }
                                    else
                                        UIPopTip.Inst.Pop("灵石不足");
                                }));
                                break;
                        }
                    });
                }
            }

        }
        public bool TryEscClose()
        {
            Close();
            return true;
        }
        void Close()
        {
            ScaleObj.SetActive(false);
            ResetShop();
            m_isCustomMenPaiShop = false;
            ESCCloseManager.Inst.UnRegisterClose(this);
        }
    }
}