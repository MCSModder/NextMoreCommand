/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore
{
    public partial class UI_ComDebugBar : GComponent
    {
        public GButton m_dramaIdButton;
        public GButton m_commandButton;
        public GButton m_mainMenuButton;
        public GButton m_debugButton;
        public const string URL = "ui://kxq1c75y1101q5l";

        public static UI_ComDebugBar CreateInstance()
        {
            return (UI_ComDebugBar)UIPackage.CreateObject("NextMoreCore", "ComDebugBar");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_dramaIdButton = (GButton)GetChild("dramaIdButton");
            m_commandButton = (GButton)GetChild("commandButton");
            m_debugButton = (GButton)GetChild("debugButton");
            m_mainMenuButton = (GButton)GetChild("mainMenuButton");
        }
    }
}