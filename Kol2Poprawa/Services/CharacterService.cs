using Kol2Poprawa.Data;
using Kol2Poprawa.Models;
using Kol2Poprawa.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Kol2Poprawa.Services;

public class CharacterService : ICharacterService
{
    private readonly DatabaseContext _context;

    public CharacterService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<GetCharacterInfoDto> GetCharacterInfo(int id)
    {
        var query = await _context.Character
            .Where(c => c.CharacterId == id)
            .Select(c => new GetCharacterInfoDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                CurrentWeight = c.CurrentWeight,
                MaxWeight = c.MaxWeight,
                BackpackItems = c.Backpacks.Select(b => new BackpackItemsDto
                {
                    ItemName = b.Item.ItemName,
                    ItemWeight = b.Item.Weight,
                    Amount = b.Amount,
                }).ToList(),
                Titles = c.CharacterTitles.Select(t => new CharacterTitleDto
                {
                    Title = t.Title.TitleName,
                    AquiredAt = t.AcquiredAt
                }).ToList()
            }).FirstOrDefaultAsync();
            
        if (query == null)
            throw new Exception("No character found");
        
        return query;
    }

    public async Task<int> AddToBackpack(int id, List<int> items)
    {
        var item = await _context.Item.Where(c => c.ItemId == id && c.ItemId != 0).ToListAsync();
        if (item == null)
        {
            return await Task.FromResult(-100);
        }
        
        var character = await _context.Character.Include(c => c.Backpacks)
            .FirstOrDefaultAsync(c => c.CharacterId == id);

        if (character == null)
        {
            return await Task.FromResult(-200);
        }
        
        var totalWeight = item.Sum(c => c.Weight);
        if (character.CurrentWeight + totalWeight > character.MaxWeight)
        {
            return await Task.FromResult(-300);
        }
        
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var itemToAdd in item)
            {
                var backpackItem = character.Backpacks.FirstOrDefault(b => b.ItemId == itemToAdd.ItemId);
                if (backpackItem != null)
                {
                    backpackItem.Amount += 1;
                }
                else
                {
                    character.Backpacks.Add(new Backpack
                    {
                        ItemId = itemToAdd.ItemId,
                        Amount = 1
                    });
                }
                character.CurrentWeight += itemToAdd.Weight;
            }
            
            _context.Character.Update(character);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return await Task.FromResult(1);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }}