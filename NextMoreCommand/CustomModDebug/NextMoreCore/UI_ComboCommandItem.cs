/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;
using SkySwordKill.Next.FGUI.Component;
using SkySwordKill.NextFGUI.NextCore;

namespace SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore
{
    public partial class UI_ComboCommandItem : GComponent
    {
        public UI_ComboSearchBox m_commandBox;
        public CtlComboSearchBox SearchBox;
        public UI_ComInput m_commandInput;
        public GButton m_downButton;
        public GButton m_upButton;
        public GButton m_deleteButton;
        public const string URL = "ui://kxq1c75yfvcj5i";

        public static UI_ComboCommandItem CreateInstance()
        {
            return (UI_ComboCommandItem)UIPackage.CreateObject("NextMoreCore", "ComboCommandItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_commandBox = (UI_ComboSearchBox)GetChild("commandBox");
            m_commandInput = (UI_ComInput)GetChild("commandInput");
            m_deleteButton = (GButton)GetChild("deleteButton");
            draggable = true;
        }
    }
}