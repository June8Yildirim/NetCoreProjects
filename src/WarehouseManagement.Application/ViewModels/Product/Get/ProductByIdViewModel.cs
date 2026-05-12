
namespace WarehouseManagement.Application.ViewModels;


public class ProductByIdViewModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public Guid SupplierId { get; set; }
  public int ReorderLevel { get; set; }
  public string SKU { get; set; } = string.Empty;
}
