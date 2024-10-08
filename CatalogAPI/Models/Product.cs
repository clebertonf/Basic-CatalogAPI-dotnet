﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogAPI.Models;

[Table("products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
    [Required]
    [Column(TypeName ="decimal(10,2)")]
    public decimal? Price { get; set; }
    [Required]
    [StringLength(300)]
    public string? UrlImage { get; set; }
    public float Stock { get; set; }
    public DateTime RegisterDate { get; set; }
    public int CategoryId { get; set; }

    [JsonIgnore] // ignore properties
    public Category? Category { get; set; }
}
