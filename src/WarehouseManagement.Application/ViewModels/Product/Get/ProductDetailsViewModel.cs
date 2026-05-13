
using WarehouseManagement.Models;

namespace WarehouseManagement.Application.ViewModels;


public class ProductDetailsViewModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public Guid SupplierId { get; set; }
  public int ReorderLevel { get; set; }
  public string SKU { get; set; } = string.Empty;
  public string SupplierName { get; set; }
  public decimal UnitCost { get; set; }
  public decimal WeightLbs { get; set; }
  public string Category { get; set; } = string.Empty;
  public string Barcode { get; set; } = string.Empty;
  public ICollection<Inventory> Inventories { get; set; }
  public ICollection<StockTracking> StockTrackings { get; set; }
  public ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; }
}
