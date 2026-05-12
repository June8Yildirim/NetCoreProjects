using System;
using System.Collections.Generic;
using WarehouseManagement.Models.Base;

namespace WarehouseManagement.Models
{
  public class Product : BaseEntity
  {
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int ReorderLevel { get; set; }
    public Guid SupplierId { get; set; }
    public decimal? UnitCost { get; set; }
    public decimal? WeightLbs { get; set; }
    public string? Category { get; set; }
    public string? Barcode { get; set; }

    // Navigation properties
    public virtual Supplier Supplier { get; set; } = null!;
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    public virtual ICollection<StockTracking> StockTrackings { get; set; } = new List<StockTracking>();
    public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; } = new List<PurchaseOrderLine>();
  }
}
// using System.ComponentModel.DataAnnotations;
//
// namespace WarehouseManagement.Core;
//
// public class Product
// {
//   [Key]
//   public Guid Id { get; set; }
//
//   [Required]
//   public string SKU { get; set; } = null!;
//
//   [Required]
//   public string Name { get; set; } = null!;
//
//   [Required]
//   public int ReorderLevel { get; set; }
//
//   [Required]
//   public Guid SupplierId { get; set; }
//
//   // Navigation Properties
//   public virtual Supplier Supplier { get; set; } = null!;
//
//   public Product()
//   {
//     Id = Guid.NewGuid();
//   }
// }
