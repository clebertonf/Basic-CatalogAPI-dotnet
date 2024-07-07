using CatalogAPI.Validations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Models;

[Table("categories")]
public class Category : IValidatableObject
{
    public Category()
    {
        Products = new Collection<Product>();
    }

    [Key]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatorio.")]
    [StringLength(100)]
    [ValidName]
    public string? Name { get; set; }
    //[Required]
    [StringLength(300)]
    public string? UrlImage { get; set; }
    public ICollection<Product>? Products { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(this.UrlImage))
        {
            yield return new ValidationResult("UrlImage cannot be empty.", new[] { nameof(this.UrlImage)});
        }
    }
}
