using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Core;

public class Inventory
{
  [Key]
  public Guid Id { get; set; }


  [Required]
  public Guid ProductId { get; set; }
  public virtual Product Product { get; set; } = null!;

  [Required]
  public Guid WarehouseId { get; set; }
  public virtual Warehouse Warehouse { get; set; } = null!;

  public int QuantityOnHand { get; set; }
  public int QuantityAllocated { get; set; }

  public Inventory()
  {
    Id = Guid.NewGuid();
  }
}
