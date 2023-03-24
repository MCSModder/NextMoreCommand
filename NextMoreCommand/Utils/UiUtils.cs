using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using JSONClass;
using SkySwordKill.Next;
using Tab;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YSGame;
using static UnityEngine.Random;
using Avatar = KBEngine.Avatar;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils;

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

public static class UiUtils
{
    private static Sprite _spriteMoney;
    public static Sprite SpriteMoney => GetSpriteMoney();
    private static Sprite GetSpriteMoney()
    {
        if (_spriteMoney != null)
        {
            return _spriteMoney;
        }
        if (!Main.Res.TryGetAsset("assets/resources/lingshi.png", out var asset)) return null;
        if (asset is Texture2D texture2D)
        {
            _spriteMoney = Main.Res.GetSpriteCache(texture2D);
        }
        return _spriteMoney;
    }
    // public static void ShowCustomMenPaiShop(this UIMenPaiShop uiMenPaiShop)
    // {
    //     uiMenPaiShop.SetCustomUiMenPaiShop();
    //     uiMenPaiShop.Show();
    // }
    public static void ShowMiniShop(int itemID, int price, int maxSellCount)
    {
        UIMiniShop.Show(itemID, price, maxSellCount);
    }
    private static UIGaoShi UIGaoShi => UIGaoShi.Inst;
    private static Avatar Player => PlayerEx.Player;
    public static void ShowGaoShi(string sceneName)
    {
        var dataDict = GaoShiLeiXing.DataDict;
        if (!dataDict.Keys.Contains(sceneName))
        {
            MyLog.Log("找不到告示的场景");
            return;
        }

        MusicMag.instance.PlayEffectMusic("open_tradepage");
        var uiGaoShi = UIGaoShi;
        uiGaoShi.ScaleObj.SetActive(true);
        uiGaoShi.ContentRT.DestoryAllChild();
        var player = Player;
        var gaoShiLeiXing = dataDict[sceneName];
        var gaoShiJson = player.GaoShi;
        if (gaoShiJson.HasField(sceneName) && gaoShiLeiXing != null)
        {
            uiGaoShi.Title.text = gaoShiLeiXing.name;
            var list = gaoShiJson[sceneName]["GaoShiList"].list;
            foreach (var jsonObject in list)
            {
                var id = jsonObject["GaoShiID"].I;
                var gaoShi = GaoShi.DataDict[id];
                switch (gaoShi.type)
                {
                    case 1:
                        uiGaoShi.CreateShouGouItem(jsonObject, gaoShi, gaoShiLeiXing);
                        break;
                    case 2:
                        uiGaoShi.CreateRenWuItem(jsonObject, gaoShi);
                        break;
                    case 3:
                        uiGaoShi.CreateQingBaoItem(jsonObject, gaoShi);
                        break;
                }
            }
        }
        else
        {
            uiGaoShi.Title.text = "无告示";
        }
        ESCCloseManager.Inst.RegisterClose(uiGaoShi);

    }
    private static UIMenPaiShop UIMenPaiShop => UIMenPaiShop.Inst;
    public static void ShowMenPaiShop(string title, bool isMoney = false, float ratio = 1f)
    {
        var all = NomelShopJsonData.DataList.FindAll(item => item.Title == title);
        if (all.Count == 0)
        {
            return;
        }

        TabUIMag.OpenTab(3);
        var tabUIMag = TabUIMag.Instance;
        var spriteMoney = SpriteMoney;
        tabUIMag.TryEscClose();
        if (isMoney && spriteMoney == null)
        {
            return;
        }
        var uiMenPaiShop = UIMenPaiShop;
        MusicMag.instance.PlayEffectMusic("open_tradepage");
        uiMenPaiShop.ScaleObj.SetActive(true);
        uiMenPaiShop.ShopTitle.text = title;
        var player = Player;
        var levelType = player.getLevelType();
        var itemDatebase = jsonData.instance.GetComponent<ItemDatebase>();
        var exGoodsID = 0;
        for (var index = 0; index < 3; ++index)
        {
            var count = all.Count - 1;
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
                if (exGoodsID == 0)
                    exGoodsID = good.EXGoodsID;
                var item = _ItemJsonData.DataDict[good.GoodsID];
                if (nomelShopJsonData.SType == 1 && levelType < item.quality && (item.type == 3 || item.type == 4)) continue;
                var shopItem = Object.Instantiate(uiMenPaiShop.UIMenPaiShopItemPrefab, uiMenPaiShop.ShopRT[index]).GetComponent<UIMenPaiShopItem>();
                var price = isMoney ? item.price : item.price / good.percent;
                if (ratio > 0)
                {
                    price = (int)(price * ratio);
                }
                if (!isMoney && item.price % good.percent > 0)
                    price++;
                shopItem.PriceText.text = price.ToString();
                shopItem.PriceIcon.sprite = isMoney ? spriteMoney : itemDatebase.items[exGoodsID].itemIconSprite;
                shopItem.IconShow.SetItem(good.GoodsID);
                shopItem.IconShow.Count = 1;
                shopItem.IconShow.OnClick += (p =>
                {
                    var num = isMoney ? (int)(player.money / (ulong)price) : player.getItemNum(good.EXGoodsID) / price;
                    var maxNum = Mathf.Min(num, item.maxNum);


                    switch (maxNum)
                    {
                        case 0:
                            var moneyName = isMoney ? "灵石" : _ItemJsonData.DataDict[good.EXGoodsID].name;
                            UIPopTip.Inst.Pop(moneyName + "不足");
                            break;
                        case 1:
                            USelectBox.Show("是否兑换" + item.name + " x1", (() =>
                            {
                                var itemName = _ItemJsonData.DataDict[good.GoodsID].name;
                                var name = isMoney ? "灵石" : _ItemJsonData.DataDict[good.EXGoodsID].name;
                                var avatar = PlayerEx.Player;
                                var isShop = isMoney ? avatar.money >= (ulong)price : avatar.getItemNum(good.EXGoodsID) >= price;
                                if (isShop)
                                {
                                    if (isMoney)
                                    {
                                        avatar.AddMoney(-price);
                                    }
                                    else
                                    {
                                        avatar.removeItem(good.EXGoodsID, price);
                                    }

                                    avatar.addItem(good.GoodsID, 1, Tools.CreateItemSeid(good.GoodsID));
                                    UIPopTip.Inst.Pop("兑换了" + itemName + "x1", PopTipIconType.包裹);
                                    // UiUtils.ShowMenPaiShop(title,isMoney);
                                }
                                else
                                    UIPopTip.Inst.Pop(name + "不足");
                            }));
                            break;
                        default:
                            USelectNum.Show("兑换数量 x{num}", 1, maxNum, (i =>
                            {
                                var itemName = _ItemJsonData.DataDict[good.GoodsID].name;
                                var name = isMoney ? "灵石" : _ItemJsonData.DataDict[good.EXGoodsID].name;
                                var avatar = PlayerEx.Player;
                                var total = i * price;
                                var isShop = isMoney ? avatar.money >= (ulong)total : avatar.getItemNum(good.EXGoodsID) >= total;
                                if (isShop)
                                {
                                    if (isMoney)
                                    {
                                        avatar.AddMoney(-price);
                                    }
                                    else
                                    {
                                        avatar.removeItem(good.EXGoodsID, price);
                                    }
                                    avatar.addItem(good.GoodsID, i, Tools.CreateItemSeid(good.GoodsID));

                                    UIPopTip.Inst.Pop($"兑换了{itemName}x{i.ToString()}", PopTipIconType.包裹);
                                    //  UiUtils.ShowMenPaiShop(title,isMoney);

                                }
                                else
                                    UIPopTip.Inst.Pop(name + "不足");
                            }));
                            break;
                    }
                });
            }
        }
        uiMenPaiShop.MoneyIcon.sprite = isMoney ? spriteMoney : itemDatebase.items[exGoodsID].itemIconSprite;
        uiMenPaiShop.MoneyText.text = isMoney ? player.money.ToString() : player.getItemNum(exGoodsID).ToString();


        ESCCloseManager.Inst.RegisterClose(uiMenPaiShop);

    }
    public static CustomMenPaiShop CustomMenPaiShop => CustomMenPaiShop.Instance;
    public static void ShowCustomShop(string title, float ratio = 1f)
    {
        var spriteMoney = SpriteMoney;
        if (spriteMoney == null)
        {
            return;
        }
        var all = Array.Empty<NomelShopJsonData>();
        // var uiMenPaiShop = UIMenPaiShop;
        //
        // MusicMag.instance.PlayEffectMusic("open_tradepage");
        // uiMenPaiShop.ScaleObj.SetActive(true);
        // uiMenPaiShop.ShopTitle.text = title;
        // var player = Player;
        // var levelType = player.getLevelType();
        // for (var index = 0; index < 3; ++index)
        // {
        //     var count = 0;
        //     if (index > count)
        //     {
        //         uiMenPaiShop.ShopName[index].text = "无";
        //         uiMenPaiShop.ShopRT[index].DestoryAllChild();
        //         continue;
        //     }
        //     var nomelShopJsonData = all[index];
        //     uiMenPaiShop.ShopName[index].text = nomelShopJsonData.ChildTitle;
        //     var shopGoods = UIDuiHuanShop.GetShopGoods(nomelShopJsonData.ExShopID);
        //     shopGoods.Sort();
        //     uiMenPaiShop.ShopRT[index].DestoryAllChild();
        //     foreach (var jiaoHuanShopGoods in shopGoods)
        //     {
        //         var good = jiaoHuanShopGoods;
        //         var item = _ItemJsonData.DataDict[good.GoodsID];
        //         if (nomelShopJsonData.SType == 1 && levelType < item.quality && (item.type == 3 || item.type == 4)) continue;
        //         var shopItem = Object.Instantiate(uiMenPaiShop.UIMenPaiShopItemPrefab, uiMenPaiShop.ShopRT[index]).GetComponent<UIMenPaiShopItem>();
        //         var price = item.price;
        //         if (ratio > 0)
        //         {
        //             price = (int)(price * ratio);
        //         }
        //
        //         shopItem.PriceText.text = price.ToString();
        //         shopItem.PriceIcon.sprite = spriteMoney;
        //         shopItem.IconShow.SetItem(good.GoodsID);
        //         shopItem.IconShow.Count = 1;
        //         shopItem.IconShow.OnClick += (p =>
        //         {
        //             var num = (int)(player.money / (ulong)price);
        //             var maxNum = Mathf.Min(num, item.maxNum);
        //
        //
        //             switch (maxNum)
        //             {
        //                 case 0:
        //                     UIPopTip.Inst.Pop("灵石不足");
        //                     break;
        //                 case 1:
        //                     USelectBox.Show("是否兑换" + item.name + " x1", (() =>
        //                     {
        //                         var itemName = _ItemJsonData.DataDict[good.GoodsID].name;
        //                         var name = "灵石";
        //                         var avatar = PlayerEx.Player;
        //                         var isShop = avatar.money >= (ulong)price;
        //                         if (isShop)
        //                         {
        //                             avatar.AddMoney(-price);
        //
        //                             avatar.addItem(good.GoodsID, 1, Tools.CreateItemSeid(good.GoodsID));
        //                             UIPopTip.Inst.Pop("兑换了" + itemName + "x1", PopTipIconType.包裹);
        //                         }
        //                         else
        //                             UIPopTip.Inst.Pop(name + "不足");
        //                     }));
        //                     break;
        //                 default:
        //                     USelectNum.Show("兑换数量 x{num}", 1, maxNum, (num =>
        //                     {
        //                         var itemName = _ItemJsonData.DataDict[good.GoodsID].name;
        //                         var avatar = PlayerEx.Player;
        //                         var total = num * price;
        //                         var isShop = avatar.money >= (ulong)total;
        //                         if (isShop)
        //                         {
        //                             avatar.AddMoney(-price);
        //                             avatar.addItem(good.GoodsID, num, Tools.CreateItemSeid(good.GoodsID));
        //
        //                             UIPopTip.Inst.Pop($"兑换了{itemName}x{num.ToString()}", PopTipIconType.包裹);
        //
        //                         }
        //                         else
        //                             UIPopTip.Inst.Pop("灵石不足");
        //                     }));
        //                     break;
        //             }
        //         });
        //     }
        // }
        // uiMenPaiShop.MoneyIcon.sprite = spriteMoney;
        // uiMenPaiShop.MoneyText.text = player.money.ToString();
        //
        //
        // ESCCloseManager.Inst.RegisterClose(uiMenPaiShop);

    }
}