using GUIPackage;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class UiUtils
{
    public static void ShowCustomUiMenPaiShop(this UIMenPaiShop uiMenPaiShop)
    {
        uiMenPaiShop.SetCustomUiMenPaiShop();
        uiMenPaiShop.Show();
    }
    public static void SetCustomUiMenPaiShop(this UIMenPaiShop uiMenPaiShop)
    {
        uiMenPaiShop.ShopTitle.text = "测试";
      //  var num1 = 0;
        var levelType = PlayerEx.Player.getLevelType();
        var database =  jsonData.instance.GetComponent<ItemDatebase>();
    }
}