using System;
using WarehouseManagement.Models.Base;

namespace WarehouseManagement.Models
{
  public enum StockTrackingType
  {
    Receipt = 1,
    Sale = 2,
    Transfer = 3,
    Adjustment = 4,
    Return = 5
  }

  public class StockTracking : BaseEntity
  {
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public StockTrackingType Type { get; set; }
    public Guid SupplierId { get; set; }
    public string? ReferenceId { get; set; }
    public Guid? UserPerformedBy { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public virtual Product Product { get; set; } = null!;
    public virtual Warehouse Warehouse { get; set; } = null!;
    public virtual Supplier Supplier { get; set; } = null!;
    public virtual User? User { get; set; }
  }
}
// using System.ComponentModel;
// using System.ComponentModel.DataAnnotations;
//
// namespace WarehouseManagement.Core;
//
// public class StockTracking
// {
//   [Key]
//   public Guid Id { get; set; }
//
//   [Required]
//   public Guid ProductId { get; set; }
//
//   [Required]
//   public Guid WarehouseId { get; set; }
//
//   [Required]
//   public int Quantity { get; set; }
//
//   [Required]
//   [DisplayName("Movement Category")]
//   public MovementType Type { get; set; }
//
//   [Required]
//   public Guid SupplierId { get; set; }
//
//   // Navigation Properties
//   public virtual Product Product { get; set; } = null!;
//   public virtual Warehouse Warehouse { get; set; } = null!;
//   public virtual Supplier Supplier { get; set; } = null!;
//
//   public StockTracking()
//   {
//     Id = Guid.NewGuid();
//   }
// }
