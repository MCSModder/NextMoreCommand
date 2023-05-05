using System;
using System.IO;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next.Extension;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.SkillCombo;

namespace SkySwordKill.NextMoreCommand.CustomModData
{
    [ModData("CustomSkillCombo")]
    public class CustomSkillComboData : IModData
    {

        public void Read(ModConfig modConfig)
        {
            CustomModDataManager.LoadData(modConfig.GetNDataDir(), "CustomSkillCombo", modConfig, LoadCustomSkillCombo);
        }
        public bool Check(ModConfig modConfig)
        {
            var modNDataDir = modConfig.GetNDataDir();
            return modNDataDir.CombinePath("CustomSkillCombo").HasPath();
        }
        private void LoadCustomSkillCombo(string path, ModConfig modConfig)
        {
            foreach (var filePath in Directory.GetFiles(path, "*.json", SearchOption.AllDirectories))
            {
                try
                {
                    var json = File.ReadAllText(filePath);

                    var skill = JObject.Parse(json)?.ToObject<SkillCombo>();
                    if (skill == null)
                    {
                        continue;
                    }

                    skill.Init();
                    SkillComboManager.SkillCombos[skill.SkillName] = skill;
                    MyPluginMain.LogInfo(string.Format("ModManager.LoadData".I18N(),
                        filePath));
                }
                catch (Exception e)
                {
                    throw new ModLoadException(string.Format("CustomSkillCombo {0} 加载失败。".I18NTodo(), filePath), e);
                }
            }
        }


    }
}