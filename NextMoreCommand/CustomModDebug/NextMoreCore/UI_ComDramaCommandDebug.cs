/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore
{
    public partial class UI_ComDramaCommandDebug : GComponent
    {
        public       GList           m_commandList;
        public       UI_ComboCommand m_addCommand;
        public       GList           m_optionList;
        public       GList           m_charaIdList;
        public       GButton         m_runButton;
        public       GButton         m_copyJsonButton;
        public       GButton         m_saveJsonButton;
        public       UI_ComInput     m_jsonID;
        public       UI_ComState     m_resetState;
        public       GButton         m_addOptionButton;
        public       GButton         m_addCharacterIdButton;
        public const string          URL = "ui://kxq1c75yfvcj5g";

        public static UI_ComDramaCommandDebug CreateInstance()
        {
            return (UI_ComDramaCommandDebug)UIPackage.CreateObject("NextMoreCore", "ComDramaCommandDebug");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_commandList = (GList)GetChild("commandList");
            m_addCommand = (UI_ComboCommand)GetChild("addCommand");
            m_optionList = (GList)GetChild("optionList");
            m_charaIdList = (GList)GetChild("charaIdList");
            m_runButton = (GButton)GetChild("runButton");
            m_copyJsonButton = (GButton)GetChild("copyJsonButton");
            m_saveJsonButton = (GButton)GetChild("saveJsonButton");
            m_jsonID = (UI_ComInput)GetChild("jsonID");
            m_resetState = (UI_ComState)GetChild("resetState");
            m_addOptionButton = (GButton)GetChild("addOptionButton");
            m_addCharacterIdButton = (GButton)GetChild("addCharacterIdButton");
        }
    }
}