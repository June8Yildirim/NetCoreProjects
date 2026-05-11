using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Core;

public class Product
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  public string SKU { get; set; } = null!;

  [Required]
  public string Name { get; set; } = null!;

  [Required]
  public int ReorderLevel { get; set; }

  [Required]
  public Guid SupplierId { get; set; }

  // Navigation Properties
  public virtual Supplier Supplier { get; set; } = null!;

  public Product()
  {
    Id = Guid.NewGuid();
  }
}
