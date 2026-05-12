namespace WarehouseManagement.Application.ViewModels;

public class ListInventoryViewModel
{
  public Guid Id { get; set; }
  public string ProductName { get; set; } = string.Empty;
  public string WarehouseName { get; set; } = string.Empty;
  public Guid ProductId { get; set; }
  public Guid WarehouseId { get; set; }
  public int QuantityOnHand { get; set; }
  public int QuantityAllocated { get; set; }

}
