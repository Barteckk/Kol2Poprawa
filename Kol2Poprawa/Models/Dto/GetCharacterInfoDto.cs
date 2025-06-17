namespace Kol2Poprawa.Models.Dto;

public class GetCharacterInfoDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<BackpackItemsDto> BackpackItems { get; set; }
    public List<CharacterTitleDto> Titles { get; set; }
}

public class BackpackItemsDto
{
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class CharacterTitleDto
{
    public string Title { get; set; }
    public DateTime AquiredAt { get; set; }
}