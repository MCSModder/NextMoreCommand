namespace SkySwordKill.NextMoreCommand.Custom.SkillCombo;

public class CacheSkillCombo
{
    public int Index;
    public SkillComboData SkillComboData;
    public string Name => SkillComboData.SkillName;
    public SkillCombo SkillCombo;

    public static CacheSkillCombo Create(int index, SkillCombo skillCombo, SkillComboData skillComboData) =>
        new(index, skillCombo, skillComboData);
    public CacheSkillCombo(int index,SkillCombo skillCombo,SkillComboData skillComboData )
    {
        SkillCombo = skillCombo;
        Index = index;
        SkillComboData = skillComboData;
    }
}