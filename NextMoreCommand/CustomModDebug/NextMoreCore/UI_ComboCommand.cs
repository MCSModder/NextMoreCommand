/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;
using SkySwordKill.Next.FGUI.Component;
using SkySwordKill.NextFGUI.NextCore;

namespace SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore
{
    public partial class UI_ComboCommand : GComponent
    {
        public       UI_ComboSearchBox m_commandSeach;
        public       CtlComboSearchBox SearchBox;
        public       GButton           m_addButton;
        public       UI_ComInput       m_commandInput;
        public const string            URL = "ui://kxq1c75yfvcj5h";

        public static UI_ComboCommand CreateInstance()
        {
            return (UI_ComboCommand)UIPackage.CreateObject("NextMoreCore", "ComboCommand");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            m_commandSeach = (UI_ComboSearchBox)GetChild("commandSeach");
            SearchBox = new CtlComboSearchBox(m_commandSeach);
            m_addButton = (GButton)GetChild("addButton");
            m_commandInput = (UI_ComInput)GetChild("commandInput");
        }
    }
}