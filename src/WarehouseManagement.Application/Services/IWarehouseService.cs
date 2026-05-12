using WarehouseManagement.Application.ViewModels;

namespace WarehouseManagement.Application.Services;

public interface IWarehouseService
{
  Task<List<ListWarehouseViewModel>> GetAllWarehousesAsync();
  Task<List<ListWarehouseViewModel>> Get3WarehousesAsync();
  Task<WarehouseByIdViewModel> GetWarehouseByIdViewModelAsync(Guid Id);
  Task<int> GetTotalWarehousesCountAsync();
}
