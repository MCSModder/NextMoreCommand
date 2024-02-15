/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;
using SkySwordKill.NextFGUI.NextCore;

namespace SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore
{
    public partial class UI_ModDialogDebug : GComponent
    {
        public       UI_WindowFrameDialogStyle2 m_frame;
        public       UI_ComDrama                m_drama;
        public       UI_ComDramaCommandDebug    m_command;
        public       UI_ComDebugBar             m_debugBar;
        public       GTextField                 m_debugText;
        public const string                     URL = "ui://kxq1c75yui2j5c";

        public static UI_ModDialogDebug CreateInstance()
        {
            return (UI_ModDialogDebug)UIPackage.CreateObject("NextMoreCore", "ModDialogDebug");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_frame = (UI_WindowFrameDialogStyle2)GetChild("frame");
            m_drama = (UI_ComDrama)GetChild("drama");
            m_command = (UI_ComDramaCommandDebug)GetChild("command");
            m_debugBar = (UI_ComDebugBar)GetChild("debugBar");
            m_debugText = (GTextField)GetChild("debugText");
        }
    }
}