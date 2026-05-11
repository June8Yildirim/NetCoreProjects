using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Core;

public class StockTracking
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  public Guid ProductId { get; set; }

  [Required]
  public Guid WarehouseId { get; set; }

  [Required]
  public int Quantity { get; set; }

  [Required]
  [DisplayName("Movement Category")]
  public MovementType Type { get; set; }

  [Required]
  public Guid SupplierId { get; set; }

  // Navigation Properties
  public virtual Product Product { get; set; } = null!;
  public virtual Warehouse Warehouse { get; set; } = null!;
  public virtual Supplier Supplier { get; set; } = null!;

  public StockTracking()
  {
    Id = Guid.NewGuid();
  }
}
