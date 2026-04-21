namespace MikuSB.Data.Excel;

[ResourceEntity("weapon.json")]
public class WeaponExcel : ExcelResource
{
    public uint Genre { get; set; }
    public uint Detail { get; set; }
    public uint Particular { get; set; }
    public uint Level { get; set; }
    public int Class { get; set; }
    public uint Icon { get; set; }
    public int EvolutionMatID { get; set; }
    public int BreakMatID { get; set; }
    public int LevelLimitID { get; set; }
    public int BreakLimitID { get; set; }
    public int AppearID { get; set; }
    public List<int> DefaultSkillID { get; set; } = [];
    public List<int> WeaponPartsLimit { get; set; } = [];
    public string I18n { get; set; } = "";
    public override uint GetId()
    {
        return (uint)I18n.GetHashCode();
    }

    public override void Loaded()
    {
        GameData.WeaponData.Add(GetId(), this);
    }
}