using Kol2Poprawa.Models.Dto;

namespace Kol2Poprawa.Services;

public interface ICharacterService
{
    Task<GetCharacterInfoDto> GetCharacterInfo(int id);
    Task<int> AddToBackpack(int id, List<int> items);
}