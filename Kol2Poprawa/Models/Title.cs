﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kol2Poprawa.Models;

[Table("Title")]
public class Title
{
    [Key] public int TitleId { get; set; }

    [MaxLength(100)] public string TitleName { get; set; }

    public ICollection<CharacterTitle> CharacterTitles { get; set; }
}