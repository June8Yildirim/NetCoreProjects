using System;
using WarehouseManagement.Models.Base;

namespace WarehouseManagement.Models
{
  public class Inventory : BaseEntity
  {
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int QuantityOnHand { get; set; }
    public int QuantityAllocated { get; set; }
    public int? MinSafetyStock { get; set; }
    public DateTime? LastCounted { get; set; }

    // Calculated property
    public int AvailableQuantity => QuantityOnHand - QuantityAllocated;
    public bool NeedsReorder => AvailableQuantity <= (MinSafetyStock ?? 0);

    // Navigation properties
    public virtual Product Product { get; set; } = null!;
    public virtual Warehouse Warehouse { get; set; } = null!;
  }
}
// using System.ComponentModel.DataAnnotations;
//
// namespace WarehouseManagement.Core;
//
// public class Inventory
// {
//   [Key]
//   public Guid Id { get; set; }
//
//
//   [Required]
//   public Guid ProductId { get; set; }
//   public virtual Product Product { get; set; } = null!;
//
//   [Required]
//   public Guid WarehouseId { get; set; }
//   public virtual Warehouse Warehouse { get; set; } = null!;
//
//   public int QuantityOnHand { get; set; }
//   public int QuantityAllocated { get; set; }
//
//   public Inventory()
//   {
//     Id = Guid.NewGuid();
//   }
// }
