// using System.Collections.Generic;
// using GUIPackage;
// using JSONClass;
// using UnityEngine;
// using UnityEngine.Events;
// using UnityEngine.EventSystems;
//
// namespace SkySwordKill.NextMoreCommand.Utils;
//
// public static class UiUtils
// {
//     public static void ShowCustomMenPaiShop(this UIMenPaiShop uiMenPaiShop)
//     {
//         uiMenPaiShop.SetCustomUiMenPaiShop();
//         uiMenPaiShop.Show();
//     }
//     public static void ShowCustomMiniShop(this UIMiniShop uiMiniShop)
//     {
//         
//         uiMiniShop.itemID = itemID;
//         uiMiniShop.price = price;
//         uiMiniShop.cmd = cmd;
//         uiMiniShop.maxSellCount = maxSellCount;
//     }
//
//     public static void SetCustomUiMenPaiShop(this UIMenPaiShop uiMenPaiShop)
//     {
//         uiMenPaiShop.ShopTitle.text = "测试";
//         //  var num1 = 0;
//         // var levelType = PlayerEx.Player.getLevelType();
//         // var database =  jsonData.instance.GetComponent<ItemDatebase>();
//         //   uiMenPaiShop.ShopTitle.text = all[0].Title;
//         int num1 = 0;
//         int levelType = PlayerEx.Player.getLevelType();
//         ItemDatebase component1 = jsonData.instance.GetComponent<ItemDatebase>();
//         //  for (int index = 0; index < 3; ++index)
//         //  {
//         //      NomelShopJsonData nomelShopJsonData = all[index];
//         //      uiMenPaiShop.ShopName[index].text = nomelShopJsonData.ChildTitle;
//         //      uiMenPaiShop.ShopRT[index].DestoryAllChild();
//         //      List<jiaoHuanShopGoods> shopGoods = UIDuiHuanShop.GetShopGoods(nomelShopJsonData.ExShopID);
//         //      shopGoods.Sort();
//
//         //      foreach (jiaoHuanShopGoods jiaoHuanShopGoods in shopGoods)
//         //      {
//         //          jiaoHuanShopGoods good = jiaoHuanShopGoods;
//         //          if (num1 == 0)
//         //              num1 = good.EXGoodsID;
//         //          _ItemJsonData item = _ItemJsonData.DataDict[good.GoodsID];
//         //          if (nomelShopJsonData.SType != 1 || levelType >= item.quality || item.type != 3 && item.type != 4)
//         //          {
//         //              UIMenPaiShopItem component2 = UnityEngine.Object
//         //                  .Instantiate<GameObject>(uiMenPaiShop.UIMenPaiShopItemPrefab,
//         //                      (Transform)uiMenPaiShop.ShopRT[index]).GetComponent<UIMenPaiShopItem>();
//         //              int price = item.price / good.percent;
//         //              if (item.price % good.percent > 0)
//         //                  price++;
//         //              component2.PriceText.text = price.ToString();
//         //              component2.PriceIcon.sprite = component1.items[good.EXGoodsID].itemIconSprite;
//         //              component2.IconShow.SetItem(good.GoodsID);
//         //              component2.IconShow.Count = 1;
//         //              component2.IconShow.OnClick += (UnityAction<PointerEventData>)(p =>
//         //              {
//         //                  int maxNum = Mathf.Min(PlayerEx.Player.getItemNum(good.EXGoodsID) / price, item.maxNum);
//         //                  switch (maxNum)
//         //                  {
//         //                      case 0:
//         //                          UIPopTip.Inst.Pop(_ItemJsonData.DataDict[good.EXGoodsID].name + "不足");
//         //                          break;
//         //                      case 1:
//         //                          USelectBox.Show("是否兑换" + item.name + " x1", (UnityAction)(() =>
//         //                          {
//         //                              if (PlayerEx.Player.getItemNum(good.EXGoodsID) >= price)
//         //                              {
//         //                                  PlayerEx.Player.removeItem(good.EXGoodsID, price);
//         //                                  PlayerEx.Player.addItem(good.GoodsID, 1, Tools.CreateItemSeid(good.GoodsID));
//         //                                  uiMenPaiShop.RefreshUI();
//         //                                  UIPopTip.Inst.Pop("兑换了" + _ItemJsonData.DataDict[good.GoodsID].name + "x1",
//         //                                      PopTipIconType.包裹);
//         //                              }
//         //                              else
//         //                                  UIPopTip.Inst.Pop(_ItemJsonData.DataDict[good.EXGoodsID].name + "不足");
//         //                          }));
//         //                          break;
//         //                      default:
//         //                          USelectNum.Show("兑换数量 x{num}", 1, maxNum, (UnityAction<int>)(num =>
//         //                          {
//         //                              if (PlayerEx.Player.getItemNum(good.EXGoodsID) >= num * price)
//         //                              {
//         //                                  PlayerEx.Player.removeItem(good.EXGoodsID, num * price);
//         //                                  PlayerEx.Player.addItem(good.GoodsID, num, Tools.CreateItemSeid(good.GoodsID));
//         //                                  uiMenPaiShop.RefreshUI();
//         //                                  UIPopTip.Inst.Pop(
//         //                                      string.Format("兑换了{0}x{1}",
//         //                                          (object)_ItemJsonData.DataDict[good.GoodsID].name, (object)num),
//         //                                      PopTipIconType.包裹);
//         //                              }
//         //                              else
//         //                                  UIPopTip.Inst.Pop(_ItemJsonData.DataDict[good.EXGoodsID].name + "不足");
//         //                          }));
//         //                          break;
//         //                  }
//         //              });
//         //          }
//         //      }
//         //  }
//
//         uiMenPaiShop.MoneyIcon.sprite = component1.items[num1].itemIconSprite;
//         uiMenPaiShop.MoneyText.text = PlayerEx.Player.getItemNum(num1).ToString();
//     }
// }