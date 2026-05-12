using WarehouseManagement.Application.ViewModels;

namespace WarehouseManagement.Application.ViewModels.CompositeViewModels;

public class HomeContentViewModel()
{
  public IEnumerable<ListProductViewModel> ThreeProducts { get; set; } = new List<ListProductViewModel>();
  public IEnumerable<ListWarehouseViewModel> ThreeWarehouses { get; set; } = new List<ListWarehouseViewModel>();
  public IEnumerable<ListInventoryViewModel> ThreeInventories { get; set; } = new List<ListInventoryViewModel>();

  public int TotalWarehouses { get; set; }
  public int TotalProducts { get; set; }
  public int TotalInventories { get; set; }
}
