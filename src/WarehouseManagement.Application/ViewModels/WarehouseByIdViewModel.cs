namespace WarehouseManagement.Application.ViewModels;

public class WarehouseByIdViewModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? WarehouseCode { get; set; }

}
