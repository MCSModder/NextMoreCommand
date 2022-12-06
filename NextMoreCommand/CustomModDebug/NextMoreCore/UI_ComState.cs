/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore
{
    public partial class UI_ComState : GComponent
    {
        public GTextField m_resetState;
        public GButton m_resetButton;
        public const string URL = "ui://kxq1c75y1101q5m";

        public static UI_ComState CreateInstance()
        {
            return (UI_ComState)UIPackage.CreateObject("NextMoreCore", "ComState");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_resetState = (GTextField)GetChild("resetState");
            m_resetButton = (GButton)GetChild("resetButton");
        }
    }
}