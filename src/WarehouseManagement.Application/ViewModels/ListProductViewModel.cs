
namespace WarehouseManagement.Application.ViewModels;


public class ListProductViewModel()
{
  public Guid Id;
  public string Name;
  public string SupplierName = string.Empty;
  public Guid SupplierId;
  public int ReorderLevel;
  public string SKU = string.Empty;
  public decimal UnitCost;
  public decimal WeightLbs;
  public string Category;
  public string Barcode;

}
