
namespace WarehouseManagement.Application.ViewModels;


public class ListProductViewModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string SupplierName { get; set; } = string.Empty;
  public Guid SupplierId { get; set; }
  public int ReorderLevel { get; set; }
  public string SKU { get; set; } = string.Empty;
  public decimal UnitCost { get; set; }
  public decimal WeightLbs { get; set; }
  public string Category { get; set; } = string.Empty;
  public string Barcode { get; set; } = string.Empty;
}
