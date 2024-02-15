/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;
using SkySwordKill.Next.FGUI.Component;
using SkySwordKill.NextFGUI.NextCore;

namespace SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore
{
    public partial class UI_ComDrama : GComponent
    {
        public       UI_ComToolsSearchBox m_search;
        public       CtlToolsSearchBox    SearchBox;
        public       GList                m_list;
        public       GList                m_listDramaId;
        public       GList                m_listString;
        public       GList                m_ListInt;
        public       UI_ComState          m_resetState;
        public const string               URL = "ui://kxq1c75yfvcj5f";

        public static UI_ComDrama CreateInstance()
        {
            return (UI_ComDrama)UIPackage.CreateObject("NextMoreCore", "ComDrama");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_search = (UI_ComToolsSearchBox)GetChild("search");
            SearchBox = new CtlToolsSearchBox(m_search);
            m_list = (GList)GetChild("list");
            m_listDramaId = (GList)GetChild("listDramaId");
            m_listString = (GList)GetChild("listString");
            m_ListInt = (GList)GetChild("ListInt");
            m_resetState = (UI_ComState)GetChild("resetState");
        }
    }
}