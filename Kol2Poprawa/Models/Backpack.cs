using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kol2Poprawa.Models;

[PrimaryKey(nameof(CharacterId), nameof(ItemId) )]
[Table("Backpack")]
public class Backpack
{
    [ForeignKey(nameof(Character))]
    public int CharacterId { get; set; }

    [ForeignKey(nameof(Item))]
    public int ItemId { get; set; }
    
    public int Amount { get; set; }

    public Character Character { get; set; }
    public Item Item { get; set; }
}