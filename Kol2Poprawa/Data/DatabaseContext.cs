using Kol2Poprawa.Models;
using Microsoft.EntityFrameworkCore;

namespace Kol2Poprawa.Data;

public class DatabaseContext :DbContext
{
    public DbSet<Backpack> Backpack { get; set; }
    public DbSet<Character> Character  { get; set; }
    public DbSet<CharacterTitle> CharacterTitle  { get; set; }
    public DbSet<Item> Item  { get; set; }
    public DbSet<Title> Title  { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>()
        {
            new Backpack() {CharacterId = 1, ItemId= 1,Amount = 3},
            new Backpack() {CharacterId = 2, ItemId= 2,Amount = 6},
            new Backpack() {CharacterId = 3, ItemId= 3,Amount = 9}
        });
        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>()
        {
            new CharacterTitle() {CharacterId = 1,TitleId = 1,AcquiredAt = DateTime.Now},
            new CharacterTitle() {CharacterId = 2,TitleId = 2,AcquiredAt = DateTime.Now},
            new CharacterTitle() {CharacterId = 3,TitleId = 3,AcquiredAt = DateTime.Now},
        });
        modelBuilder.Entity<Character>().HasData(new List<Character>()
        {
            new Character() {CharacterId = 1,FirstName = "Jan",LastName = "Kwiatek", CurrentWeight = 20, MaxWeight = 50},   
            new Character() {CharacterId = 2,FirstName = "Miłosz",LastName = "Dziuba", CurrentWeight = 50, MaxWeight = 70},   
            new Character() {CharacterId = 3,FirstName = "Norbi",LastName = "Czaban", CurrentWeight = 100, MaxWeight = 150}
        });
        modelBuilder.Entity<Item>().HasData(new List<Item>()
        {
            new Item() {ItemId = 1,ItemName = "tor numero uno",Weight = 20},
            new Item() {ItemId = 2,ItemName = "nigol",Weight = 50},
            new Item() {ItemId = 3,ItemName = "bigol",Weight = 30},
        });
        modelBuilder.Entity<Title>().HasData(new List<Title>(){
            new Title() {TitleId = 1, TitleName = "Mocny"},
            new Title() {TitleId = 2, TitleName = "Niezły"},
            new Title() {TitleId = 3, TitleName = "Niesamowity"}
        });
    }
}