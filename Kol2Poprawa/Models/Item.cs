using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kol2Poprawa.Models;

[Table("Item")]
public class Item
{
    
    [Key]
    public int ItemId { get; set; }
    
    [MaxLength(100)]
    public string ItemName { get; set; }
    
    public int Weight { get; set; }
    
    public ICollection<Backpack> Backpacks { get; set; }
}