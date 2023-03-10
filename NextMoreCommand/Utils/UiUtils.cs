using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using JSONClass;
using KBEngine;
using Tab;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using YSGame;
using GameObject = UnityEngine.GameObject;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class UiUtils
{
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

        MusicMag.instance.PlayEffectMusic("open_tradepage", 1f);
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
    private static UIMenPaiShop _uiMenPaiShop => UIMenPaiShop.Inst;
    public static void ShowMenPaiShop(string title, bool isMoney = false)
    {
        var all = NomelShopJsonData.DataList.FindAll(item => item.Title == title);
        if (all.Count == 0)
        {
            return;
        }
        TabUIMag.OpenTab(3);
        var tabUIMag = TabUIMag.Instance;
        var spriteMoney = tabUIMag.TabBag.MoneyIcon.sprite;
        tabUIMag.TryEscClose();
        if (isMoney && spriteMoney == null)
        {
            return;
        }
        var uiMenPaiShop = _uiMenPaiShop;
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
                            USelectNum.Show("兑换数量 x{num}", 1, maxNum, (num =>
                            {
                                var itemName = _ItemJsonData.DataDict[good.GoodsID].name;
                                var name = isMoney ? "灵石" : _ItemJsonData.DataDict[good.EXGoodsID].name;
                                var avatar = PlayerEx.Player;
                                var total = num * price;
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
                                    avatar.addItem(good.GoodsID, num, Tools.CreateItemSeid(good.GoodsID));

                                    UIPopTip.Inst.Pop($"兑换了{itemName}x{num.ToString()}", PopTipIconType.包裹);
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


        ESCCloseManager.Inst.RegisterClose((IESCClose)uiMenPaiShop);

    }
}