/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore
{
    public partial class UI_ComboCommandCharacterItem : GComponent
    {
        public UI_ComInput m_itemInput;
        public GComboBox m_itemBox;
        public GButton m_deleteButton;
        public const string URL = "ui://kxq1c75yfvcj5k";

        public static UI_ComboCommandCharacterItem CreateInstance()
        {
            return (UI_ComboCommandCharacterItem)UIPackage.CreateObject("NextMoreCore", "ComboCommandCharacterItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_itemInput = (UI_ComInput)GetChild("itemInput");
            m_itemBox = (GComboBox)GetChild("itemBox");
            m_deleteButton = (GButton)GetChild("deleteButton");
        }
    }
}