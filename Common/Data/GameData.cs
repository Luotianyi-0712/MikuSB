using MikuSB.Data.Excel;

namespace MikuSB.Data;

public static class GameData
{
    public static Dictionary<uint, CardExcel> CardData { get; private set; } = [];
    public static Dictionary<uint, WeaponExcel> WeaponData { get; private set; } = [];
    public static Dictionary<uint, CardSkinExcel> CardSkinData { get; private set; } = [];
}

public static class GameResourceTemplateId
{
    public static ulong FromGdpl(uint genre, uint detail, uint particular, uint level) =>
        ((ulong)genre << 48) | ((ulong)detail << 32) | ((ulong)particular << 16) | level;

    public static ulong FromGdpl(IReadOnlyList<uint> gdpl) =>
        gdpl.Count >= 4 ? FromGdpl(gdpl[0], gdpl[1], gdpl[2], gdpl[3]) : 0;
}