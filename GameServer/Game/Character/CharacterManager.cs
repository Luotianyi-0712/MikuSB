using MikuSB.Data;
using MikuSB.Data.Excel;
using MikuSB.Database;
using MikuSB.Database.Character;
using MikuSB.Enums.Item;
using MikuSB.GameServer.Game.Player;
using MikuSB.Util.Extensions;

namespace MikuSB.GameServer.Game.Character;

public class CharacterManager(PlayerInstance player) : BasePlayerManager(player)
{
    public CharacterData CharacterData { get; } = DatabaseHelper.GetInstanceOrCreateNew<CharacterData>(player.Uid);
    public async ValueTask<CardExcel?> AddCharacter(ItemTypeEnum genre, uint detail, uint particular, uint level = 1)
    {
        var characterId = GameResourceTemplateId.FromGdpl((uint)genre,detail,particular,level);
        if (CharacterData.Characters.Any(a => a.TemplateId == characterId)) return null;
        var CharacterExcel = GameData.CardData.Values.FirstOrDefault(x => x.Genre == (int)genre && x.Detail == detail && x.Particular == particular);
        if (CharacterExcel == null) return null;

        var character = new CharacterInfo
        {
            Guid = CharacterData.NextCharacterGuid++,
            TemplateId = characterId,
            Level = level,
            Break = CharacterExcel.InitBreak,
            Timestamp = Extensions.GetUnixSec(),
        };

        var weaponInfo = await Player.InventoryManager!.AddWeaponItem((ItemTypeEnum)CharacterExcel.DefaultWeaponGPDL[0], CharacterExcel.DefaultWeaponGPDL[1], CharacterExcel.DefaultWeaponGPDL[2], (uint)CharacterExcel.DefaultWeaponGPDL[3]);
        if (weaponInfo != null) character.WeaponUniqueId = weaponInfo.UniqueId;

        //var skinInfo = await Player.InventoryManager!.AddSkinItem(ItemTypeEnum.TYPE_CARD_SKIN, detail, particular, level);
        var skinInfo = Player.InventoryManager!.GetSkinItemGDPL(ItemTypeEnum.TYPE_CARD_SKIN, detail, particular, level);
        if (skinInfo != null)
        {
            character.SkinId = skinInfo.UniqueId;
            character.UnlockedSkin.Add(skinInfo.UniqueId);
        }

        CharacterData.Characters.Add(character);
        return CharacterExcel;
    }

    public CharacterInfo? GetCharacter(ulong TemplateId)
    {
        return CharacterData.Characters.Find(Character => Character.TemplateId == TemplateId);
    }

    public CharacterInfo? GetCharacterByGUID(uint guid)
    {
        return CharacterData.Characters.Find(Character => Character.Guid == guid);
    }

    public CharacterInfo? GetCharacterGDPL(ItemTypeEnum genre, int detail, int particular)
    {
        var templateId = GameResourceTemplateId.FromGdpl((uint)genre,(uint)detail,(uint)particular,1);
        return CharacterData.Characters.Find(Character => Character.TemplateId == templateId);
    }
}