using WarehouseManagement.Application.ViewModels;

namespace WarehouseManagement.Application.ViewModels.CompositeViewModels;

public class HomeContentViewModel()
{
  public IEnumerable<ListProductViewModel> Products { get; set; } = new List<ListProductViewModel>();
  public IEnumerable<ListWarehouseViewModel> Warehouses { get; set; } = new List<ListWarehouseViewModel>();
  public IEnumerable<ListInventoryViewModel> Inventories { get; set; } = new List<ListInventoryViewModel>();
}
