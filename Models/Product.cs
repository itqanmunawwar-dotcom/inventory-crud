using System.ComponentModel.DataAnnotations;

namespace Inventory.Web.Models;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0.01, 1000000)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Display(Name = "Stock quantity")]
    [Range(0, 100000)]
    public int StockQuantity { get; set; }

    [Display(Name = "Last updated")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
