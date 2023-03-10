using System;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils;

[DialogEvent("ShowMiniShop")]
[DialogEvent("显示迷你兑换")]
public class ShowMiniShop : IDialogEvent
{
    private int _itemId;
    private int _price;
    private int _maxSellCount;
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
        _itemId = command.GetInt(0);
        _price = command.GetInt(1);
        _maxSellCount = command.GetInt(2);
        UiUtils.ShowMiniShop(_itemId,_price,_maxSellCount);
        MyLog.Log(command,$"显示迷你兑换 兑换物品id:{_itemId} 价格:{_price} 上限限购:{_maxSellCount}" );
        MyLog.LogCommand(command, false);
        callback?.Invoke();
    }
}