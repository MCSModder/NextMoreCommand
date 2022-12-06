/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SkySwordKill.NextMoreCommand.CustomModDebug.NextMoreCore
{
    public partial class UI_ComInput : GComponent
    {
        public GTextInput m_inContent;
        public const string URL = "ui://kxq1c75yfvcj5j";

        public static UI_ComInput CreateInstance()
        {
            return (UI_ComInput)UIPackage.CreateObject("NextMoreCore", "ComInput");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_inContent = (GTextInput)GetChild("inContent");
        }
    }
}